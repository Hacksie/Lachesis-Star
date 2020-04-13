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
        [SerializeField] private int selectedSlot = 0;

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
                canvasGroup.blocksRaycasts = true;
                UpdatePanels();
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                startPanel.blocksRaycasts = false;
                optionsPanel.blocksRaycasts = false;
                creditsPanel.blocksRaycasts = false;
                startPanel.interactable = false;
                optionsPanel.interactable = false;
                creditsPanel.interactable = false;
            }
        }

        void UpdatePanels()
        {
            switch (Game.instance.menuState)
            {
                case MenuStateEnum.NONE:
                    startPanel.alpha = 0;
                    optionsPanel.alpha = 0;
                    creditsPanel.alpha = 0;
                    startPanel.blocksRaycasts = false;
                    optionsPanel.blocksRaycasts = false;
                    creditsPanel.blocksRaycasts = false;
                    startPanel.interactable = false;
                    optionsPanel.interactable = false;
                    creditsPanel.interactable = false;
                    break;
                case MenuStateEnum.START:
                    startPanel.alpha = 1;
                    optionsPanel.alpha = 0;
                    creditsPanel.alpha = 0;
                    startPanel.blocksRaycasts = true;
                    optionsPanel.blocksRaycasts = false;
                    creditsPanel.blocksRaycasts = false;
                    startPanel.interactable = true;
                    optionsPanel.interactable = false;
                    creditsPanel.interactable = false;
                    UpdateSlots();
                    break;
                case MenuStateEnum.OPTIONS:
                    startPanel.alpha = 0;
                    optionsPanel.alpha = 1;
                    creditsPanel.alpha = 0;
                    startPanel.blocksRaycasts = false;
                    optionsPanel.blocksRaycasts = true;
                    creditsPanel.blocksRaycasts = false;
                    startPanel.interactable = false;
                    optionsPanel.interactable = true;
                    creditsPanel.interactable = false;
                    break;
                case MenuStateEnum.CREDITS:
                    startPanel.alpha = 0;
                    optionsPanel.alpha = 0;
                    creditsPanel.alpha = 1;
                    startPanel.blocksRaycasts = false;
                    optionsPanel.blocksRaycasts = false;
                    creditsPanel.blocksRaycasts = true;
                    startPanel.interactable = false;
                    optionsPanel.interactable = false;
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

        public void OptionsClicked()
        {
            Logger.Log(name, "Start Clicked");
            Game.instance.menuState = MenuStateEnum.OPTIONS;
        }

        public void CreditsClicked()
        {
            Logger.Log(name, "Start Clicked");
            Game.instance.menuState = MenuStateEnum.CREDITS;
        }

        public void QuitClicked()
        {
            Logger.Log(name, "Quit Clicked");
            Game.instance.menuState = MenuStateEnum.QUIT;
            Game.instance.Quit();
        }

        public void Slot0Clicked()
        {
            Logger.Log(name, "Slot 0 Clicked");
            selectedSlot = 0;
        }

        public void Slot1Clicked()
        {
            Logger.Log(name, "Slot 1 Clicked");
            selectedSlot = 1;
        }

        public void Slot2Clicked()
        {
            Logger.Log(name, "Slot 2 Clicked");
            selectedSlot = 2;
        }

        public void StartGameClicked()
        {
            Logger.Log(name, "Start Game Clicked");
            Game.instance.menuState = MenuStateEnum.NONE;
            UpdatePanels();
            Game.instance.StartGame(selectedSlot);
        }

        public void DeleteSlotClicked()
        {
            Logger.Log(name, "Delete Slot Clicked ", selectedSlot.ToString());
            Game.instance.DeleteSaveGame(selectedSlot);
            UpdateSlots();
        }

        public void CreditsCloseClicked()
        {
            Logger.Log(name, "Credits Close");
            Game.instance.menuState = MenuStateEnum.NONE;
            UpdatePanels();
        }
    }
}
