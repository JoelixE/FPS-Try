using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Impact System/Spawn Object Effect", fileName = "SpawnObjectEffect")]

public class SpawnObjectEffect : ScriptableObject
{
    public GameObject Prefab;
    public float Probability = 1.0f;
    public bool RandomizeRotation;
    [Tooltip("Zero values will lock the rotation on the axis. Values up to 360 are sensible for each X, Y, Z")]
    public Vector3 RandomizeRotationMultiplier = Vector3.zero;
}
