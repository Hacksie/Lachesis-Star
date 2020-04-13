using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HackedDesign
{
    [RequireComponent(typeof(Camera))]
    public class Cursor : MonoBehaviour
    {
        private Camera mainCamera;
        private Vector2 mousePosition;
        [SerializeField] private LayerMask selectableLayerMask = 0;
        // Start is called before the first frame update
        void Start()
        {
            mainCamera = GetComponent<Camera>();
        }

        public void UpdateMousePoint(InputAction.CallbackContext context)
        {
            mousePosition = context.ReadValue<Vector2>();
        }

        public void MouseClicked(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.PLAY)
                {
                    Game.instance.SelectSelectable();
                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            if(Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.PLAY)
            {
                
                RaycastHit2D hit = Physics2D.Raycast(mainCamera.ScreenToWorldPoint(mousePosition), Vector2.zero, 100.0f, selectableLayerMask);

                if(hit.transform != null)
                {
                    Selectable s = hit.transform.gameObject.GetComponent<Selectable>();
                    if(s != null)
                    {
                        Game.instance.SetSelectable(s);
                    }
                    else
                    {
                        Logger.LogWarning(name, "Selectable without component");
                    }
                    /*
                    Planet p = hit.transform.gameObject.GetComponent<Planet>();
                    if (p != null)
                    {
                        Game.instance.SetHoverPlanet(p);
                    }
                    else
                    {

                        Logger.LogWarning(name, "Planet without Planet component");
                    }*/
                }
                else
                {
                    Game.instance.SetSelectable(null);
                }
            }
        }
    }
}