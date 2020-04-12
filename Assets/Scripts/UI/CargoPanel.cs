using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace HackedDesign
{
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
        [SerializeField] int selectedHold = 0;
        [SerializeField] int selectedEngine = 0;


        [Header("Cargo Frame")]
        [SerializeField] Text cargoHoldText = null;
        [SerializeField] Text cargoItemText = null;
        [SerializeField] Image cargiItemImage = null;
        [SerializeField] Text cargoItemTypeText = null;
        [SerializeField] Text cargoMinPriceText = null;
        [SerializeField] Text cargoMaxPriceText = null;

        // Start is called before the first frame update
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            if(shipData == null)
            {
                Logger.LogError(name, "shipData is null");
            }
            if(oreGen == null)
            {
                Logger.LogError(name, "oreGen is null");
            }
            if(palette == null)
            {
                Logger.LogError(name, "palette is null");
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.CARGO)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                UpdateCargoGroups();
                UpdateCargoHolds();
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
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
            for(int i=0; i< cargoImages.Count;i++)
            {
                CargoHold hold = Game.instance.state.shipState.cargoHold[i];

                if (hold == null || hold.count == 0)
                {
                    UpdateCargoHoldEmpty(i, hold);
                }
                else
                {
                    switch (hold.cargoType)
                    {
                        case "Ore":
                            UpdateCargoHoldOre(i, hold);
                            break;
                    }
                }
            }
        }

        private void UpdateCargoHoldOre(int holdIndex, CargoHold hold)
        {
            var ore = oreGen.GetOre(hold.cargoName);
            cargoImages[holdIndex].sprite = ore.cargoSprite;
            cargoImages[holdIndex].color = palette.GetColor(ore.hue, ore.colorValue);
            cargoTexts[holdIndex].text = ore.symbol;
        }

        private void UpdateCargoHoldEmpty(int holdIndex, CargoHold hold)
        {
            cargoImages[holdIndex].sprite = null;
            cargoImages[holdIndex].color = Color.black;
            cargoTexts[holdIndex].text = "-";
        }

        private void UpdateShipSelected()
        {

        }

        public void CloseClicked()
        {
            Game.instance.SetPlayStatePlay();
        }

        public void Cargo1Clicked()
        {
            Logger.Log(name, "Cargo 1 Clicked");
        }

        public void Cargo2Clicked()
        {
            Logger.Log(name, "Cargo 2 Clicked");
        }

        public void Cargo3Clicked()
        {
            Logger.Log(name, "Cargo 3 Clicked");
        }

        public void Cargo4Clicked()
        {
            Logger.Log(name, "Cargo 4 Clicked");
        }

        public void Cargo5Clicked()
        {
            Logger.Log(name, "Cargo 5 Clicked");
        }

        public void Cargo6Clicked()
        {
            Logger.Log(name, "Cargo 6 Clicked");
        }

        public void Cargo7Clicked()
        {
            Logger.Log(name, "Cargo 7 Clicked");
        }

        public void Cargo8Clicked()
        {
            Logger.Log(name, "Cargo 8 Clicked");
        }

        public void Cargo9Clicked()
        {
            Logger.Log(name, "Cargo 9 Clicked");
        }

        public void Cargo10Clicked()
        {
            Logger.Log(name, "Cargo 10 Clicked");
        }

        public void Cargo11Clicked()
        {
            Logger.Log(name, "Cargo 11 Clicked");
        }

        public void Cargo12Clicked()
        {
            Logger.Log(name, "Cargo 12 Clicked");
        }

        public void Cargo13Clicked()
        {
            Logger.Log(name, "Cargo 13 Clicked");
        }

        public void Cargo14Clicked()
        {
            Logger.Log(name, "Cargo 14 Clicked");
        }

        public void Cargo15Clicked()
        {
            Logger.Log(name, "Cargo 15 Clicked");
        }

        public void Cargo16Clicked()
        {
            Logger.Log(name, "Cargo 16 Clicked");
        }


        public void Engine1Clicked()
        {
            Logger.Log(name, "Engine 1 Clicked");
        }

        public void Engine2Clicked()
        {
            Logger.Log(name, "Engine 2 Clicked");
        }

        
    }
}
