using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{

    public class MainMenuPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;
        [SerializeField] private CanvasGroup startPanel = null;
        [SerializeField] private CanvasGroup optionsPanel = null;
        [SerializeField] private CanvasGroup creditsPanel = null;
        [SerializeField] private Text slot0desc = null;
        [SerializeField] private Text slot1desc = null;
        [SerializeField] private Text slot2desc = null;

        // Start is called before the first frame update
        void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.MAINMENU)
            {
                canvasGroup.alpha = 1;
                canvasGroup.interactable = true;
                UpdatePanels();
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
            }
        }

        void UpdatePanels()
        {
            switch (Game.instance.menuState)
            {
                case MenuStateEnum.NONE:
                    startPanel.alpha = 0;
                    //startPanel.interactable = false;
                    optionsPanel.alpha = 0;
                    //optionsPanel.interactable = false;
                    creditsPanel.alpha = 0;
                    //creditsPanel.interactable = false;
                    break;
                case MenuStateEnum.START:
                    startPanel.alpha = 1;
                    //startPanel.interactable = true;
                    optionsPanel.alpha = 0;
                    //optionsPanel.interactable = false;
                    creditsPanel.alpha = 0;
                    //creditsPanel.interactable = false;
                    UpdateSlots();
                    break;
                case MenuStateEnum.OPTIONS:
                    startPanel.alpha = 0;
                    //startPanel.interactable = false;
                    optionsPanel.alpha = 1;
                    //optionsPanel.interactable = true;
                    creditsPanel.alpha = 0;
                    //creditsPanel.interactable = false;
                    break;
                case MenuStateEnum.CREDITS:
                    startPanel.alpha = 0;
                    startPanel.interactable = false;
                    optionsPanel.alpha = 0;
                    optionsPanel.interactable = false;
                    creditsPanel.alpha = 1;
                    creditsPanel.interactable = true;
                    break;
            }
        }

        void UpdateSlots()
        {
            slot0desc.text = Game.instance.slots[0] != null ? Game.instance.slots[0].GetDescription() : "Empty Slot";
            slot1desc.text = Game.instance.slots[1] != null ? Game.instance.slots[1].GetDescription() : "Empty Slot";
            slot2desc.text = Game.instance.slots[2] != null ? Game.instance.slots[2].GetDescription() : "Empty Slot";
        }

        public void StartClicked()
        {
            Logger.Log(name, "Start Clicked");
            Game.instance.menuState = MenuStateEnum.START;
        }

        public void Slot0Clicked()
        {
            Logger.Log(name, "Slot 0 Clicked");
            Game.instance.menuState = MenuStateEnum.NONE;
            UpdatePanels();
            Game.instance.StartGame(0);
        }

        public void Slot1Clicked()
        {
            Logger.Log(name, "Slot 1 Clicked");
            Game.instance.menuState = MenuStateEnum.NONE;
            UpdatePanels();
            Game.instance.StartGame(1);
        }

        public void Slot2Clicked()
        {
            Logger.Log(name, "Slot 2 Clicked"); 
            Game.instance.menuState = MenuStateEnum.NONE;
            UpdatePanels();
            Game.instance.StartGame(2);
        }
    }
}
