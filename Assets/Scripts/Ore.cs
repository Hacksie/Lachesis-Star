using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    public class Ore : MonoBehaviour
    {
        [Header("Runtime GameObjects")]
        [SerializeField] public OreState oreState;
        [Header("Configured Game Object")]
        [SerializeField] private SpriteRenderer spriteRenderer = null;
        [SerializeField] private Palette palette = null;
        [SerializeField] private Text worldText = null;

        // Start is called before the first frame update
        void Awake()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(oreState.touched)
            {
                spriteRenderer.gameObject.SetActive(false);
            }
            else
            {
                spriteRenderer.color = palette.GetColor(oreState.hue, oreState.colorValue);
                gameObject.transform.position = new Vector2(oreState.x, oreState.y);
                worldText.text = oreState.name + " Ore";
                gameObject.name = oreState.name;
                spriteRenderer.gameObject.SetActive(true);
            }
        }

        public void OnTriggerEnter2D(Collider2D collider)
        {
            Logger.Log(name, "Trigger");
            if (!oreState.touched)
            {
                if (Game.instance.AddOre(oreState.name, oreState.count))
                {
                    oreState.touched = true;
                }
            }
            
        }
    }
}
