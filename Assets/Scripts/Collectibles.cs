using UnityEngine;

public class Collectibles : MonoBehaviour
{
    // Empty que contiene las puertas o bloques finales
    private GameObject puertasFinales;

    void Start()
    {
        // Buscamos el Empty "PuertasFinales" en la escena
        puertasFinales = GameObject.Find("PuertasFinales");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gm = FindFirstObjectByType<GameManager>();
            gm.AddCollectible();
            Destroy(gameObject);

            // Comprobamos si ya se recogieron todos
            if (gm.collectibleNumber >= gm.totalCollectibles)
            {
                if (puertasFinales != null)
                {
                    foreach (Transform block in puertasFinales.transform)
                    {
                        block.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

}
