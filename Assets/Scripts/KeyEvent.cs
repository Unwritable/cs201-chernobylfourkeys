// Code written by Jamie Adaway, using tutorial by BMo
// Tutorial URL: https://www.youtube.com/watch?v=cLzG1HDcM4s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEvent : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI _keyText;
    public void CollectKey() // This is temporary, and will be better implemented when the switch to the GameManager system happens
    {
        _keyText.text = "Keys Held: 1";
        Destroy(this.gameObject);
    }
}
