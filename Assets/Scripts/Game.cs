using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

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
        [SerializeField] public OreGen oreGen;

        public static Game instance;

        [Header("Runtime GameObjects")]
        [SerializeField] private Selectable selectedObject;

        [Header("Settings")]
        [SerializeField] private int planetItemCount = 6;
        [SerializeField] private int marketOreCount = 10;
        [SerializeField] private int marketUpgradeCount = 10;

        [SerializeField] private float solMarketTimer = 60.0f;
        [SerializeField] public float planetMarketTimerCountdown = 0.0f;

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
            UpdateMarketTimer();
        }

        private void UpdateMarketTimer()
        {
            if (gameState == GameStateEnum.PLAYING)
            {
                state.solTimer += Time.deltaTime;
                planetMarketTimerCountdown += Time.deltaTime;
            }

            if (planetMarketTimerCountdown >= solMarketTimer)
            {
                state.solTimer = 0;
                state.sol++;
                planetMarketTimerCountdown = 0;
                // Update market prices
                UpdateMarketPrices();
                UpdateDecay();
                universe.UpdateOres();
            }
        }

        private void UpdateDecay()
        {

        }

        private void UpdateMarketPrices()
        {
            for (int i = 0; i < state.planets.Count; i++)
            {
                state.planets[i].items = new List<PlanetItem>(oreGen.ores.Count);
                for (int j = 0; j < oreGen.ores.Count; j++)
                {
                    state.planets[i].items.Add(new PlanetItem()
                    {
                        type = "Ore",
                        name = oreGen.ores[j].name,
                        price = Random.Range(oreGen.ores[j].minPrice, oreGen.ores[j].maxPrice + 1),
                        qty = Random.value > 0.5 ? Random.Range(0, oreGen.ores[j].maxSellQty) : 0,
                    });
                }
            }
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
                state.playingState = PlayStateEnum.INTRO;
            }
            else
            {
                state = slots[currentSlot];
                LoadGame();
                state.playingState = PlayStateEnum.PLAY;
            }

            SaveGame();
        }

        public bool SellCargoItem(Planet planet)
        {
            var hold = state.selectedCargoHold;

            if (hold.count <= 0)
                return false;

            Logger.Log(name, "Finding planet item price", hold.cargoName, hold.cargoType);

            var item = planet.planetState.items.First(i => i.name == hold.cargoName && i.type == hold.cargoType);

            if(item == null)
                return false;

            var credits = item.price;

            state.credits += credits;
            hold.solTimer.RemoveAt(0);
            hold.count--;
            item.qty++;

            if(hold.count ==0)
            {
                hold.cargoType = "";
                hold.cargoName = "";
            }


            return true;
        }

        public bool BuySelectedItem()
        {
            var item = state.selectedPlanetItem;

            if (item.qty <= 0)
                return false;

            if (state.credits < item.price)
                return false;

            state.credits -= item.price;
            item.qty--;

            switch (item.type)
            {
                case "Ore":
                    AddOre(item.name, 1);
                    break;
            }

            return false;
        }



        // Probably should have its own class
        public void AddOre(string name, int count)
        {
            Logger.Log(this.name, "Add Ore", name, count.ToString());

            for (int i = 0; i < state.shipState.cargoHold.Count; i++)
            {
                if (count <= 0)
                    break;

                CargoHold hold = state.shipState.cargoHold.FirstOrDefault(h => h.cargoType == "Ore" && h.cargoName == name && h.count < h.maxCount);

                if (hold == null)
                {
                    Logger.Log(name, "no existing hold found");
                    hold = state.shipState.cargoHold.FirstOrDefault(h => h.cargoType == "");
                    hold.cargoType = "Ore";
                    hold.cargoName = name;
                    
                }

                int total = Mathf.Min(count, hold.maxCount - hold.count);
                for(int j = 0;j< count; j++)
                {
                    hold.solTimer.Add(state.sol);
                }
                hold.count += total;
                count -= total;
            }
        }

        public void NewGame()
        {
            state = slots[currentSlot] = new State();
            state.planets = universe.GenerateNewPlanets();
            state.ores = universe.GenerateNewOres();


            universe.SpawnPlanets(state.planets);
            universe.SpawnOres(state.ores);
            UpdateMarketPrices();
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

            if (selectable == null && selectedObject != null)
            {
                selectedObject.Leave();
            }
            else if (selectable != null)
            {
                selectable.Hover();
            }
            selectedObject = selectable;
        }

        public void SelectSelectable()
        {
            if (selectedObject != null)
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