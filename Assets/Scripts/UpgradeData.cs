using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace HackedDesign
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "UpgradeData", menuName = "Lachesis/Upgrade Data")]
    public class UpgradeData : ScriptableObject
    {
        public string description;
        //public string name;
        public int price;

    }
}
