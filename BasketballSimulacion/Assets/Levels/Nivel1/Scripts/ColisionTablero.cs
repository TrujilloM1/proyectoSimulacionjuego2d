using UnityEngine;

public class ColisionTablero : MonoBehaviour
{
    public float coeficienteRestitucion = 0.8f;

    void OnTriggerEnter2D(Collider2D other)
    {
        Lanzador lanzador = other.GetComponent<Lanzador>();
        if (lanzador != null)
        {
            Vector2 nuevaVelocidad = lanzador.GetVelocidad();
            nuevaVelocidad.x *= -coeficienteRestitucion;

            lanzador.ActualizarVelocidad(nuevaVelocidad);
        }
    }
}
