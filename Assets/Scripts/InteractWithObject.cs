// Code written by Jamie Adaway, using tutorial by BMo
// Tutorial URL: https://www.youtube.com/watch?v=cLzG1HDcM4s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWithObject : MonoBehaviour
{
    public bool withinRange;
    [SerializeField] TMPro.TextMeshProUGUI _interactText;
    public KeyCode interactKey;
    public UnityEvent interactAction;

    void Start()
    {
        
    }


    void Update()
    {
        if (withinRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                _interactText.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            withinRange = true;
            _interactText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            withinRange = false;
            _interactText.gameObject.SetActive(false);
        }
    }
}
