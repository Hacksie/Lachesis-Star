using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class BlackHole : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnTriggerEnter2D(Collider2D collider)
        {
            Logger.Log(name, "Trigger");
            Game.instance.SetPlayStateBlackHole();

        }
    }
}