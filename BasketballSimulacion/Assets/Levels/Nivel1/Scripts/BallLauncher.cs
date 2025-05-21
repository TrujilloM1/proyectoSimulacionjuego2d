using UnityEngine;

public class BallLauncher : MonoBehaviour
{
    [Header("Parámetros de disparo")]
    public float powerFactor = 5f;
    public float gravity = 9.8f;
    public float groundY = -3f;
    public float restitution = 0.6f; // rebote (0 = sin rebote, 1 = perfecto)
    public float stopThreshold = 0.5f;

    [Header("Línea de trayectoria")]
    public LineRenderer trajectoryLine;
    public int predictionPoints = 30;
    public float timeStep = 0.1f;

    [Header("Zona de canasta")]
    public float canastaXMin = 5.8f;
    public float canastaXMax = 6.2f;
    public float canastaY = -1.5f;

    private Vector2 launchVelocity;
    private Vector2 startDragPosition;
    private bool isDragging = false;
    private bool isLaunched = false;
    private bool haAnotado = false;
    private int puntos = 0;

    private float minX, maxX, maxY;

    void Start()
    {
        // Calcular límites de la cámara
        Camera cam = Camera.main;
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        minX = bottomLeft.x;
        maxX = topRight.x;
        maxY = topRight.y;
    }

    void Update()
    {
        if (isLaunched)
        {
            // Aplicar gravedad
            launchVelocity.y -= gravity * Time.deltaTime;
            transform.position += (Vector3)(launchVelocity * Time.deltaTime);

            Vector2 pos = transform.position;

            // Rebote con el suelo
            if (pos.y <= groundY)
            {
                pos.y = groundY;
                launchVelocity.y *= -restitution;
                launchVelocity.x *= 0.9f;
            }

            // Rebote con los bordes laterales
            if (pos.x <= minX)
            {
                pos.x = minX;
                launchVelocity.x *= -restitution;
            }
            else if (pos.x >= maxX)
            {
                pos.x = maxX;
                launchVelocity.x *= -restitution;
            }

            // Rebote en la parte superior (opcional)
            if (pos.y > maxY)
            {
                pos.y = maxY;
                launchVelocity.y *= -restitution;
            }

            transform.position = pos;

            // Detección de canasta
            if (!haAnotado &&
                pos.x >= canastaXMin &&
                pos.x <= canastaXMax &&
                pos.y <= canastaY &&
                launchVelocity.y < 0)
            {
                haAnotado = true;
                puntos++;
                Debug.Log("¡Canasta! Puntos: " + puntos);
            }

            // Detener la pelota si se vuelve muy lenta
            if (launchVelocity.magnitude < stopThreshold)
            {
                launchVelocity = Vector2.zero;
                isLaunched = false;
            }
        }
    }

    void OnMouseDown()
    {
        if (!isLaunched)
        {
            startDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
            trajectoryLine.positionCount = predictionPoints;
            haAnotado = false;
        }
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 currentDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = startDragPosition - currentDragPosition;
            Vector2 simulatedVelocity = direction * powerFactor;

            ShowTrajectory(transform.position, simulatedVelocity);
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            Vector2 endDragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = startDragPosition - endDragPosition;

            launchVelocity = direction * powerFactor;
            isLaunched = true;
            isDragging = false;
            trajectoryLine.positionCount = 0;
        }
    }

    void ShowTrajectory(Vector2 startPos, Vector2 velocity)
    {
        Vector2 currentVelocity = velocity;

        for (int i = 0; i < predictionPoints; i++)
        {
            float t = i * timeStep;
            float x = startPos.x + currentVelocity.x * t;
            float y = startPos.y + currentVelocity.y * t - 0.5f * gravity * t * t;

            if (y < groundY)
            {
                y = groundY - (y - groundY) * restitution;
                currentVelocity.y *= -restitution;
            }

            trajectoryLine.SetPosition(i, new Vector3(x, y, 0));
        }
    }
}
