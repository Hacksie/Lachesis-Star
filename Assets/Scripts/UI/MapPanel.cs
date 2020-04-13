using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HackedDesign
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MapPanel : MonoBehaviour
    {
        private CanvasGroup canvasGroup = null;
        [SerializeField] private Universe universe;

        [SerializeField] private GameObject pixelPrefab;
        [SerializeField] private GameObject parent;
        [SerializeField] private Palette palette;

        private GameObject shipProxy = null;
        private List<GameObject> pool = new List<GameObject>();

        private bool dirty = true;

        // Start is called before the first frame update
        void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            shipProxy = Instantiate(pixelPrefab, parent.transform);
            var s = shipProxy.GetComponent<Image>();
            s.color = palette.GetColor(13, 1);
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
            srt.anchoredPosition = new Vector2(Mathf.RoundToInt(Game.instance.player.transform.position.x / 16), Mathf.RoundToInt(Game.instance.player.transform.position.y / 16));


            for(int i=0; i< Game.instance.state.planets.Count;i++)
            {
                //Vector2 worldPos = Game.instance.state.planets[i].
                var rt = pool[i].GetComponent<RectTransform>();
                var pos = new Vector2(Mathf.RoundToInt(Game.instance.state.planets[i].x / 16), Mathf.RoundToInt(Game.instance.state.planets[i].y / 16));
                Logger.Log(name, pos.ToString());

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