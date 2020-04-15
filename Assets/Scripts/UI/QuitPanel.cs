using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    [RequireComponent(typeof(CanvasGroup))]
    public class QuitPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }


        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.PLAY)
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

        public void QuitClicked()
        {
            Game.instance.QuitPlaying();
        }

    }

}