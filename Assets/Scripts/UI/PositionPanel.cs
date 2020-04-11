using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class PositionPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;
        [SerializeField] Text XText = null;
        [SerializeField] Text YText = null;
        [SerializeField] Transform playerTransform = null;
        
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (XText == null)
            {
                Logger.LogError(name, "XText is null");
            }
            if (YText == null)
            {
                Logger.LogError(name, "YText is null");
            }
            if (playerTransform == null)
            {
                Logger.LogError(name, "playerTransform is null");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                UpdatePanel();
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
            }

            
            //positionText.text =  $"{playerTransform.position.x:N1} , {playerTransform.position.y:N1}";
        }

        private void UpdatePanel()
        {
            XText.text = $"{playerTransform.position.x:N1}";
            YText.text = $"{playerTransform.position.y:N1}";
        }
    }
}
