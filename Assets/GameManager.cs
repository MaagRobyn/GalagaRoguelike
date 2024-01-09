using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Dictionary<int, ProjectileManager> bulletDict = new Dictionary<int, ProjectileManager>();
    public Dictionary<int, ShipScript> damagableDict = new Dictionary<int, ShipScript>();
    [SerializeField] public ShipScript player;

    //[SerializeField] Dictionary<ProjectileType, GameObject> projectiles = new Dictionary<ProjectileType, GameObject>();
    //[SerializeField] Dictionary<AlienType, GameObject> aliens = new Dictionary<AlienType, GameObject>();
    [SerializeField] GameObject basicProjectile;
    [SerializeField] ShipScript basicAlien;

    float spawnDelay = 1.0f;
    int maxAliens = 2;

    void Start()
    {
        Instance = this;
        damagableDict.Add(player.GetInstanceID(), player);
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnDelay > 0)
        {
            spawnDelay -= Time.deltaTime;
        }
        if(damagableDict.Count < maxAliens && spawnDelay <= 0) 
        {
            var tmp = new GameObject();
            var randomNum = Random.Range(-10, 10);
            tmp.transform.SetPositionAndRotation(transform.position + new Vector3(randomNum,10), transform.rotation);
            var alien = Instantiate(basicAlien, tmp.transform);
            damagableDict.Add(alien.GetInstanceID(), alien);
            spawnDelay = 1.0f;
        }
    }

    public void ShootProjectile(ProjectileType projectileType, Transform transform, Team team, float damage, float velocity, float angle)
    {
        var bulletTransform = new GameObject().transform;
        bulletTransform.position = transform.position;
        bulletTransform.rotation = transform.rotation;

        // Adjust for 90 degree skew
        //angle += 90;

        var projectileObj = Instantiate(basicProjectile, bulletTransform);
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
    public enum AlienType
    {
        Basic = 0
    }
}
