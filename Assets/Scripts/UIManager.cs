using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    private GameObject _player;

    [field: SerializeField] private TMPro.TextMeshProUGUI _radText;
    [field : SerializeField] private TMPro.TextMeshProUGUI _interactText;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.Log("UIManager :: Error: _instance is NULL.");

            return _instance;
        }
    }

    public void SetRadText(string changeText)
    {
        _radText.text = changeText;
    }
    public void EnableDisableInteractText( bool makeActive)
    {
        _interactText.gameObject.SetActive(makeActive);
    }

    public void ModifyInteractText(string stringToAdd)
    {

    }

    private void Awake()
    {
        _instance = this;
        Debug.Log("UIManager :: _instance awake.");
    }

    private void Start()
    {
        // Get a reference to the player ready for distance checks
        _player = GameObject.FindGameObjectWithTag("Player");
    }
}

