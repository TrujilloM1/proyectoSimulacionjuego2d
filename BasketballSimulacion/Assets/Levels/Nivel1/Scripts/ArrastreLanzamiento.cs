using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ArrastreLanzamiento : MonoBehaviour
{
    public float fuerzaMaxima = 10f;
    public Lanzador lanzador;

    private bool arrastrando = false;
    private Vector2 puntoInicio;
    private Camera cam;
    private LineRenderer linea;

    void Start()
    {
        cam = Camera.main;
        linea = GetComponent<LineRenderer>();
        linea.positionCount = 2;
        linea.enabled = false;
    }

    void OnMouseDown()
    {
        puntoInicio = cam.ScreenToWorldPoint(Input.mousePosition);
        arrastrando = true;
        linea.enabled = true;
    }

    void OnMouseDrag()
    {
        if (!arrastrando) return;

        Vector2 puntoActual = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = puntoInicio - puntoActual;
        float magnitud = Mathf.Min(direccion.magnitude, fuerzaMaxima);
        Vector2 fuerza = direccion.normalized * magnitud;

        linea.SetPosition(0, transform.position);
        linea.SetPosition(1, (Vector2)transform.position + fuerza);
    }

    void OnMouseUp()
    {
        if (!arrastrando) return;

        arrastrando = false;
        linea.enabled = false;

        Vector2 puntoFinal = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direccion = puntoInicio - puntoFinal;
        float magnitud = Mathf.Min(direccion.magnitude, fuerzaMaxima);
        Vector2 fuerza = direccion.normalized * magnitud;

        lanzador.ActualizarVelocidad(fuerza);
    }
}
