using UnityEngine;
using TMPro;

public class UIUpdater : MonoBehaviour
{
    public TextMeshProUGUI textoMonedas;
    public TextMeshProUGUI textoVidas;

    void Start()
    {
        if (GameManagerClase.instancia != null)
        {
            GameManagerClase.instancia.OnMonedasChanged += ActualizarMonedas;
            GameManagerClase.instancia.OnVidasChanged += ActualizarVidas;
    
            ActualizarMonedas(0);
            ActualizarVidas(3);
        }
    }

    private void OnDestroy()
    {
        if (GameManagerClase.instancia != null)
        {
            GameManagerClase.instancia.OnMonedasChanged -= ActualizarMonedas;
            GameManagerClase.instancia.OnVidasChanged -= ActualizarVidas;
        }
    }

    private void ActualizarMonedas(int totalMonedas)
    {
        if (textoMonedas != null)
        {
            textoMonedas.text = totalMonedas.ToString();
        }
    }

    private void ActualizarVidas(int totalVidas)
    {
        if (textoVidas != null)
        {
            textoVidas.text = totalVidas.ToString();
        }
    }
}