using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterPrototypeBtn : MonoBehaviour
{
    public void LoadPrototype()
    {
        SceneManager.LoadScene(1); // Prototype scene is number 1 in Scene list
    }
}
