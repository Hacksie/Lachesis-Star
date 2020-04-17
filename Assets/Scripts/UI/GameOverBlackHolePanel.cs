using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameOverBlackHolePanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Start is called before the first frame update
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.BLACKHOLE)
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
            Game.instance.state.success = true;
            Game.instance.SaveGame();
            Game.instance.menuState = MenuStateEnum.CREDITS;
            Game.instance.gameState = GameStateEnum.MAINMENU;
        }
    }
}