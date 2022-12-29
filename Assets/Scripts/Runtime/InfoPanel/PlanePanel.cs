using Data.Plane;
using UnityEngine;
using TMPro;

namespace Runtime.InfoPanel
{
    public class PlanePanel : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text flightIata;
        [SerializeField]
        private TMP_Text depIata;
        [SerializeField]
        private TMP_Text arrIata;
        [SerializeField]
        private TMP_Text depTime;
        [SerializeField]
        private TMP_Text arrTime;
        [SerializeField]
        private TMP_Text depActual;
        [SerializeField]
        private TMP_Text arrEstimated;
        [SerializeField]
        private TMP_Text planeModel;

        public void SetText(FlightResponse flight)
        {
            flightIata.text = flight.Flight_Iata;
            depIata.text = flight.Dep_Iata;
            arrIata.text = flight.Arr_Iata;
            depTime.text = flight.Dep_Time_Utc + " UTC";
            arrTime.text = flight.Arr_Time_Utc + " UTC";
            depActual.text = flight.Dep_Actual_Utc + " UTC";
            arrEstimated.text = flight.Arr_Estimated_Utc + " UTC";
            planeModel.text = flight.Model;
        }
    }
}