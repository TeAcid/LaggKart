using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof(CarController))]
    public class Road_detection : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        // Use this for initialization
        void Start()
        {
            m_Car = GetComponent<CarController>();
        }

        /* private void OnCollisionEnter(Collision collision)
         {
             Debug.Log(collision.gameObject.tag);
         }*/

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Proga")
            {
                Debug.Log("Smo na progi");
            }
            else if (collision.gameObject.tag != "Proga")
            {
                Debug.Log("Nismo na progi");
                // pass the input to the car!
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
                float handbrake = CrossPlatformInputManager.GetAxis("Jump");
                m_Car.Move(h, v, v, 1f);
#else
            m_Car.Move(h, v, v, 0f);
#endif
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
