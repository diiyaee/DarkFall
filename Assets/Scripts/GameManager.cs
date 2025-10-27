using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text collectibleNumberText;
    public TMP_Text collectibleNumberTotalText;
    public int collectibleNumber;
    public AudioSource collectibleSound;

    [HideInInspector] public int totalCollectibles; // guardamos el total al inicio

    void Start()
    {
        collectibleNumber = 0;
        totalCollectibles = transform.childCount; // total de Kids al inicio
        collectibleNumberText.text = collectibleNumber.ToString();
        collectibleNumberTotalText.text = totalCollectibles.ToString();
    }

    public void AddCollectible()
    {
        collectibleSound.Play();

        collectibleNumber++;
        collectibleNumberText.text = collectibleNumber.ToString();
    }
}
