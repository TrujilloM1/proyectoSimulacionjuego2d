using UnityEngine;

public class Lanzador : MonoBehaviour
{
    [Header("Límites del área de juego")]
    public float limiteIzquierdo = -8f;
    public float limiteDerecho = 8f;
    public float limiteSuperior = 5f;

    public Vector2 velocidad;
    public float gravedad = -9.8f;
    public float velocidadMinima = 0.05f;
    public float groundY = -3f; // altura del suelo

    private Vector2 posicion;
    private bool enMovimiento = true;

    void Start()
    {
        posicion = transform.position;
    }

    void Update()
    {

        if (enMovimiento)
        {
            // Aplicar gravedad
            velocidad.y += gravedad * Time.deltaTime;

            // Mover la pelota manualmente
            posicion += velocidad * Time.deltaTime;

            // Verificar colisión con el suelo (Y mínima)
            if (posicion.y <= groundY)
            {
                posicion.y = groundY;
                velocidad.y *= -0.6f;       // rebote vertical
                velocidad.x *= 0.9f;        // fricción
            }

            // Detener si la velocidad es muy baja
            if (velocidad.magnitude < velocidadMinima)
            {
                velocidad = Vector2.zero;
                enMovimiento = false;
            }

            transform.position = posicion;
        }
        // Limitar horizontalmente
        if (posicion.x <= limiteIzquierdo)
        {
            posicion.x = limiteIzquierdo;
            velocidad.x *= -0.6f; // rebote
        }

        if (posicion.x >= limiteDerecho)
        {
            posicion.x = limiteDerecho;
            velocidad.x *= -0.6f;
        }

        // Limitar verticalmente
        if (posicion.y >= limiteSuperior)
        {
            posicion.y = limiteSuperior;
            velocidad.y *= -0.6f;
        }

    }

    public void ActualizarVelocidad(Vector2 nuevaVelocidad)
    {
        velocidad = nuevaVelocidad;
        enMovimiento = true;
    }

    public Vector2 GetVelocidad()
    {
        return velocidad;
    }

    public void ReiniciarDesdeInicio()
    {
        velocidad = Vector2.zero;
        posicion = transform.position;
        transform.position = posicion;
        enMovimiento = false;
    }

    public void RelanzarDesdePosicion()
    {
        velocidad = new Vector2(6f, 8f); // o alguna velocidad predeterminada
        enMovimiento = true;
    }
}
