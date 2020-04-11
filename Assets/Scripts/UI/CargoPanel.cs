using UnityEngine;

namespace HackedDesign
{
    public class CargoPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;
        [SerializeField] GameObject cargoGroup0 = null;
        [SerializeField] GameObject cargoGroup1 = null;
        [SerializeField] GameObject cargoGroup2 = null;


        // Start is called before the first frame update
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.CARGO)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                UpdateCargoGroups();
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
            }
        }

        private void UpdateCargoGroups()
        {
            cargoGroup0.SetActive(Game.instance.state.cargoState.upgrades >= 0);
            cargoGroup1.SetActive(Game.instance.state.cargoState.upgrades >= 1);
            cargoGroup2.SetActive(Game.instance.state.cargoState.upgrades >= 2);

        }
    }
}
