using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace HackedDesign
{
    public class Game : MonoBehaviour
    {
        [Header("State")]
        public GameStateEnum gameState;
        public MenuStateEnum menuState;
        [SerializeField] public List<State> slots = new List<State>(3);
        [SerializeField] public int currentSlot = 0;
        [SerializeField] public State state = null;
        [Header("Game Objects")]
        [SerializeField] public PlayerController player;

        public static Game instance;

        

        public Game()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            var slotFiles = LoadSlots();
            foreach(var f in slotFiles)
            {
                if(f.Contains("SaveFile0.json"))
                {

                }
                if (f.Contains("SaveFile1.json"))
                {

                }
                if (f.Contains("SaveFile2.json"))
                {

                }
                Logger.Log(name, f);

            }

            //state = slots[currentSlot];
            //state.gameState = GameStateEnum.PLAYING;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTime();
        }

        public string[] LoadSlots()
        {
            return Directory.GetFiles(Application.persistentDataPath,"SaveFile*.json");
        }

        public void SaveGame()
        {
            state.Save(currentSlot);
        }

        public void StartGame(int slot)
        {
            currentSlot = slot;
            gameState = GameStateEnum.PLAYING;
            if(!state.started)
            {
                NewGame();
            }

            SaveGame();
        }



        public void NewGame()
        {
            state = slots[currentSlot] = new State();
            state.started = true;
        }

        public void LoadGame()
        {

        }

        private void UpdateTime()
        {
            switch(gameState)
            {
                case GameStateEnum.MAINMENU:
                    Time.timeScale = 0;
                    break;
                case GameStateEnum.PLAYING:
                    switch(state.playingState)
                    {
                        case PlayStateEnum.CARGO:
                        case PlayStateEnum.END:
                            Time.timeScale = 0;
                            break;
                        case PlayStateEnum.PLAY:
                            Time.timeScale = 1;
                            break;
                    }
                    break;
            }
        }




    }
}