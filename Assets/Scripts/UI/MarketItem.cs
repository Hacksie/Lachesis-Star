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
        [SerializeField] private Image frame = null;
        [SerializeField] private Color selectedColor;
        [SerializeField] private Text nameText = null;
        [SerializeField] private Text qtyText = null;
        [SerializeField] private Text priceText = null;

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

        public void Update()
        {
            if(Game.instance.state.selectedPlanetItem == planetItem)
            {
                frame.color = selectedColor;
            }
            else
            {
                frame.color = Color.white;
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
            nameText.text = planetItem.name + " " + planetItem.description;
            qtyText.text = planetItem.qty.ToString();
            priceText.text = "$" + planetItem.price;
        }
    }
}