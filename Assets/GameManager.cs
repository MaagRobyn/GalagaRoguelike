using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Dictionary<int, ProjectileManager> bulletDict = new Dictionary<int, ProjectileManager>();
    public static GameManager Instance { get; private set; }

    [SerializeField] GameObject basicProjectile;
    [SerializeField] GameObject basicAlien;

    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShootProjectile(ProjectileType projectileType, Vector3 position, Team team, float damage, float velocity, float angle)
    {
        var bulletTransform = new GameObject().transform;
        bulletTransform.position = position;

        // Adjust for 90 degree skew
        angle += 90;

        var projectileObj = Instantiate(basicProjectile, bulletTransform);
        bulletDict.Add(projectileObj.GetInstanceID(), new ProjectileManager() 
        { 
            type = projectileType,
            owningTeam = team,
            damage = damage,
            velocity = velocity,
            angle = angle
        });
        Debug.Log("bullet shot");
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
}
