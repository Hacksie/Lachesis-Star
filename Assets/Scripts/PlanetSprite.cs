using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PlanetSprite : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        [SerializeField] private Palette palette;
        [SerializeField] private int width = 32;
        [SerializeField] private int height = 32;
        [SerializeField] private int pixelsPerUnit = 16;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            GenerateSprite();
        }

        void GenerateSprite()
        {
            Texture2D sprite = new Texture2D(width, height);

            int landColor = Random.Range(0, 13);
            int waterColor = Random.Range(0, 13);

            //float landColor = Random.Range(0.0f, 1.0f);
            //float waterColor = Random.Range(0.0f, 1.0f);

            bool[,] landMass = new bool[width, height];

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    landMass[x, y] = Random.value >= 0.5;
                }
            }

            for (int y = 0; y < width; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (x != 0 && y != 0 && x != width - 1 && y != height - 1)
                    {
                        float score = landMass[x, y] ? 1.0f : 0.0f;
                        score += landMass[x - 1, y - 1] ? 0.2f : -0.2f;
                        score += landMass[x, y - 1] ? 0.2f : -0.2f;
                        score += landMass[x + 1, y - 1] ? 0.2f : -0.2f;
                        score += landMass[x - 1, y] ? 0.2f : -0.2f;
                        score += landMass[x + 1, y] ? 0.2f : -0.2f;
                        score += landMass[x - 1, y + 1] ? 0.2f : -0.2f;
                        score += landMass[x, y + 1] ? 0.2f : -0.2f;
                        score += landMass[x + 1, y + 1] ? 0.2f : -0.2f;

                        score = Mathf.Clamp(score, 0, 1);
                        landMass[x, y] = score > 0.5f;
                    }

                    int dist = Mathf.RoundToInt((new Vector2(x + 0.0f, y + 0.0f) - new Vector2(width / 4, height - (height / 3))).magnitude / width * 4);
                    
                    if (landMass[x,y])
                    {
                        //sprite.SetPixel(x, y, Random.ColorHSV(landColor, landColor + 0.1f, 0.3f, 0.35f, 0.9f - dist, 1f - dist));
                        sprite.SetPixel(x, y, palette.palette[landColor * 4 + dist]);
                    }
                    else
                    {
                        sprite.SetPixel(x, y, palette.palette[waterColor * 4 + dist]);
                        //sprite.SetPixel(x, y, Random.ColorHSV(waterColor, waterColor + 0.1f, 0.65f, 0.7f, 0.9f - dist, 1f - dist));
                    }
                }
            }

            sprite.filterMode = FilterMode.Point;
            sprite.Apply();
            spriteRenderer.sprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }

    }

}