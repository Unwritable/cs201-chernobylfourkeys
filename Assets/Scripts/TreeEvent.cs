// Code written by Jamie Adaway, using tutorial by BMo
// Tutorial URL: https://www.youtube.com/watch?v=cLzG1HDcM4s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TreeEvent : MonoBehaviour
{
    [SerializeField, Range(0.1f, 100f)] protected float _tweenSpeed;
    [SerializeField, Range(0.1f, 100f)] protected float _deathTime;

    public void FellTree()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(this.transform.DOLocalMoveY(this.transform.position.y - 2f, _tweenSpeed));
        sequence.Append(this.transform.DOLocalMoveY(this.transform.position.y - 10f, 0f).OnComplete(DestroyOnComplete));
    }

    public void DestroyOnComplete()
    {
        Destroy(this.gameObject, _deathTime);
    }
}
