using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "UpgradeManager", menuName = "Infinity Star Blues/Upgrade Manager")]
    public class UpgradeManager : ScriptableObject
    {
        public List<CargoUpgradeData> upgradesCargo;
        public List<EngineData> upgradesEngines;
        public List<EngineData> engines;
    }
}