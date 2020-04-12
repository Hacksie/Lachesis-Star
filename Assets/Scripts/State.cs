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

        public State()
        {
            startDate = System.DateTime.Now.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            shipState = new ShipState();
            playerState = new PlayerState();
        }

        public string GetDescription()
        {
            return started ? $"Last:{lastSaveDate}\nCredit:#{credits}" : "Empty Slot";
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
        public int size;
        public int landHue;
        public int waterHue;
        public int x;
        public int y;
    }

    [System.Serializable]
    public class OreState
    {
        public string name;
        public int hue;
        public int colorValue;
        public int size;
        public int x;
        public int y;
    }

    [System.Serializable]
    public class Engine
    {
        public string name;
    }

    [System.Serializable]
    public class CargoHold
    {
        public string cargoType;
        public string cargoName;
        public int count;
        public int maxCount = 5;
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