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
    public static GameMode gameMode;

    public PlayerScript Player;
    public float bounty = 0;
    private float currentDangerLevel;
    private readonly List<ShipScript> existingShips = new();
    private EncounterType encounterType;

    [SerializeField] Button optionButton;
    [SerializeField] GameObject basicProjectile;
    [SerializeField] AlienShipScript basicAlien;
    [SerializeField] RectTransform rewardMenu;
    [SerializeField] RadarScript radar;
    [SerializeField] Transform radarHolder;
    [SerializeField] TextMeshProUGUI bountyText;
    [SerializeField] GameObject deathscreen;

    readonly List<Reward> rewards = new();

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
            playerDeathEventHasBeenSet = true;
            Player.PlayerDied += () =>
            {
                playerHasDied = true;
            };
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
        var crewMember = new CrewMember
        {
            Title = "Bobby Hill",
            Subtitle = "Gives you 10% speed boost"
        };
        rewards.Add(crewMember);

        var baseTransform = new GameObject().transform;
        var buttons = new List<Button>();
        float spacing = rewardMenu.rect.height / (rewardCount + 1) - 200f;
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
    public void ResetLevel()
    {
        for (int i = 0; i < existingShips.Count;)
        {
            Destroy(existingShips[i]);
            bounty = 0;
            AddBounty(5);
        }
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
    public enum GameMode
    {
        Endless = 0,
        Story = 1
    }
}
