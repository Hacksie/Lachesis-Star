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
        [Header("Configurated GameObjects")]
        [SerializeField] public PlayerController player;
        [SerializeField] public Universe universe;

        public static Game instance;

        [Header("Runtime GameObjects")]
        [SerializeField] private Selectable selectedObject;

        public Game()
        {
            instance = this;
        }

        void Awake()
        {
            LoadSlots();
        }

        State LoadSaveFile(int slot)
        {
            var path = Path.Combine(Application.persistentDataPath, $"SaveFile{slot}.json");
            Logger.Log(name, "Attempting to load ", path);
            if (File.Exists(path))
            {
                Logger.Log(name, "Loading ", path);
                var contents = File.ReadAllText(path);
                return JsonUtility.FromJson<State>(contents);
            }
            else
            {
                Logger.Log(name, "Save file does not exist ", path);
            }

            return null;
        }

        // Update is called once per frame
        void Update()
        {
            UpdateTime();
        }

        public void LoadSlots()
        {
            slots[0] = LoadSaveFile(0);
            slots[1] = LoadSaveFile(1);
            slots[2] = LoadSaveFile(2);
        }

        public void SaveGame()
        {
            state.Save(currentSlot);
        }

        public void DeleteSaveGame(int slot)
        {
            File.Delete(Path.Combine(Application.persistentDataPath, $"SaveFile{slot}.json"));
            slots[slot] = null;
        }

        public void StartGame(int slot)
        {
            currentSlot = slot;
            gameState = GameStateEnum.PLAYING;
            if (slots[currentSlot] == null || !slots[currentSlot].started)
            {
                NewGame();
            }
            else
            {
                state = slots[currentSlot];
                LoadGame();
            }

            SaveGame();
        }



        public void NewGame()
        {
            state = slots[currentSlot] = new State();
            state.planets = universe.GenerateNewPlanets();
            state.ores = universe.GenerateNewOres();
            
            
            universe.SpawnPlanets(state.planets);
            universe.SpawnOres(state.ores);
            state.started = true;
        }

        public void LoadGame()
        {
            universe.SpawnPlanets(state.planets);
        }

        public void SetPlayStatePlay()
        {
            SaveGame();
            state.playingState = PlayStateEnum.PLAY;
        }

        public void SetPlayStateMap()
        {
            SaveGame();
            state.playingState = PlayStateEnum.MAP;
        }

        public void SetPlayStateMarket()
        {
            SaveGame();
            state.playingState = PlayStateEnum.MARKET;

        }

        public void SetPlayStateCargo()
        {
            SaveGame();
            state.playingState = PlayStateEnum.CARGO;
        }

        public void ToggleCargo()
        {
            if (state.playingState == PlayStateEnum.PLAY)
            {
                SaveGame();
                state.playingState = PlayStateEnum.CARGO;
            }
            else if (state.playingState == PlayStateEnum.CARGO)
            {
                SaveGame();
                state.playingState = PlayStateEnum.PLAY;
            }
        }

        public void ToggleMap()
        {
            if (state.playingState == PlayStateEnum.PLAY)
            {
                SaveGame();
                state.playingState = PlayStateEnum.MAP;
            }
            else if (state.playingState == PlayStateEnum.MAP)
            {
                SaveGame();
                state.playingState = PlayStateEnum.PLAY;
            }
        }

        public void QuitPlaying()
        {
            SaveGame();
            gameState = GameStateEnum.MAINMENU;
        }

        public void Quit()
        {
            Logger.Log(name, "Quit");
            Application.Quit();
        }

        //public void SetHoverPlanet(Planet planet)
        //{
        //    hoverPlanet = planet;
        //}

        public void SetSelectable(Selectable selectable)
        {
            
            if(selectable == null && selectedObject != null)
            {
                selectedObject.Leave();
            }
            else if(selectable != null)
            {
                selectable.Hover();
            }
            selectedObject = selectable;
        }

        public void SelectSelectable()
        {
            if(selectedObject != null)
            {
                selectedObject.Select();
            }
        }

        public Selectable GetSelectable()
        {
            return selectedObject;
        }

        private void UpdateTime()
        {
            switch (gameState)
            {
                case GameStateEnum.MAINMENU:
                    Time.timeScale = 0;
                    break;
                case GameStateEnum.PLAYING:
                    switch (state.playingState)
                    {
                        case PlayStateEnum.INTRO:
                        case PlayStateEnum.CARGO:
                        case PlayStateEnum.MAP:
                        case PlayStateEnum.MARKET:
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