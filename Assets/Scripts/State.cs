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
        public CargoState cargoState;
        public PlayerState playerState;

        public State()
        {
            startDate = System.DateTime.Now.ToString("s", System.Globalization.CultureInfo.InvariantCulture);
            cargoState = new CargoState();
            playerState = new PlayerState();
        }

        public string GetDescription()
        {
            if (started)
            {
                return "Last Saved: " + lastSaveDate + "\nCredits: #" + credits;
            }
            else
            {
                return "Empty Slot";
            }
        }

        public void ToggleCargo()
        {
            if (playingState == PlayStateEnum.PLAY)
            {
                playingState = PlayStateEnum.CARGO;
            }
            else if (playingState == PlayStateEnum.CARGO)
            {
                playingState = PlayStateEnum.PLAY;
            }
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
    public class CargoState
    {
        public int upgrades = 0;
    }

    [System.Serializable]
    public class PlanetState
    {

    }

    public enum GameStateEnum
    {
        MAINMENU,
        PLAYING,
    }

    public enum PlayStateEnum
    {
        PLAY,
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