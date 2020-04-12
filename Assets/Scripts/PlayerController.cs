using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Runtime GameObjects")]
        [SerializeField] private Rigidbody2D rigidBody = null;
        [Header("Configured GameObjects")]
        [SerializeField] private ShipData shipData = null;
        [Header("Settings")]
        [SerializeField] private float turnRate = 180.0f;
        //[SerializeField] private float maxThrust = 5.0f;

        private float turn = 0.0f;

        public float thrust = 0.0f;
        public float currentThrust = 0.0f;
        public float maxThrust = 0.0f;

        // Start is called before the first frame update
        void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.PLAY)
            {
                transform.Rotate(new Vector3(0, 0, -1.0f * turn * turnRate * Time.deltaTime));

                var engine1 = shipData.GetEngine(Game.instance.state.shipState.engines[0]);
                var engine2 = shipData.GetEngine(Game.instance.state.shipState.engines[1]);

                maxThrust = engine1.thrustRate + engine2.thrustRate;
                currentThrust = thrust * maxThrust;

                float force = currentThrust * Time.deltaTime;

                rigidBody.AddRelativeForce(new Vector2(0, force), ForceMode2D.Impulse);
                if (rigidBody.velocity.magnitude > maxThrust)
                {
                    rigidBody.velocity = rigidBody.velocity.normalized * maxThrust;
                }
            }
        }

        public void Turn(InputAction.CallbackContext context)
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

        public void Thrust(InputAction.CallbackContext context)
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

        public void Brake(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                thrust = 0.0f;
                rigidBody.velocity = Vector2.zero;
            }
        }

        public void Ship(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                Game.instance.ToggleCargo();
            }
        }

        public void Map(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                Game.instance.ToggleMap();
                
            }
        }

        public void Quit(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                Game.instance.QuitPlaying();
            }
        }
    }
}