using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "EngineData", menuName = "Lachesis/Engine Data")]
    public class EngineData : ScriptableObject
    {
        public string description;
        public float thrustRate;
        public float fuelRate;
        public int minPrice;
        public int maxPrice;
    }
}