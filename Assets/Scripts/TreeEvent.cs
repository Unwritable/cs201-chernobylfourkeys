// Code written by Jamie Adaway, using tutorial by BMo
// Tutorial URL: https://www.youtube.com/watch?v=cLzG1HDcM4s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeEvent : MonoBehaviour
{
    private Vector3 _fallenRot = new Vector3(-90f, 0f, 0f);
    [SerializeField, Range(0.1f, 100f)] protected float _tweenSpeed;

    public void FellTree()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOLocalRotate(_fallenRot, _tweenSpeed, RotateMode.Fast));

    }
}
