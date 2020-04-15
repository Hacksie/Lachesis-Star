using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace HackedDesign
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CargoPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;
        [Header("Configured Game Objects")]
        [SerializeField] ShipData shipData = null;
        [SerializeField] OreGen oreGen = null;
        [SerializeField] Palette palette = null;
        [SerializeField] GameObject cargoGroup0 = null;
        [SerializeField] GameObject cargoGroup1 = null;
        [SerializeField] GameObject cargoGroup2 = null;
        [SerializeField] List<Image> cargoImages = null;
        [SerializeField] List<Text> cargoTexts = null;
        [SerializeField] List<CargoItem> cargoItems = null;
        [SerializeField] int selectedEngine = 0;


        [Header("Cargo Frame")]
        [SerializeField] Text cargoHoldText = null;
        [SerializeField] Text cargoItemText = null;
        [SerializeField] Image cargoItemImage = null;
        [SerializeField] Text cargoItemTypeText = null;
        [SerializeField] Text cargoMinPriceText = null;
        [SerializeField] Text cargoMaxPriceText = null;

        [Header("Engine Frame")]
        [SerializeField] Text engineText = null;
        [SerializeField] Text engineNameText = null;
        [SerializeField] Text engineImage = null;
        [SerializeField] Text engineLevelText = null;
        [SerializeField] Text engineFuelText = null;
        [SerializeField] Text engineMaxThrust = null;

        bool dirty = true;

        // Start is called before the first frame update
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if (shipData == null)
            {
                Logger.LogError(name, "shipData is null");
            }
            if (oreGen == null)
            {
                Logger.LogError(name, "oreGen is null");
            }
            if (palette == null)
            {
                Logger.LogError(name, "palette is null");
            }

            UpdateShipSelected();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.CARGO)
            {
                if (dirty)
                {
                    canvasGroup.alpha = 1;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    UpdateCargoGroups();
                    UpdateCargoHolds();
                    dirty = false;
                }

                UpdateShipSelected();
            }
            else
            {
                dirty = true;
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        private void UpdateCargoGroups()
        {
            cargoGroup0.SetActive(Game.instance.state.shipState.cargoUpgrades >= 0);
            cargoGroup1.SetActive(Game.instance.state.shipState.cargoUpgrades >= 1);
            cargoGroup2.SetActive(Game.instance.state.shipState.cargoUpgrades >= 2);
        }

        private void UpdateCargoHolds()
        {
            for (int i = 0; i < cargoItems.Count; i++)
            {
                CargoHold hold = Game.instance.state.shipState.cargoHold[i];
                //cargoItems[i].cargoHold = hold;
                cargoItems[i].UpdatePanel(hold);
            }
        }

        private void UpdateShipSelected()
        {
            if (Game.instance.state.selectedCargoHold == null)
            {
                UpdateShipSelectedEmpty();

                return;
            }

            //Game.instance.state.selectedCargoHold = Game.instance.state.shipState.cargoHold[selected - 1]; 
            
            switch (Game.instance.state.selectedCargoHold.cargoType)
            {
                case "Ore":
                    UpdateShipSelectedOre(Game.instance.state.selectedCargoHold);
                    break;
                default:
                    UpdateShipSelectedEmpty();
                    break;
            }
        }

        private void UpdateShipSelectedEmpty()
        {
            cargoHoldText.text = "Hold " + (Game.instance.state.selectedCargoHold == null ? "" : Game.instance.state.selectedCargoHold.holdId.ToString()) + " ()";
            cargoItemText.text = "";
            cargoItemImage.sprite = null;
            cargoItemImage.color = Color.black;
            cargoItemTypeText.text = "Empty";
            cargoMaxPriceText.text = "";
            cargoMinPriceText.text = "";
        }

        private void UpdateShipSelectedOre(CargoHold hold)
        {

            var ore = oreGen.GetOre(hold.cargoName);
            cargoHoldText.text = "Hold " + hold.holdId + " (" + (hold.orgProof ? "+O" : "") + (hold.radProof ? " +R" : "") + ")";
            cargoItemText.text = ore.name;
            cargoItemImage.sprite = ore.cargoSprite;
            cargoItemImage.color = palette.GetColor(ore.hue, ore.colorValue);
            cargoItemTypeText.text = hold.cargoType;
        }

        private void UpdateEngineSelected(int selected)
        {

        }

        public void CloseClicked()
        {
            Game.instance.SetPlayStatePlay();
        }

        public void Engine1Clicked() => UpdateEngineSelected(1);
        public void Engine2Clicked() => UpdateEngineSelected(2);


    }
}
