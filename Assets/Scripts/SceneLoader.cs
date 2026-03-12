using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        if (GameManagerClase.instancia != null)
        {
            GameManagerClase.instancia.ReiniciarPartida();
        }

        

        SceneManager.LoadScene(sceneName);
    }
}