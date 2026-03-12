using System; 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerClase : MonoBehaviour
{
    static public GameManagerClase instancia;

    public event Action<int> OnMonedasChanged;
    public event Action<int> OnVidasChanged; 

    public int monedas = 0;
    [SerializeField] public int lives = 3; 

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void AddMoneda()
    {
        monedas++;
        Debug.Log("Monedas: " + monedas);
        OnMonedasChanged?.Invoke(monedas);
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log("Vidas restantes: " + lives);
        OnVidasChanged?.Invoke(lives);

        if (lives <= 0)
        {
            Debug.Log("¡Game Over! Cargando pantalla de Game Over...");
            ReiniciarPartida(); 
            SceneManager.LoadScene("GameOver"); 
        }
    }

    public void ReiniciarPartida()
    {
        monedas = 0;
        lives = 3;
    }
}