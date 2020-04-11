using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class Universe : MonoBehaviour
    {
        [SerializeField] Rect rect;
        [SerializeField] GameObject universeParent = null;

        [SerializeField] GameObject planet16 = null;
        [SerializeField] GameObject planet32 = null;
        [SerializeField] GameObject planet64 = null;
        [SerializeField] GameObject planet128 = null;

        [SerializeField] int planetCount16 = 0;
        [SerializeField] int planetCount32 = 0;
        [SerializeField] int planetCount64 = 0;
        [SerializeField] int planetCount128 = 0;

        void Awake()
        {
            //SpawnPlanets();
        }

        public void SpawnPlanets()
        {
            for(int i=0;i<planetCount16;i++)
            {
                float x = Random.Range(rect.xMin, rect.xMax);
                float y = Random.Range(rect.yMin, rect.yMax);
                Instantiate(planet16, new Vector2(x, y), Quaternion.identity, universeParent.transform);
            }
            for (int i = 0; i < planetCount32; i++)
            {
                float x = Random.Range(rect.xMin, rect.xMax);
                float y = Random.Range(rect.yMin, rect.yMax);
                Instantiate(planet32, new Vector2(x, y), Quaternion.identity, universeParent.transform);
            }
            for (int i = 0; i < planetCount64; i++)
            {
                float x = Random.Range(rect.xMin, rect.xMax);
                float y = Random.Range(rect.yMin, rect.yMax);
                Instantiate(planet64, new Vector2(x, y), Quaternion.identity, universeParent.transform);
            }
            for (int i = 0; i < planetCount128; i++)
            {
                float x = Random.Range(rect.xMin, rect.xMax);
                float y = Random.Range(rect.yMin, rect.yMax);
                Instantiate(planet128, new Vector2(x, y), Quaternion.identity, universeParent.transform);
            }
        }
    }
}