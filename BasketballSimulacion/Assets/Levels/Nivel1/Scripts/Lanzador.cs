using UnityEngine;

public class Lanzador : MonoBehaviour
{
    public Vector2 velocidad;
    public float gravedad = -9.8f;
    public float velocidadMinima = 0.05f;
    public Vector2 posicionInicial;

    private Vector2 posicion;
    private bool enMovimiento = true;

    void Start()
    {
        posicion = posicionInicial;
        transform.position = posicion;
    }

    void Update()
    {
        if (enMovimiento)
        {
            velocidad.y += gravedad * Time.deltaTime;
            posicion += velocidad * Time.deltaTime;
            transform.position = posicion;

            if (velocidad.magnitude < velocidadMinima)
            {
                enMovimiento = false;
                velocidad = Vector2.zero;
            }
        }
    }

    public void ActualizarVelocidad(Vector2 nuevaVelocidad)
    {
        velocidad = nuevaVelocidad;
        enMovimiento = true;
    }

    public void ReiniciarDesdeInicio()
    {
        posicion = posicionInicial;
        transform.position = posicionInicial;
        velocidad = Vector2.zero;
        enMovimiento = false;
    }

    public void RelanzarDesdePosicion()
    {
        velocidad = new Vector2(6f, 8f); // Puedes ajustar esto o conectarlo a otro sistema
        enMovimiento = true;
    }

    public Vector2 GetVelocidad()
    {
        return velocidad;
    }
}
