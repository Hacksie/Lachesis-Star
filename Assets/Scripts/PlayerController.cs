using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidBody = null;
        [SerializeField] private float turnRate = 180.0f;
        [SerializeField] private float maxThrust = 5.0f;

        private float turn = 0.0f;

        private float thrust = 0.0f;

        // Start is called before the first frame update
        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(new Vector3(0, 0, -1.0f * turn * turnRate * Time.deltaTime));

            float force = thrust * maxThrust * Time.deltaTime;

            rigidBody.AddRelativeForce(new Vector2(0, force), ForceMode2D.Impulse);
            if (rigidBody.velocity.magnitude > maxThrust)
            {
                rigidBody.velocity = rigidBody.velocity.normalized * maxThrust;
            }
        }

        public void UpdateRotation(InputAction.CallbackContext context)
        {

            if(context.performed)
            {
                turn = context.ReadValue<float>();
            }
            if(context.canceled)
            {
                turn = 0.0f;
            }
        }

        public void UpdateThrust(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                thrust = context.ReadValue<float>();
            }
            if(context.canceled)
            {
                thrust = 0.0f;
            }
        }

    }
}