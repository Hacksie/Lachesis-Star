using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace HackedDesign
{
    [System.Serializable]
    public class State
    {
        public string startDate;
        public string lastSaveDate;
        public bool started = false;
        public int credits = 100;
        public PlayStateEnum playingState;
        public ShipState shipState;
        public PlayerState playerState;
        public List<PlanetState> planets;
        public List<OreState> ores;
        public PlanetItem selectedPlanetItem;
        public CargoHold selectedCargoHold;
        public int sol;
        public float solTimer;

        

        public State()
        {
            startDate = System.DateTime.Now.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            shipState = new ShipState();
            playerState = new PlayerState();
            sol = 0;
        }

        public string GetDescription()
        {
            return started ? $"Last:{lastSaveDate}\nCredit:${credits}" : "Empty Slot";
        }

        public void Save(int slot)
        {
            Logger.Log("State", "Saving state");
            lastSaveDate = System.DateTime.Now.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            playerState.UpdatePlayerPosition();
            string json = JsonUtility.ToJson(this);
            string path = Path.Combine(Application.persistentDataPath, $"SaveFile{slot}.json");
            File.WriteAllText(path, json);
            Logger.Log("State", "Saved ", path);
        }

        
    }

    [System.Serializable]
    public class PlayerState
    {
        public float playerPositionX;
        public float playerPositionY;

        public void UpdatePlayerPosition()
        {
            playerPositionX = Game.instance.player.transform.position.x;
            playerPositionY = Game.instance.player.transform.position.y;
        }
    }

    [System.Serializable]
    public class ShipState
    {
        public int cargoUpgrades = 0;
        public float fuel = 0;
        public string[] engines = new string[2];
        public List<CargoHold> cargoHold; 
        public ShipState()
        {
            engines[0] = "Ion Thruster";
            engines[1] = "Ion Thruster";
            cargoHold = new List<CargoHold>(16);
            for(int i=0; i < 16; i++)
            {
                cargoHold.Add(new CargoHold()
                {
                    holdId = i + 1,
                    cargoType = "",
                    cargoName = "",
                    count = 0,
                    maxCount = 5
                });
            }
        }
    }

    [System.Serializable]
    public class PlanetState
    {
        public string name;
        public string description;
        public int size;
        public int landHue;
        public int waterHue;
        public int x;
        public int y;
        public int reputation = 0;
        public List<PlanetItem> items;
    }

    [System.Serializable]
    public class PlanetItem
    {
        public string name;
        public string type;
        public int qty;
        public int price;
    }

    [System.Serializable]
    public class OreState
    {
        public string name;
        public int hue;
        public int count;
        public int colorValue;
        public int size;
        public int x;
        public int y;
        public bool touched = false;
    }

    [System.Serializable]
    public class Engine
    {
        public string name;
    }

    [System.Serializable]
    public class CargoHold
    {
        public int holdId = 0;
        public string cargoType;
        public string cargoName;
        public bool radProof;
        public bool orgProof;
        public int count;
        public int maxCount = 5;
        public List<int> solTimer;
    }

    public enum GameStateEnum
    {
        MAINMENU,
        PLAYING,
    }

    public enum PlayStateEnum
    {
        PLAY,
        INTRO,
        CARGO,
        MAP,
        MARKET,
        END
    }

    public enum MenuStateEnum
    {
        NONE,
        START,
        OPTIONS,
        CREDITS,
        QUIT
    }
}