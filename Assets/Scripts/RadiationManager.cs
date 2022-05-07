using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationManager : MonoBehaviour
{
    private static RadiationManager _instance;
    private GameObject _player;
    private GameObject _currentClosestRadSource;
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
        // Get a reference to the player ready for distance checks
        _player = GameObject.FindGameObjectWithTag("Player");

        // Find and add the radiation sources we will be checking the distance from the player on
        _radSources.AddRange(GameObject.FindGameObjectsWithTag("RadSource"));
        foreach(GameObject rs in _radSources)
        {
            Debug.Log($"RadiationManager :: AddRadSources Check: {rs.name}");
        }


    }

    private GameObject CheckDistance(GameObject _player, List<GameObject> _radSources)
    {
        GameObject ret = null;

        // Set this to a arbitrarily high value, as we want to get the radSource if it is closer
        float distMax = 1000;

        foreach(GameObject gm in _radSources)
        {
            float dist = Vector3.Distance(gm.transform.position, _player.transform.position);
            if (dist < distMax)
            {
                distMax = dist;
                ret = gm;
            }
        }

        return ret;
    }
    

}
