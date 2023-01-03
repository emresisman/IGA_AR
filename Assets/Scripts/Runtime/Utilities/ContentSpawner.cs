using UnityEngine;

namespace Runtime.Utilities
{
    public class ContentSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject contentPrefab;
        private bool isSpawned = false;
        
        private void Update()
        {
            if (Input.touchCount <= 0 || isSpawned) return;
            Instantiate(contentPrefab, new Vector3(-8,-5,7), Quaternion.identity);
            isSpawned = true;
        }
    }
}