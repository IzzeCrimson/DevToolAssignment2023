using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AstreoidType : ScriptableObject
{

    public Color color;
    public float minForce;
    public float maxForce;
    public float minSize;
    public float maxSize;
    public float minTorque;
    public float maxTorque;
}
