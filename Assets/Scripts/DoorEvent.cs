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

    public void OpenDoor()
    {
        if(!isOpen)
        {
            var sequence = DOTween.Sequence();
            sequence.Append(this.transform.DOLocalRotate(_openPosition, 3f, RotateMode.Fast));
            isOpen = true;
        } else
        {
            var sequence = DOTween.Sequence();
            sequence.Append(this.transform.DOLocalRotate(_closePosition, 3f, RotateMode.Fast));
            isOpen= false;
        }


    }
}
