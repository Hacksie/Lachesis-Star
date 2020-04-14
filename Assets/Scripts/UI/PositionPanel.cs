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
        [SerializeField] Text SpeedText = null;
        [SerializeField] Text TimerText = null;
        [SerializeField] PlayerController player = null;
        
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
            if(SpeedText == null)
            {
                Logger.LogError(name, "SpeedText is null");
            }
            if (player == null)
            {
                Logger.LogError(name, "player is null");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.PLAY)
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
        }

        private void UpdatePanel()
        {
            XText.text = $"{player.transform.position.x:N1}";
            YText.text = $"{player.transform.position.y:N1}";
            SpeedText.text = $"{player.currentThrust:N1} / {player.maxThrust:N1}";
            TimerText.text = Game.instance.planetMarketTimerCountdown.ToString("N0");
        }
    }
}
