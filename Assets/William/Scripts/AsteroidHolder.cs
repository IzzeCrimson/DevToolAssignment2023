using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHolder : MonoBehaviour
{
    
    [SerializeField] private GameObject[] _serializeAsteroidPrefabs;
    public static GameObject[] asteroidPrefabs;

    private void Start()
    {
        asteroidPrefabs = _serializeAsteroidPrefabs;
    }
}
