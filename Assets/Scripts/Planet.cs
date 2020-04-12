using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace HackedDesign
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Planet : MonoBehaviour
    {

        [Header("Runtime GameObjects")]
        private SpriteRenderer spriteRenderer = null;
        [SerializeField] public PlanetState planetState;
        [Header("Configured Game Object")]
        [SerializeField] private Palette palette = null;
        [SerializeField] private Text worldText = null;
        [Header("Settings")]
        [SerializeField] private int pixelsPerUnit = 16;

        // Start is called before the first frame update
        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (worldText == null)
            {
                Logger.LogError(name, "worldText is null");
            }

        }

        public void UpdateText()
        {
            worldText.text = planetState.name;
        }

        public void GenerateSprite()
        {
            Texture2D sprite = new Texture2D(planetState.size, planetState.size);

            int[,] landMass = new int[planetState.size, planetState.size];

            for (int y = 0; y < planetState.size; y++)
            {
                for (int x = 0; x < planetState.size; x++)
                {
                    landMass[x, y] = Mathf.RoundToInt(Random.value);
                }
            }

            for (int y = 0; y < planetState.size; y++)
            {
                for (int x = 0; x < planetState.size; x++)
                {
                    if (x != 0 && y != 0 && x != planetState.size - 1 && y != planetState.size - 1)
                    {
                        float score = landMass[x, y] == 1 ? 1.0f : 0.0f;
                        score += landMass[x - 1, y - 1] == 1 ? 0.2f : -0.2f;
                        score += landMass[x, y - 1] == 1 ? 0.2f : -0.2f;
                        score += landMass[x + 1, y - 1] == 1 ? 0.2f : -0.2f;
                        score += landMass[x - 1, y] == 1 ? 0.2f : -0.2f;
                        score += landMass[x + 1, y] == 1 ? 0.2f : -0.2f;
                        score += landMass[x - 1, y + 1] == 1 ? 0.2f : -0.2f;
                        score += landMass[x, y + 1] == 1 ? 0.2f : -0.2f;
                        score += landMass[x + 1, y + 1] == 1 ? 0.2f : -0.2f;

                        score = Mathf.Clamp(score, 0, 1);
                        landMass[x, y] = Mathf.RoundToInt(score);
                    }

                    int value = Mathf.RoundToInt((new Vector2(x + 0.0f, y + 0.0f) - new Vector2(planetState.size / 3, planetState.size - (planetState.size / 3))).magnitude / planetState.size * palette.step);

                    if (landMass[x, y] == 1)
                    {
                        sprite.SetPixel(x, y, palette.GetColor(planetState.landHue, value));
                    }
                    else
                    {
                        sprite.SetPixel(x, y, palette.GetColor(planetState.waterHue, value));
                    }
                }
            }

            sprite.filterMode = FilterMode.Point;
            sprite.Apply();
            spriteRenderer.sprite = Sprite.Create(sprite, new Rect(0, 0, sprite.width, sprite.height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
        }
    }
}