using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Runtime.Routes
{
    public class RouteManager : Singleton<RouteManager>
    {
        [SerializeField] private List<Route> landingRoutes = new List<Route>();
        [SerializeField] private List<Route> takeOffRoutes = new List<Route>();

        public Route GetAvailableLandingRoute()
        {
            var available = landingRoutes.Where(x => x.isUsing == false).ToList();
            var index = Random.Range(0, available.Count());
            return available[index];
        }        
        
        public Route GetAvailableTakeOffRoute()
        {
            var available = takeOffRoutes.Where(x => x.isUsing == false).ToList();
            var index = Random.Range(0, available.Count());
            return available[index];
        }
    }
}