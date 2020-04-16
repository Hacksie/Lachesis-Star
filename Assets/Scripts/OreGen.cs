using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
            /*
            var converted = new List<Items<string>>(initial.Count);
            var sum = 0.0;
            foreach (var item in initial.Take(initial.Count - 1))
            {
                sum += item.Probability;
                converted.Add(new Items<string> { Probability = sum, Item = item.Item });
            }
            converted.Add(new Items<string> { Probability = 1.0, Item = initial.Last().Item });

            var rnd = new Random();
while (true)
{
    var probability = rnd.NextDouble();
    var selected = converted.SkipWhile(i => i.Probability < probability).First();
    Console.WriteLine($"Selected item = {selected.Item}");
}
            */

            var probList = new List<OreProb>(ores.Count);
            float sum = 0.0f;
            foreach(var item in ores.Take(ores.Count - 1))
            {
                sum += item.probability;
                probList.Add(new OreProb()
                {
                    ore = item,
                    probability = sum
                });
            }

            probList.Add(new OreProb()
            {
                ore = ores.Last(),
                probability = 1.0f
            });

            float prob = Random.value;

            return probList.SkipWhile(i => i.probability < prob).First().ore;
        }

    }

    public class OreProb
    {
        public OreData ore;
        public float probability;

    }
}

