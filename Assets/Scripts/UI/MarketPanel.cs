using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MarketPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;
        [Header("Configured GameObjects")]
        [SerializeField] private Text title;
        [SerializeField] private Text comms;
        [SerializeField] private Image speaker;
        

        // Start is called before the first frame update
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.MARKET)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                UpdatePanel();
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        public void UpdatePanel()
        {
            Selectable s = Game.instance.GetSelectable();
            if(s.type != SelectableType.Planet)
            {
                Logger.LogError(name, "Cannot open a market with non planet");
                return;
            }
            var p = s.planet;

            if(p== null)
            {
                Logger.LogError(name, "Selectable of type planet has null planet");
                return;
            }

            title.text = p.planetState.name + " Trading Market";
            //description.text = p.planetState.description;
            
        }

        public void CloseClicked()
        {
            Game.instance.SetPlayStatePlay();
        }
    }
}