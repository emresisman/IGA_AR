using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Runtime
{
    public class ARCursor : MonoBehaviour
    {
        [SerializeField] private GameObject cursorChildObject;
        [SerializeField] private GameObject objectToPlace;
        [SerializeField] private ARRaycastManager raycastManager;
        [SerializeField] private ARPlaneManager planeManager;
        [SerializeField] private Camera arCamera;
    
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
                Instantiate(objectToPlace, SpawnPosition(), Quaternion.identity);
                isSpawned = true;
                cursorChildObject.SetActive(false);
                planeManager.planePrefab.SetActive(false);
                planeManager.enabled = false;
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

        private Vector3 SpawnPosition()
        {
            return transform.position + new Vector3(0, -1, 1); 
        }
    }
}
