using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    [Header("Sensibilidad")]
    public float sensibilidadX = 3f; // velocidad de giro horizontal

    [Header("Colisi�n c�mara")]
    public Transform pivot;          // CameraPivot (Empty, hijo del jugador)
    public LayerMask collisionMask;  // capas de obst�culos
    public float defaultDistance = 3f;
    public float minDistance = 0.5f;
    public float smoothSpeed = 10f;
    public float verticalOffset = 0.2f; // altura del raycast para evitar cabeza

    private Transform playerCameraRoot;
    private float currentDistance;

    void Start()
    {
        playerCameraRoot = transform.parent; // la c�mara est� dentro del PlayerCameraRoot
        currentDistance = defaultDistance;
    }

    void LateUpdate()
    {
        // --- SENSIBILIDAD ---
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadX;
        playerCameraRoot.Rotate(Vector3.up * mouseX);

        // --- LANZA UN RAYO DEL PIVOT A LA C�MARA Y DETECTA SI HAY ALG�N OBST�CULO QUE CORTA EL RAYO ---
        Vector3 rayOrigin = pivot.position + Vector3.up * verticalOffset;
        Vector3 rayDir = transform.position - rayOrigin;
        float targetDistance = defaultDistance;

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDir, out hit, defaultDistance, collisionMask))
        {
            targetDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
        }

        // --- HACER MOVIMIENTOS M�S SUAVES Y NO BRUSCOS AL ENCONTRARSE CON OBST�CULOS ---
        currentDistance = Mathf.Lerp(currentDistance, targetDistance, Time.deltaTime * smoothSpeed);

        Vector3 localDir = transform.localPosition.normalized;
        transform.localPosition = localDir * currentDistance;

        // Mantener la c�mara mirando al pivot
        transform.LookAt(pivot.position);
    }
}
