using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "ShipData", menuName = "Lachesis/Ship Data")]
    public class ShipData : ScriptableObject
    {
        [SerializeField] public List<EngineData> engines;

        public EngineData GetEngine(string engineName)
        {
            return engines.Find(e => e.name == engineName);
        }
    }
}