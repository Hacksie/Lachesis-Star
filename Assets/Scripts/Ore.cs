using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Ore : MonoBehaviour
    {
        [Header("Runtime GameObjects")]
        private SpriteRenderer spriteRenderer = null;
        [SerializeField] public OreState oreState;
        [Header("Configured Game Object")]
        [SerializeField] private Palette palette = null;
        //[SerializeField] private Text worldText = null;

        // Start is called before the first frame update
        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void UpdateSprite()
        {
            spriteRenderer.color = palette.GetColor(oreState.hue,oreState.colorValue);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
