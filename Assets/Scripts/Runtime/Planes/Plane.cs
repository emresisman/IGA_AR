using Data.Plane;
using Runtime.InfoPanel;
using UnityEngine;

namespace Runtime.Planes
{
    public class Plane : MonoBehaviour
    {
        [SerializeField]
        private PlanePanel myPanel;

        public void SetTextPlanePanel(FlightResponse flight)
        {
            myPanel.SetPlaneInfoText(flight);
        }
    }
}