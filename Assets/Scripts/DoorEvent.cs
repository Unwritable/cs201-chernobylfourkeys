// Code written by Jamie Adaway, using tutorial by BMo
// Tutorial URL: https://www.youtube.com/watch?v=cLzG1HDcM4s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour
{
    public void OpenDoor()
    {
        this.transform.Rotate(new Vector3(0, -90, 0), Space.Self);

    }
}
