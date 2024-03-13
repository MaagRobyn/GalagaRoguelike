using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class RadarScript : MonoBehaviour
    {
        public GameObject matchingObject;
        public Image radar;
        private const int RADAR_RADIUS = 13;

        // Use this for initialization
        void Start()
        {
            radar = GetComponentInChildren<Image>();
            RotateTowardsTarget();

        }

        private void FixedUpdate()
        {
            if (matchingObject == null || matchingObject.IsDestroyed() || !matchingObject.gameObject.activeSelf)
            {
                Destroy(gameObject);
            }
            else
            {
                RotateTowardsTarget();
            }
        }

        private void RotateTowardsTarget()
        {
            var cameraTransform = GameManager.Instance.Player.GetComponentInChildren<Camera>().transform;
            var distance = Vector3.Distance(matchingObject.transform.position, GameManager.Instance.Player.transform.position);
            //Debug.Log(distance);
            if (radar.enabled && distance <= RADAR_RADIUS)
            {
                radar.enabled = false;
            }
            else if (!radar.enabled && distance > RADAR_RADIUS)
            {
                radar.enabled = true;
            }
            var angle = Tools.FindAngleBetweenTwoPositions(cameraTransform.position, matchingObject.transform.position);
            var angles = (Vector3.forward * angle) - cameraTransform.eulerAngles;
            transform.eulerAngles = angles;

            //Debug.DrawLine(cameraTransform.position, matchingShip.transform.position, Color.blue);
        }
    }
}