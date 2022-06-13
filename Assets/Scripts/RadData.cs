using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadData : MonoBehaviour
{
    [field:SerializeField, Range(5f, 25f)] public float RadDist { get; private set; }
}
