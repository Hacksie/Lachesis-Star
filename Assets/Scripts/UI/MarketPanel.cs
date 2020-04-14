using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
        [SerializeField] private Text creditsText;
        [SerializeField] private Text repText;
        [SerializeField] private GameObject itemParent;

        [SerializeField] ShipData shipData = null;
        [SerializeField] OreGen oreGen = null;
        [SerializeField] Palette palette = null;
        [SerializeField] GameObject cargoGroup0 = null;
        [SerializeField] GameObject cargoGroup1 = null;
        [SerializeField] GameObject cargoGroup2 = null;
        [SerializeField] List<Image> cargoImages = null;
        [SerializeField] List<Text> cargoTexts = null;
        [SerializeField] int selectedEngine = 0;

        [Header("Prefabs")]
        [SerializeField] private GameObject itemPrefab;

        private int selectedItem = 0;

        private bool dirty = true;


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
                if (dirty)
                {
                    canvasGroup.alpha = 1;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    UpdatePanel();
                    UpdateCargoGroups();
                    UpdateCargoHolds();
                    dirty = false;
                }
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                dirty = true;
            }
        }

        public void UpdatePanel()
        {

            creditsText.text = "#" + Game.instance.state.credits.ToString();
            Selectable s = Game.instance.GetSelectable();
            if (s.type != SelectableType.Planet)
            {
                Logger.LogError(name, "Cannot open a market with non planet");
                return;
            }
            var p = s.planet;

            if (p == null)
            {
                Logger.LogError(name, "Selectable of type planet has null planet");
                return;
            }

            title.text = p.planetState.name + " Trading Market";
            repText.text = p.planetState.reputation.ToString();
            //description.text = p.planetState.description;
            UpdateItems(p);
        }

        void UpdateItems(Planet planet)
        {
            Logger.Log(name, "items count", planet.planetState.items.Count.ToString());

            foreach (var planetItem in planet.planetState.items.Where(e => e.qty > 0))
            {
                var item = Instantiate(itemPrefab, itemParent.transform);
                var marketItem = item.GetComponent<MarketItem>();
                marketItem.UpdatePanel(planetItem);
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
            for (int i = 0; i < cargoImages.Count; i++)
            {
                CargoHold hold = Game.instance.state.shipState.cargoHold[i];

                if (hold == null || hold.count == 0)
                {
                    UpdateCargoHoldEmpty(i);
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

        private void UpdateCargoHoldEmpty(int holdIndex)
        {
            cargoImages[holdIndex].sprite = null;
            cargoImages[holdIndex].color = Color.black;
            cargoTexts[holdIndex].text = "-";
        }

        public void BuyClicked()
        {
            Logger.Log(name, "Buy", Game.instance.state.selectedPlanetItem.name);
            //Game.instance.state.selectedPlanetItem = planetItem;
        }

        public void SellClicked()
        {

        }

        public void CloseClicked()
        {

            Game.instance.SetPlayStatePlay();
        }
    }
}