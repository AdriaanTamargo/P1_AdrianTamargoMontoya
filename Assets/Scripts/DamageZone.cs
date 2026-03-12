using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerController jugador = other.GetComponentInParent<PlayerController>();

        if (jugador != null)
        {
            if (GameManagerClase.instancia != null)
            {
                GameManagerClase.instancia.LoseLife();
            }
            jugador.Respawn();
        }
    }
}