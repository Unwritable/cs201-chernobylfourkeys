using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationManager : MonoBehaviour
{
    private static RadiationManager _instance;
    private float _currentRads { get; set; } = 0.0f;
    [field : SerializeField, Range(1000, 100000)] private float _maxRads { get; set; }
    [field: SerializeField, Range(2, 25)] private float _baseRadsIncrease { get; set; }
    [field : SerializeField, Range(1, 10)] private float _RadsIncreaseScalingFactor { get; set; }

    private List<GameObject> _radSources = new List<GameObject>();



    public static RadiationManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Might be better as a Debug.Log that just returns the current _instance
                Debug.Log("RadiationManager :: Error: _instance is NULL.");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _radSources.AddRange(GameObject.FindGameObjectsWithTag("RadSource"));
        foreach(GameObject rs in _radSources)
        {
            Debug.Log($"RadiationManager :: AddRadSourcesCheck: {rs.name}");
        }
    }

    // ADD RADIATION CODE HERE, INCLUDING THE COROUTINE AND POSSIBLY UPDATE METHOD
}
