using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts
{
    public class RadarScript : MonoBehaviour
    {
        public ShipScript matchingShip;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            
            if (matchingShip == null || matchingShip.IsDestroyed())
            {
                Destroy(gameObject);
            }
            else
            {
                var playerTransform = GameManager.Instance.PlayerTransform;

                var angle = Tools.FindAngleBetweenTwoTransforms(playerTransform, matchingShip.transform);
                var angles = Vector3.forward * angle - playerTransform.eulerAngles;
                transform.eulerAngles = angles;

                //Debug.DrawLine(playerTransform.position, matchingShip.transform.position, Color.blue);
            }
        }
    }
}