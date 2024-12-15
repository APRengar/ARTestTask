using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ApplicationQuit);
    }

    private void ApplicationQuit()
    {
        Application.Quit();
    }
}
