using UnityEngine;

namespace Runtime.Planes
{
    public class PlaneSO : ScriptableObject
    {
        public Routes Routes { get; private set; }
        public string CallSign { get; private set; }
        public string DepartureAirport { get; private set; }
        public string ArrivalAirport { get; private set; }
    }
}