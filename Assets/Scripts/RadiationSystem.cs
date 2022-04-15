// Code written by Jamie Adaway

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class RadiationSystem : MonoBehaviour
{
    public TMPro.TextMeshProUGUI _radText;

    public float _currentRads { get; private set; } = 0.0f;
    public float _currentRadsIncrease { get; private set; } = 0.0f;

    [SerializeField, Range(0.5f, 25)] private float _baseRadsIncrease = 5.0f;
    [SerializeField, Range(1, 10)] private float _radsScalingFactor = 1.0f;
    [SerializeField, Range(1000, 100000)] private float _maxRads = 1000.0f; // 100k is roughly the point at which a human being has taken too much radiation, and will pass out and die within the hour
    [SerializeField] private float _minRadDistance = 1.0f;
    [SerializeField] private float _maxRadDistance = 10.0f;

    private float CheckDistance(Collider col)
    {
        float ret = Vector3.Distance(col.transform.position, this.transform.position);
        if (ret > _maxRadDistance)
            return _maxRadDistance;
        else if (ret <= _minRadDistance)
            return _minRadDistance;

        return ret;
    }

    private void OnTriggerStay(Collider col)
    {
        if(col.CompareTag("RadSource"))
        {
            _currentRadsIncrease = (_baseRadsIncrease / CheckDistance(col)) * _radsScalingFactor;
            _currentRads += _currentRadsIncrease;
            _radText.text = "Current Rads: " + _currentRads.ToString("F2") + "(+" + _currentRadsIncrease.ToString("F2") + ")";
        }
    }

    private void OnTriggerExit(Collider col)
    {
        _radText.text = "Current Rads: " + _currentRads.ToString("F2") + "(+0.0)";
    }
}
