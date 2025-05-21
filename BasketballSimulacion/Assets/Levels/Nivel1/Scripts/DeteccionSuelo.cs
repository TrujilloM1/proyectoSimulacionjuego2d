using UnityEngine;

public class DeteccionSuelo : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Lanzador lanzador = other.GetComponent<Lanzador>();
        if (lanzador != null)
        {
            lanzador.Invoke("RelanzarDesdePosicion", 1f);
        }
    }
}
