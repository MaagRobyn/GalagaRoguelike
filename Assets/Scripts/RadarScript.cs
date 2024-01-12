using System.Collections;
using UnityEngine;

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
            if (matchingShip == null)
            {
                Destroy(gameObject);
            }
            else
            {
                var angles = Vector3.forward * Vector2.Angle(GameManager.Instance.PlayerTransform.position, matchingShip.transform.position);

                transform.eulerAngles = angles;

            }
        }
    }
}