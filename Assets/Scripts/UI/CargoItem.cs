using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class CargoItem : MonoBehaviour
    {
        public CargoHold cargoHold;
        [Header("Configured Game Objects")]
        [SerializeField] OreGen oreGen = null;
        [SerializeField] Palette palette = null;
        [SerializeField] private Image frame;
        [SerializeField] private Text symbol;
        [SerializeField] private Image image;
        [SerializeField] private Text qty;
        [SerializeField] private Color selectedColor;

        void Update()
        {
            if (Game.instance.state.selectedCargoHold == cargoHold)
            {
                frame.color = selectedColor;
            }
            else
            {
                frame.color = Color.white;
            }
        }

        public void Selected()
        {
            Logger.Log(name, "Selected");
            if (cargoHold != null)
            {
                Game.instance.state.selectedCargoHold = cargoHold;
            }
            else
            {
                Logger.LogError(name, "No cargoHold set");
            }
        }

        public void UpdatePanel(CargoHold hold)
        {
            cargoHold = hold;

            switch (hold.cargoType)
            {
                case "Ore":
                    UpdateCargoHoldOre();
                    break;
                case "":
                default:
                    UpdateCargooHoldEmpty();
                    break;
            }
        }

        private void UpdateCargoHoldOre()
        {
            var ore = oreGen.GetOre(cargoHold.cargoName);
            image.sprite = ore.cargoSprite;
            image.color = palette.GetColor(ore.hue, ore.colorValue);
            symbol.text = ore.symbol;
            qty.text = cargoHold.count.ToString();
        }

        private void UpdateCargooHoldEmpty()
        {
            image.sprite = null;
            image.color = Color.black;
            symbol.text = "-";
            qty.text = "";
        }

    }
}