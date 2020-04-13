using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HackedDesign
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] public SelectableType type = SelectableType.Planet;
        [SerializeField] private UnityEvent selectEvent = null;
        [SerializeField] private GameObject hoverIndicator = null;

        public Planet planet;
        public Ore ore;

        void Awake()
        {
            if(type==SelectableType.Blackhole)
            {

            }
            if(type == SelectableType.Planet)
            {
                planet = GetComponent<Planet>();
            }
            if(type == SelectableType.Ore)
            {
                ore = GetComponent<Ore>();
            }
            if(type == SelectableType.Ship)
            {

            }

            Leave();
        }


        public string GetDescription()
        {
            switch(type)
            {
                case SelectableType.Planet:
                    return planet.planetState.name;
            }

            return "";
        }

        public void Hover()
        {
            if(hoverIndicator != null)
            {
                hoverIndicator.SetActive(true);
            }
        }

        public void Leave()
        {
            if (hoverIndicator != null)
            {
                hoverIndicator.SetActive(false);
            }
        }

        public void Select()
        {
            selectEvent.Invoke();
        }
    }

    public enum SelectableType
    {
        Planet,
        Ore,
        Blackhole,
        Ship
    }
}