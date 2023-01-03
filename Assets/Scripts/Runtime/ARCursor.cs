using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Runtime
{
    public class ARCursor : MonoBehaviour
    {
        public GameObject cursorChildObject;
        public GameObject objectToPlace;
        public ARRaycastManager raycastManager;
        public Camera arCamera;
    
        private bool isSpawned = false;

        private void Start()
        {
            cursorChildObject.SetActive(true);
        }

        private void Update()
        {
            if (isSpawned) return;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {

                Instantiate(objectToPlace, transform.position, Quaternion.identity);
                isSpawned = true;
                cursorChildObject.SetActive(false);
            }
            
            UpdateCursor();
        }

        private void UpdateCursor()
        {
            Vector2 screenPosition = arCamera.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
            var hits = new List<ARRaycastHit>();
            raycastManager.Raycast(screenPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes);

            if (hits.Count > 0)
            {
                transform.position = hits[0].pose.position;
            }
        }
    }
}
