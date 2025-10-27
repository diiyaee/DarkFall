using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

public class GestorMenus : MonoBehaviour
{
    [Header("Menus")]
    public GameObject menuPrincipal;
    public GameObject menuIntro;
    public GameObject menuIntro2;
    public GameObject menuPausa;
    public GameObject menuVictoria;

    void Awake()
    {
        if (menuPrincipal) menuPrincipal.SetActive(false);
        if (menuIntro) menuIntro.SetActive(false);
        if (menuIntro2) menuIntro2.SetActive(false);
        if (menuPausa) menuPausa.SetActive(false);
        if (menuVictoria) menuVictoria.SetActive(false);
    }

    void Start()
    {
        // Mostrar MenuPrincipal al entrar a Sendero
        if (SceneManager.GetActiveScene().name == "Sendero" && menuPrincipal)
        {
            ActivarMenu(menuPrincipal);
        }
        else
        {
            // Si no hay menú activo, ocultar cursor
            OcultarCursor();
        }

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (menuPausa && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!menuPausa.activeSelf) AbrirMenuPausa();
            else CerrarMenuPausa();
        }
    }

    // ===== BOTONES =====
    public void BotonJugar()
    {
        if (menuPrincipal) menuPrincipal.SetActive(false);

        if (menuIntro)
        {
            ActivarMenu(menuIntro);
            StartCoroutine(EscucharClickCerrarMenu(menuIntro));
            StartCoroutine(OcultarCursorDespues(menuIntro));
        }
    }

    public void BotonSortir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ===== MENU PAUSA =====
    public void AbrirMenuPausa()
    {
        if (menuPausa) ActivarMenu(menuPausa);
        Time.timeScale = 0f;
    }

    public void CerrarMenuPausa()
    {
        if (menuPausa) menuPausa.SetActive(false);
        OcultarCursor();
        Time.timeScale = 1f;
    }

    public void BotonReanudar()
    {
        CerrarMenuPausa();
    }

    // ===== BotónMenuPrincipal =====
    public void BotonMenuPrincipal()
    {
        // Forzar cursor visible al ir al menú
        ActivarMenu(menuPrincipal);

        // Recargar Sendero
        SceneManager.LoadScene("Sendero");
    }

    // ===== MENU VICTORIA =====
    
    public void BotonVictoriaJugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Sendero");
    }

    public void BotonVictoriaSortir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // ===== CORUTINA PARA CERRAR MENÚS DE INTRO =====
    private IEnumerator EscucharClickCerrarMenu(GameObject menu)
    {
        bool clickDetectado = false;

        while (!clickDetectado)
        {
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
                clickDetectado = true;

            yield return null;
        }

        if (menu) menu.SetActive(false);
    }

    // ===== CORUTINA PARA OCULTAR CURSOR DESPUÉS DE CERRAR UN MENÚ =====
    private IEnumerator OcultarCursorDespues(GameObject menu)
    {
        while (menu.activeSelf)
            yield return null;

        OcultarCursor();
    }

    // ===== MÉTODOS DE CURSOR =====
    private void ActivarMenu(GameObject menu)
    {
        if (menu)
        {
            menu.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OcultarCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
