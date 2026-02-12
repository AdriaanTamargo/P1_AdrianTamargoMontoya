using UnityEngine;

public class MovingPlatformClase : MonoBehaviour
{
    public float velocidad = 1f;           // Velocidad de la plataforma
    public GameObject objetoDestino;       // Objeto que marcara el destino de nuestra MovingPlatform

    private Rigidbody rb;
    private Vector3 posInicial;
    private Vector3 posFinal;
    private Vector3 siguientePosicion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        // Guardamos posición de inicio
        posInicial = transform.position;

        if (objetoDestino != null)
        {
            posFinal = objetoDestino.transform.position;
            siguientePosicion = posFinal;

            // Soltamos el destino
            objetoDestino.transform.parent = null;
        }
        else
        {
            Debug.LogError("Falta asignar el Objeto Destino en el Inspector!");
        }
    }

    void FixedUpdate()
    {
        if (objetoDestino == null) return;

        // --- 1. MOVIMIENTO DE LA PLATAFORMA ---
        Vector3 nuevaPosicion = Vector3.MoveTowards(transform.position, siguientePosicion, velocidad * Time.fixedDeltaTime);
        
        // Movemos el Rigidbody
        rb.MovePosition(nuevaPosicion);

        // --- 2. BUCLE DE MOVIMIENTO DE LA PLATAFORMA ---
        // Si la distancia al objetivo es casi cero
        if (Vector3.Distance(transform.position, siguientePosicion) < 0.1f)
        {
            // Cambiamos el destino
            if (siguientePosicion == posFinal)
            {
                siguientePosicion = posInicial;
            }
            else
            {
                siguientePosicion = posFinal;
            }
        }
    }

    // --- 3.PARTE PARA MANTENER AL JUGADOR ENCIMA DE LA MOVINGPLATFORM ---
    private void OnCollisionStay(Collision collision)
    {
        // Si choca el Jugador 
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hacemos al jugador HIJO de la plataforma para que se mueva con ella
            collision.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Si el jugador se baja o salta
        if (collision.gameObject.CompareTag("Player"))
        {
            // Lo liberamos
            collision.gameObject.transform.parent = null;
        }
    }
}