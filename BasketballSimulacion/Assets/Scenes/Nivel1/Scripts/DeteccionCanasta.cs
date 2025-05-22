using UnityEngine;

public class DeteccionCanasta : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Lanzador lanzador = other.GetComponent<Lanzador>();
        if (lanzador != null)
        {
            lanzador.ReiniciarDesdeInicio();
        }
    }
}
