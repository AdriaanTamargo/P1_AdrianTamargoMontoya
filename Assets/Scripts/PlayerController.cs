using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 5f;
    public float rotationSpeed = 15f; 

    private Rigidbody rb;
    private Animator anim;

    // Variables de Estado
    private bool isGrounded;    // Estoy tocando suelo?
    private bool canDoubleJump; // Puedo hacer doble salto?
    private float currentSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>(); // Busca el animator en el modelo
                
    }

    void Update()
    {
        // --- 1. MOVIMIENTO EN EJES  ---
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
        // Dirección
        Vector3 direction = new Vector3(h, 0, v).normalized;

        // --- 2. SPRINT ---
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = walkSpeed;
        }

        // --- 3. SISTEMA DE SALTO Y DOLE SALTO ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Salto normal (desde el suelo)
            if (isGrounded)
            {
                EjecutarSalto();
                canDoubleJump = true; // Se puede volver a usar el doble salto
                isGrounded = false;   
            }
            // Doble salto (en el aire)
            else if (canDoubleJump)
            {
                EjecutarSalto();
                canDoubleJump = false; // Gastamos el doble salto
                
            }
        }

        // --- 4. FÍSICAS DE MOVIMIENTO ---
        if (direction.magnitude >= 0.1f)
        {
            // Rotar suavemente hacia donde caminamos
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Calculamos la velocidad
            Vector3 moveVelocity = direction * currentSpeed;
            
            // Y la aplicamos
            rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
        }
        else
        {
            // Frenamos en seco al personaje si dejamos de tocar las teclas (no sigue avanzando)
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        // --- 5. ANIMACIONES DE TODO ---
        if (anim != null)
        {
            anim.SetFloat("Speed", direction.magnitude * currentSpeed);
            anim.SetBool("IsGrounded", isGrounded);
        }
    }

    void EjecutarSalto()
    {
        // Reseteamos velocidad vertical para que el salto sea siempre igual de potente
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // --- COLISIONES (DETECCION DE TAG GROUND) ---

    private void OnCollisionEnter(Collision collision)
    {
        // Si tocamos algo con el tag "Ground"
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = true;
            canDoubleJump = true; // Al tocar suelo recuperamos saltos
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
        }
    }
}