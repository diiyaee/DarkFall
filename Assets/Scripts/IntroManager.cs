using UnityEngine;
using System.Collections;

public class LabIntroManager : MonoBehaviour
{
    public GameObject MenuIntro2; // Panel que aparece al cargar Laberinto

    void Start()
    {
        if (MenuIntro2)
        {
            MenuIntro2.SetActive(true);
            // Inicia la corutina para cerrar al click
            StartCoroutine(EscucharClick());
        }
    }

    private IEnumerator EscucharClick()
    {
        bool clickDetectado = false;

        while (!clickDetectado)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
                clickDetectado = true;

            yield return null;
        }

        if (MenuIntro2)
            MenuIntro2.SetActive(false);
    }
}
