using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationManager : MonoBehaviour
{
    private static RadiationManager _instance;
    private GameObject _player;

    // DEBUG
    public TMPro.TextMeshProUGUI _radText;
    WaitForSeconds _radUpdateTimer;

    private float _CurrentRads { get; set; } = 0.0f;
    private float _CurrentRadsIncrease { get; set; } = 0.0f;
    [field : SerializeField, Range(1000, 100000)] private float _MaxRads { get; set; }
    [field : SerializeField, Range(5, 25)] private float _MaxRadSourceDist { get; set; }
    [field: SerializeField, Range(2, 25)] private float _BaseRadsIncrease { get; set; }
    [field : SerializeField, Range(25, 100)] private float _MaxRadsIncrease { get; set; }
    [field: SerializeField, Range(0.1f, 5)] private float _RadUpdateTime { get; set; }

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


    private GameObject CheckDistance(GameObject _player, List<GameObject> _radSources)
    {
        GameObject ret = null;

        // Set this to a arbitrarily high value, as we want to get the radSource if it is closer
        float distMax = 1000;

        foreach (GameObject gm in _radSources)
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

    private IEnumerator RadCalc()
    {
        while(true)
        {
            GameObject _closestRadSource = CheckDistance(_player, _radSources);
            float dist = Vector3.Distance(_closestRadSource.transform.position, _player.transform.position);
            if (dist <= _MaxRadSourceDist)
            {
                _CurrentRadsIncrease = _MaxRadsIncrease / dist;
            }
            else
            {
                _CurrentRadsIncrease = _BaseRadsIncrease;
            }

            yield return _radUpdateTimer;

            _CurrentRads += _CurrentRadsIncrease;
            _radText.text = "Current Rads: " + _CurrentRads.ToString("F2") + "(+" + _CurrentRadsIncrease.ToString("F2") + ")";
        }

    }



    private void Start()
    {
        // Get a reference to the player ready for distance checks
        _player = GameObject.FindGameObjectWithTag("Player");

        // Find and add the radiation sources we will be checking the distance from the player on
        _radSources.AddRange(GameObject.FindGameObjectsWithTag("RadSource"));

        _radUpdateTimer = new WaitForSeconds(_RadUpdateTime);

        StartCoroutine(RadCalc());
    }


}
