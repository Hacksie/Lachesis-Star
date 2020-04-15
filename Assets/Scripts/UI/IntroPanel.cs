using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(CanvasGroup))]
    public class IntroPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();

        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.INTRO)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        public void CloseClicked()
        {
            Game.instance.SetPlayStateMap();
        }
    }
}