using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleSceneManager : MonoBehaviour
{
    public void OnButtonClick(int sceneToLoadIndex)
    {
        SceneManager.LoadScene(sceneToLoadIndex);
    }
}
