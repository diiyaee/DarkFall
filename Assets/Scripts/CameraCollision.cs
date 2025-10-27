using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [Header("Sensibilidad")]
    public float sensibilidadX = 3f; // velocidad de giro horizontal

    [Header("Colisión cámara")]
    public Transform pivot;          // CameraPivot (Empty, hijo del jugador)
    public LayerMask collisionMask;  // capas de obstáculos
    public float defaultDistance = 3f;
    public float minDistance = 0.5f;
    public float smoothSpeed = 10f;
    public float verticalOffset = 0.2f; // altura del raycast para evitar cabeza

    private Transform playerCameraRoot;
    private float currentDistance;

    void Start()
    {
        playerCameraRoot = transform.parent; // la cámara está dentro del PlayerCameraRoot
        currentDistance = defaultDistance;
    }

    void LateUpdate()
    {
        // --- SENSIBILIDAD ---
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadX;
        playerCameraRoot.Rotate(Vector3.up * mouseX);

        // --- LANZA UN RAYO DEL PIVOT A LA CÁMARA Y DETECTA SI HAY ALGÚN OBSTÁCULO QUE CORTA EL RAYO ---
        Vector3 rayOrigin = pivot.position + Vector3.up * verticalOffset;
        Vector3 rayDir = transform.position - rayOrigin;
        float targetDistance = defaultDistance;

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDir, out hit, defaultDistance, collisionMask))
        {
            targetDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
        }

        // --- HACER MOVIMIENTOS MÁS SUAVES Y NO BRUSCOS AL ENCONTRARSE CON OBSTÁCULOS ---
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * smoothSpeed);

        Vector3 localDir = transform.localPosition.normalized;
        transform.localPosition = localDir * currentDistance;

        // Mantener la cámara mirando al pivot
        transform.LookAt(pivot.position);
    }
}
