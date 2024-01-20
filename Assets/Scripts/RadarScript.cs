using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

namespace Assets.Scripts
{
    public class RadarScript : MonoBehaviour
    {
        public ShipScript matchingShip;
        public Image radar;
        private const int RADAR_RADIUS = 13;

        // Use this for initialization
        void Start()
        {
            radar = GetComponentInChildren<Image>();
 
        }

        private void FixedUpdate()
        {
            if (matchingShip == null || matchingShip.IsDestroyed())
            {
                Destroy(gameObject);
            }
            else
            {
                var distance = Vector3.Distance(matchingShip.transform.position, transform.position);
                //Debug.Log(distance);
                if (radar.enabled && distance <= RADAR_RADIUS)
                {
                    radar.enabled = false;
                }
                else if (!radar.enabled && distance > RADAR_RADIUS)
                {
                    radar.enabled = true;
                }
                var cameraTransform = GameManager.Instance.Player.GetComponentInChildren<Camera>().transform;
                var angle = Tools.FindAngleBetweenTwoPositions(cameraTransform.position, matchingShip.transform.position);
                var angles = (Vector3.forward * angle) - cameraTransform.eulerAngles;
                transform.eulerAngles = angles;

                Debug.DrawLine(cameraTransform.position, matchingShip.transform.position, Color.blue);
            }
        }
    }
}