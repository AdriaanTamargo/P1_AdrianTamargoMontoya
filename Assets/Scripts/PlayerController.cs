using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 5f;
    public float rotationSpeed = 15f; 

    private Rigidbody rb;
    private Animator anim;

    private bool isGrounded;    
    private bool canDoubleJump; 
    private float currentSpeed;
    private Vector3 respawnPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; 
        anim = GetComponentInChildren<Animator>(); 

        respawnPosition = transform.position;
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(h, 0, v).normalized;

        if (Input.GetKey(KeyCode.LeftShift)) currentSpeed = sprintSpeed;
        else currentSpeed = walkSpeed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                EjecutarSalto();
                canDoubleJump = true; 
                isGrounded = false;   
            }
            else if (canDoubleJump)
            {
                EjecutarSalto();
                canDoubleJump = false; 
            }
        }

        if (direction.magnitude >= 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            Vector3 moveVelocity = direction * currentSpeed;
            rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
        }

        if (anim != null)
        {
            anim.SetFloat("Speed", direction.magnitude * currentSpeed);
            anim.SetBool("IsGrounded", isGrounded);
        }
    }

    void EjecutarSalto()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = true;
            canDoubleJump = true; 
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("MovingPlatform"))
        {
            isGrounded = false;
        }
    }

    public void Respawn()
    {
        transform.position = respawnPosition;
        rb.linearVelocity = Vector3.zero;
    }
}