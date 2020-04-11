using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HackedDesign
{
    public class Planet : MonoBehaviour
    {
        [SerializeField] private new string name; 
        [SerializeField] private PlanetNameGen nameGen = null;
        [SerializeField] private Text worldText = null;

        // Start is called before the first frame update
        void Awake()
        {
            if (nameGen == null)
            {
                Logger.LogError(name, "nameGen is null");
            }
            if (worldText == null)
            {
                Logger.LogError(name, "worldText is null");
            }

            SetName();
        }

        void SetName()
        {
            name = nameGen.GetName();
            worldText.text = name;
        }
    }
}