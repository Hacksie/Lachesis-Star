﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Universe : MonoBehaviour
    {
        [Header("Configurable Game Objects")]
        [SerializeField] Rect rect;
        [SerializeField] GameObject universeParent = null;
        [SerializeField] private OreGen oreGen = null;
        [SerializeField] private PlanetNameGen nameGen = null;
        [SerializeField] private Palette palette = null;

        [Header("Prefabs")]
        [SerializeField] GameObject orePrefab = null;
        [SerializeField] GameObject planet16 = null;
        [SerializeField] GameObject planet32 = null;
        [SerializeField] GameObject planet64 = null;
        [SerializeField] GameObject planet128 = null;

        [Header("Settings")]
        [SerializeField] int planetCount16 = 0;
        [SerializeField] int planetCount32 = 0;
        [SerializeField] int planetCount64 = 0;
        [SerializeField] int planetCount128 = 0;
        [SerializeField] int oreCount = 0;
        
        void Awake()
        {
            //SpawnPlanets();
        }

        public List<OreState> GenerateNewOres()
        {
            var ores = new List<OreState>();

            for (int i = 0; i < oreCount; i++)
            {
                var ore = oreGen.GetRandomOre();

                ores.Add(new OreState()
                {
                    name = ore.name,
                    hue = ore.hue,
                    colorValue = ore.colorValue,
                    size = 8,
                    x = Mathf.RoundToInt(Random.Range(rect.xMin, rect.xMax)),
                    y = Mathf.RoundToInt(Random.Range(rect.yMin, rect.yMax))
                });
            }

            return ores;
        }

        public List<PlanetState> GenerateNewPlanets()
        {
            var planets = new List<PlanetState>();

            for (int i = 0; i < planetCount16; i++)
            {
                planets.Add(GenPlanet(16));
            }

            for (int i = 0; i < planetCount32; i++)
            {
                planets.Add(GenPlanet(32));
            }

            for (int i = 0; i < planetCount64; i++)
            {
                planets.Add(GenPlanet(64));
            }

            for (int i = 0; i < planetCount128; i++)
            {
                planets.Add(GenPlanet(128));
            }

            return planets;
        }

        public PlanetState GenPlanet(int size) => new PlanetState()
        {
            name = nameGen.GenerateName(),
            size = size,
            landHue = Random.Range(0, palette.hues),
            waterHue = Random.Range(0, palette.hues),
            x = Mathf.RoundToInt(Random.Range(rect.xMin, rect.xMax)),
            y = Mathf.RoundToInt(Random.Range(rect.yMin, rect.yMax))
        };

        public void SpawnPlanet(PlanetState state)
        {
            GameObject prefab;
            switch(state.size)
            {
                case 128:
                    prefab = planet128;
                    break;
                case 64:
                    prefab = planet64;
                    break;
                case 32:
                    prefab = planet32;
                    break;
                case 16:
                default:
                    prefab = planet16;
                    break;
            }

            var gameObject = Instantiate(prefab, new Vector2(state.x, state.y), Quaternion.identity, universeParent.transform);
            gameObject.name = state.name;

            Planet p = gameObject.GetComponent<Planet>();
            p.planetState = state;
            p.UpdateText();
            p.GenerateSprite();
        }


        public void SpawnPlanets(List<PlanetState> planets)
        {
            planets.ForEach(s => SpawnPlanet(s));
        }

        public void SpawnOre(OreState state)
        {
            var gameObject = Instantiate(orePrefab, new Vector2(state.x, state.y), Quaternion.identity, universeParent.transform);
            gameObject.name = state.name;
            Ore o = gameObject.GetComponent<Ore>();
            o.oreState = state;
            o.UpdateSprite();
            
        }

        public void SpawnOres(List<OreState> ores)
        {
            ores.ForEach(s => SpawnOre(s));
        }
    }
}