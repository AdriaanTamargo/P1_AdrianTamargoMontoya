using UnityEngine;

public class Coleccionable : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {

        GameManagerClase.instancia.AddMoneda();
    }
}