using Assets;
using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Dictionary<int, ProjectileManager> bulletDict = new();
    public PlayerMovementScript Player;
    public float bounty = 0;
    private float currentDangerLevel;
    private List<ShipScript> existingShips = new();
    private EncounterType encounterType;

    [SerializeField] Button optionButton;
    [SerializeField] GameObject basicProjectile;
    [SerializeField] AlienShipScript basicAlien;
    [SerializeField] RectTransform rewardMenu;
    [SerializeField] RadarScript radar;
    [SerializeField] Transform radarHolder;
    [SerializeField] TextMeshProUGUI bountyText;
    [SerializeField] GameObject deathscreen;

    float spawnDelay = 1.0f;
    bool roundHasEnded = false;
    bool roundCanEnd = false;

    float deathScreenTimer = 3.0f;
    bool playerHasDied = false;
    bool playerDeathEventHasBeenSet = false;

    void Start()
    {
        Instance = this;
        AddBounty(5.0f);
        encounterType = EncounterType.Basic;
    }
    // Update is called once per frame
    void Update()
    {
        if (!playerDeathEventHasBeenSet && Player != null)
        {
            Player.PlayerDied += () =>
            {
                playerHasDied = true;
            };
        }
        if (Input.GetAxisRaw("Close") == 1)
        {
            Debug.Log("Quitting Game");
            Application.Quit();
        }
        if (playerHasDied)
        {
            if (deathScreenTimer <= 0)
            {
                deathscreen.SetActive(true);
            }
            else
            {
                deathScreenTimer -= Time.deltaTime;
            }
        }
        if (spawnDelay > 0)
        {
            spawnDelay -= Time.deltaTime;
        }
        if (currentDangerLevel < bounty && spawnDelay <= 0)
        {
            var alienShip = SpawnAlien(basicAlien);
            existingShips.Add(alienShip);
        }
        if (!roundCanEnd && currentDangerLevel >= bounty)
        {
            roundCanEnd = true;
        }
        for (int i = 0; i < existingShips.Count; i++)
        {
            if (existingShips[i] == null)
            {
                existingShips.Remove(existingShips[i]);
                i--;
            }
        }
        if (roundCanEnd && existingShips.Count == 0 && !roundHasEnded)
        {
            roundHasEnded = true;
            Debug.Log("Round finished");
            switch (encounterType)
            {
                case EncounterType.Endless:
                    spawnDelay = 5;
                    roundHasEnded = false;
                    roundCanEnd = false;
                    currentDangerLevel = 0;
                    AddBounty(1.0f);
                    break;
                case EncounterType.Basic:
                    spawnDelay = 0;
                    roundHasEnded = false;
                    roundCanEnd = false;
                    currentDangerLevel = 0;
                    AddBounty(1.0f);
                    OpenMenu(3);
                    break;
                default:
                    break;
            }
        }

    }
    private void OpenMenu(int rewardCount)
    {
        List<Reward> rewards = new();
        var crewMember = new CrewMember
        {
            Title = "Bobby Hill",
            Subtitle = "Gives you 10% speed boost"
        };
        rewards.Add(crewMember);

        var baseTransform = new GameObject().transform;
        var buttons = new List<Button>();
        float spacing = rewardMenu.rect.height / rewardCount + 1;
        for (int i = 0; i < rewardCount; i++)
        {
            var reward = rewards[Random.Range(0, rewards.Count - 1)];
            var transform = rewardMenu.transform;
            transform.position = new Vector3(0, (spacing * (i + 1)));
            var button = Instantiate(optionButton, transform);
            buttons.Add(button);
            var textBoxes = button.GetComponentsInChildren<TextMeshPro>();
            foreach (var textBox in textBoxes)
            {
                if (textBox.gameObject.name.ToLower().Contains("subtitle"))
                {
                    textBox.text = crewMember.Subtitle;
                }
                else if (textBox.gameObject.name.ToLower().Contains("title"))
                {
                    textBox.text = crewMember.Title;
                }
            }
            button.onClick.AddListener(() =>
            {
                while (buttons.Count > 0)
                {
                    Destroy(buttons[0]);
                }
            });
        }
    }
    private void AddBounty(float bountyIncrease)
    {
        bounty += bountyIncrease;
        bountyText.text = $"${bounty * 10}00";
    }
    private AlienShipScript SpawnAlien(AlienShipScript shipToSpawn)
    {
        var randomNum = Random.Range(-10, 10);
        switch (shipToSpawn.type)
        {
            case AlienShipScript.AlienType.Basic:
                currentDangerLevel += 1;
                break;
            default:
                Debug.LogError("Attempted to spawn and alien of an unkown type");
                break;
        }
        var alienShip = Instantiate(shipToSpawn);
        alienShip.transform.SetPositionAndRotation(transform.position + new Vector3(randomNum, 10), transform.rotation);
        var radarObj = Instantiate(radar, radarHolder);
        radarObj.matchingShip = alienShip;
        spawnDelay = 1.0f;
        return alienShip;
    }
    public void ShootProjectile(ProjectileType projectileType, Transform transform, Team team, float damage, float velocity, float angle)
    {
        // Adjust for 90 degree skew
        //angle += 90;

        var projectileObj = Instantiate(basicProjectile);
        projectileObj.transform.position = transform.position;
        projectileObj.transform.rotation = transform.rotation;
        projectileObj.name = "Projectile";
        projectileObj.layer = 9 + (int)team;


        bulletDict.Add(projectileObj.GetInstanceID(), new ProjectileManager()
        {
            type = projectileType,
            owningTeam = team,
            damage = damage,
            velocity = velocity,
            angle = angle
        });
        //Debug.Log("bullet shot");
    }
    public void ResetLevel()
    {
        for (int i = 0; i < existingShips.Count;)
        {
            Destroy(existingShips[i]);
            bounty = 0;
            AddBounty(5);
        }
    }
    public class ProjectileManager
    {
        public Team owningTeam;
        public float damage;
        public float velocity;
        public float angle;
        public ProjectileType type;
    }
    public enum ProjectileType
    {
        Basic = 0
    }
    public enum Team
    {
        Player = 0,
        Alien = 1
    }
    private enum EncounterType
    {
        Endless,
        Basic
    }
}
