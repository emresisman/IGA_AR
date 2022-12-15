using UnityEngine;

namespace Runtime.Planes
{
    public class Plane : MonoBehaviour
    {
        [SerializeField]
        private PlaneSO planeObject;

        public PlaneSO PlaneObject => planeObject;

        public void SetPlaneObject(PlaneSO planeObj)
        {
            planeObject = planeObj;
        }
    }
}