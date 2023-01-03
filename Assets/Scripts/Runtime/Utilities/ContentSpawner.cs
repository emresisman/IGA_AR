using System;
using Unity.Mathematics;
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
            Instantiate(contentPrefab, new Vector3(2,1,-1), quaternion.identity);
            isSpawned = true;
        }
    }
}