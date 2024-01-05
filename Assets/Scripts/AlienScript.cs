using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    private static GameObject StaticPlayerObj;

    [SerializeField] private GameObject Player;
    [SerializeField] private float firerate = 1f;
    [SerializeField] private Rigidbody2D rb;

    float angleToPlayer = 0;
    Vector2 playerVector = Vector2.zero;
    Vector2 alienVector = Vector2.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        if (StaticPlayerObj == null)
        {
            StaticPlayerObj = Player;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.SetRotation(Vector2.Angle(gameObject.transform.position, StaticPlayerObj.transform.position));
    }
}
