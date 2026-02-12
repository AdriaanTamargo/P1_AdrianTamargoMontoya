using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float tiempoRetardo = 2.0f;     // Tiempo antes de empezar a caer
    public float velocidadCaida = 5f;      // Velocidad al bajar
    public float distanciaCaida = 5f;      // Cuánto va a bajar
    public float tiempoEsperaAbajo = 2f;   // Tiempo que espera antes de subir
    public float velocidadSubida = 5f;     // Velocidad al volver a su sitio

    // Estados internos
    private Vector3 posInicial;
    private Vector3 posFinal;
    private bool estaActivada = false;     // Ya la han pisado?
    private bool estaCayendo = false;      // Está bajando?
    private bool estaReseteando = false;   // Está subiendo?
    private float momentoActivacion;       

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posInicial = transform.position;
        // Calculamos la posición final (de la de ahora a la de abajo)
        posFinal = posInicial + Vector3.down * distanciaCaida; 
    }

    void FixedUpdate()
    {
        // 1. LÓGICA DE CUENTA ATRÁS
        if (estaActivada && !estaCayendo && !estaReseteando)
        {
            float tiempoPasado = Time.time - momentoActivacion;

            if (tiempoPasado >= tiempoRetardo)
            {
                estaCayendo = true; // Empieza a caer
            }
        }

        // 2. LÓGICA DE CAÍDA
        if (estaCayendo)
        {
            Vector3 nuevaPos = Vector3.MoveTowards(transform.position, posFinal, velocidadCaida * Time.fixedDeltaTime);
            rb.MovePosition(nuevaPos);

            // Si llegamos al fondo
            if (Vector3.Distance(transform.position, posFinal) < 0.01f)
            {
                estaCayendo = false;
                // Esperamos un poco antes de subir
                Invoke("ComenzarSubida", tiempoEsperaAbajo); 
            }
        }

        // 3. LÓGICA DE RESET (Subida)
        if (estaReseteando)
        {
            Vector3 nuevaPos = Vector3.MoveTowards(transform.position, posInicial, velocidadSubida * Time.fixedDeltaTime);
            rb.MovePosition(nuevaPos);

            // Si llegamos arriba
            if (Vector3.Distance(transform.position, posInicial) < 0.01f)
            {
                estaReseteando = false;
                estaActivada = false;
            }
        }
    }

    // Función para que la plataforma vuelva a subir
    void ComenzarSubida()
    {
        estaReseteando = true;
    }

    // --- DETECCIÓN DEL JUGADOR ---
    private void OnCollisionStay(Collision collision)
    {
        if (estaActivada) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            // Detectamos si el jugador está por encima y no chocando de lado
            if (collision.transform.position.y > transform.position.y)
            {
                estaActivada = true;
                momentoActivacion = Time.time;
            }
            
            // Hacemos al jugador hijo para que baje con la plataforma
            collision.transform.SetParent(transform, true);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Soltamos al jugador
            collision.transform.SetParent(null);
        }
    }
}