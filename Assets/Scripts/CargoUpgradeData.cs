using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HackedDesign
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "CargoUpgrade", menuName = "Infinity Star Blues/Cargo Upgrade Data")]
    public class CargoUpgradeData : ScriptableObject
    {
        public int level;
        public int minPrice;
        public int maxPrice;
    }
}