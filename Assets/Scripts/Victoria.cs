using UnityEngine;
using UnityEngine.InputSystem;

public class Victoria : MonoBehaviour
{
    public GameObject MenuVictoria; // Panel de victoria

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Activar el menú de victoria
            if (MenuVictoria)
                MenuVictoria.SetActive(true);

            // Mostrar el ratón y desbloquearlo
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Bloquear controles del jugador
            var playerInput = other.GetComponent<PlayerInput>();
            if (playerInput != null)
                playerInput.DeactivateInput();

            // Pausar el tiempo del juego
            Time.timeScale = 0f;
        }
    }
}
