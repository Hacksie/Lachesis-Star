using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [CreateAssetMenu(fileName = "Palette", menuName = "Lachesis/Palette")]
    public class Palette : ScriptableObject
    {
        public int step = 4;
        public Color[] palette = new Color[64];
    }
}