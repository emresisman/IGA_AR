using System;
using Data.Plane;
using Runtime.InfoPanel;
using Runtime.Utilities;
using UnityEngine;

namespace Runtime.Planes
{
    public class Plane : MonoBehaviour
    {
        private PlanePanel myPanel;
        private PlaneMovement myMovement;
        private FlightResponse myFlightInfo;

        private void Start()
        {
            myPanel = PanelManager.Instance.GetPlanePanel();
        }

        private void Awake()
        {
            myMovement = this.gameObject.GetComponent<PlaneMovement>();
        }

        public void SetFlight(FlightResponse flight, FlightDirection direction)
        {
            myFlightInfo = flight;
            myMovement.StartFlight(direction);
        }

        public void SetText()
        {
            myPanel.SetText(myFlightInfo);
        }
    }
}