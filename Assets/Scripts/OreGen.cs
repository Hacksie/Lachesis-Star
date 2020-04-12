using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "OreGen", menuName = "Lachesis/Ore Gen")]
    public class OreGen : ScriptableObject
    {
        public List<OreData> ores;

        public OreData GetOre(string name)
        {
            return ores.Find(o => o.name == name);
        }

        public OreData GetRandomOre()
        {
            return ores[Random.Range(0, ores.Count)];
        }

    }
}

