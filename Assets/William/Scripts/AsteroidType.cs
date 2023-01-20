using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script created by William Isacsson
[CreateAssetMenu]
public class AsteroidType : ScriptableObject
{
  
    public enum AsteroidSize
    {
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public AsteroidSize asteroidSize;

    public Color asteroidColor;
    public float minForce;
    public float maxForce;
    public float minSize;
    public float maxSize;
    public float minTorque;
    public float maxTorque;
    public int damage;
    public int smallerPartsAmount;
}
