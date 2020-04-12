using UnityEngine;

namespace HackedDesign
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "OreData", menuName = "Lachesis/Ore Data")]
    public class OreData : ScriptableObject
    {
        public string symbol;
        public OreType oreType;
        public int hue;
        public int colorValue;
        public int minPrice;
        public int maxPrice;
        public float rarity;
        public Sprite cargoSprite;
    }

    public enum OreType
    {
        Basic,
        Metallic,
        Organic,
        RadIso
    }
}

