// Code written by Jamie Adaway, using tutorial by BMo
// Tutorial URL: https://www.youtube.com/watch?v=cLzG1HDcM4s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InteractWithObject : MonoBehaviour
{
    public bool withinRange;
    public KeyCode interactKey;
    public UnityEvent interactAction;


    void Update()
    {
        if (withinRange)
        {
            if(Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
                UIManager.Instance.EnableDisableInteractText(false);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("InteractWithObject :: Trigger entered.");
        if(col.gameObject.CompareTag("Player"))
        {
            withinRange = true;
            UIManager.Instance.EnableDisableInteractText(withinRange);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("InteractWithObject :: Trigger exited.");
        if (col.gameObject.CompareTag("Player"))
        {
            withinRange = false;
            UIManager.Instance.EnableDisableInteractText(withinRange);
        }
    }
}
