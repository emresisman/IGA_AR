using System;
using Data.Plane;
using Runtime.InfoPanel;
using UnityEngine;

namespace Runtime.Planes
{
    public class Plane : MonoBehaviour
    {
        private PlanePanel myPanel;
        private FlightResponse myFlightInfo;

        private void Start()
        {
            myPanel = PanelManager.Instance.GetPlanePanel();
        }

        public void SetFlight(FlightResponse flight)
        {
            myFlightInfo = flight;
        }

        public void SetText()
        {
            myPanel.SetText(myFlightInfo);
        }
    }
}