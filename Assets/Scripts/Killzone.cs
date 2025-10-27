using UnityEngine;
using UnityEngine.SceneManagement;

public class Killzone : MonoBehaviour
{
    public string escenaDestino = "Laberinto";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(escenaDestino);
        }
    }
}
