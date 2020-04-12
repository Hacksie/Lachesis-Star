using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [CreateAssetMenu(fileName = "PlanetNameGen", menuName = "Lachesis/Planet Name Gen")]
    public class PlanetNameGen : ScriptableObject
    {
        [SerializeField] private float prefixChance = 0.1f;
        [SerializeField] private float suffixChance = 0.1f;
        [SerializeField] private List<string> prefixes = null;
        [SerializeField] private List<string> stems = null;
        [SerializeField] private List<string> suffixes = null;

        public string GenerateName()
        {
            string name = "";
            if(Random.value < prefixChance)
            {
                name += prefixes[Random.Range(0, prefixes.Count)];
            }

            int pos = Random.Range(0, stems.Count);
            name += stems[pos];
            
            

            if (Random.value < suffixChance)
            {
                name += suffixes[Random.Range(0, suffixes.Count)];
            }

            return name;
        }
    }
}