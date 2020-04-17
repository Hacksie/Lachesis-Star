using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MapPanel : MonoBehaviour
    {

        [SerializeField] private Universe universe = null;

        [SerializeField] private GameObject pixelPrefab = null;
        [SerializeField] private GameObject parent = null;
        [SerializeField] private Palette palette = null;

        [SerializeField] private GameObject shipProxy = null;
        private List<GameObject> pool = new List<GameObject>();

        private bool dirty = true;
        private CanvasGroup canvasGroup = null;

        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            CreatePool();
        }

        private void CreatePool()
        {
            for(int i=0; i<universe.planetCount16;i++)
            {
                var go = Instantiate(pixelPrefab, parent.transform);
                pool.Add(go);
            }
            for (int i = 0; i < universe.planetCount32; i++)
            {
                var go = Instantiate(pixelPrefab, parent.transform);
                pool.Add(go);
            }
            for (int i = 0; i < universe.planetCount64; i++)
            {
                var go = Instantiate(pixelPrefab, parent.transform);
                pool.Add(go);
            }
            for (int i = 0; i < universe.planetCount128; i++)
            {
                var go = Instantiate(pixelPrefab, parent.transform);
                pool.Add(go);
            }

        }

        private void UpdateMap()
        {
            var srt = shipProxy.GetComponent<RectTransform>();
            var ppos = new Vector2(Mathf.RoundToInt(Game.instance.player.transform.position.x / 16), Mathf.RoundToInt(Game.instance.player.transform.position.y / 16));
            srt.anchoredPosition = ppos;


            for(int i=0; i< Game.instance.state.planets.Count;i++)
            {
                var rt = pool[i].GetComponent<RectTransform>();
                var pos = new Vector2(Mathf.RoundToInt(Game.instance.state.planets[i].x / 16), Mathf.RoundToInt(Game.instance.state.planets[i].y / 16));

                var image = rt.gameObject.GetComponent<Image>();
                image.color = palette.GetColor(Game.instance.state.planets[i].landHue, 1);

                rt.anchoredPosition = pos;
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (Game.instance.gameState == GameStateEnum.PLAYING && Game.instance.state.playingState == PlayStateEnum.MAP)
            {
                if (dirty)
                {
                    canvasGroup.alpha = 1;
                    canvasGroup.interactable = true;
                    canvasGroup.blocksRaycasts = true;
                    UpdateMap();
                    dirty = false;
                }
            }
            else
            {
                canvasGroup.alpha = 0;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
                dirty = true;
            }
        }

        public void CloseClicked()
        {
            Game.instance.SetPlayStatePlay();
        }
    }
}