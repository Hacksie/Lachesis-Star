using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HackedDesign
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "Infinity Star Blues/Upgrade Data")]
    public class UpgradeData : ScriptableObject
    {
        public string description;
        public string upgradeId;
        //public string name;
        public int minPrice;
        public int maxPrice;
    }
}
