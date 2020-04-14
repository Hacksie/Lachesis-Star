using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    [RequireComponent(typeof(Button))]
    public class MarketItem : MonoBehaviour
    {
        public PlanetItem planetItem;
        [Header("Configured Game Objects")]
        [SerializeField] private Text nameText;
        [SerializeField] private Text qtyText;
        [SerializeField] private Text priceText;

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();

        }
        public void Selected()
        {
            Logger.Log(name, "Selected");
            if (planetItem != null)
            {
                Game.instance.state.selectedPlanetItem = planetItem;
            }
            else
            {
                Logger.LogError(name, "No planetItem set");
            }
        }

        public void UpdatePanel(PlanetItem planetItem)
        {
            if(planetItem.qty == 0)
            {
                button.interactable = false;
            }
            else
            {
                button.interactable = true;
            }

            this.planetItem = planetItem;
            nameText.text = planetItem.name;
            qtyText.text = planetItem.qty.ToString(); ;
            priceText.text = "#" + planetItem.price;
        }
    }
}