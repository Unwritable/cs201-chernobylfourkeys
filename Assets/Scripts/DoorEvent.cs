// Code written by Jamie Adaway, using tutorial by BMo
// Tutorial URL: https://www.youtube.com/watch?v=cLzG1HDcM4s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorEvent : MonoBehaviour
{
    private bool isOpen;
    private Vector3 _openPosition = new Vector3(0, -90, 0);
    private Vector3 _closePosition = new Vector3(0, 0, 0);

    [SerializeField, Range(0.1f, 100f)] protected float _tweenSpeed;

    public void OpenDoor()
    {
        if(!isOpen)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(this.transform.DOLocalRotate(_openPosition, _tweenSpeed, RotateMode.Fast));
            isOpen = true;
        } else
        {
            var sequence = DOTween.Sequence();
            sequence.Append(this.transform.DOLocalRotate(_closePosition, _tweenSpeed, RotateMode.Fast));
            isOpen= false;
        }


    }
}
