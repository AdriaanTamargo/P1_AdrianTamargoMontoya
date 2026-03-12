using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            if (GameManagerClase.instancia != null)
            {
                GameManagerClase.instancia.ReiniciarPartida();
            }

            SceneManager.LoadScene("Victory");
        }
    }
}