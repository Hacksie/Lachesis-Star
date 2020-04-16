using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class ActionPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;
        // Start is called before the first frame update
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.PLAY)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
            }
        }

        public void MapClicked()
        {
            Logger.Log(name, "Map Clicked");
            Game.instance.SetPlayStateMap();
        }

        public void ShipClicked()
        {
            Logger.Log(name, "Ship Clicked");
            Game.instance.SetPlayStateCargo();
        }

    }
}
