using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Arma arma;
    private CameraController1 cameraController1;
    private Cronometro cronometro;

  
    private void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        arma = FindFirstObjectByType<Arma>();
        cameraController1 = FindFirstObjectByType<CameraController1>();
        cronometro = FindFirstObjectByType<Cronometro>();
    }
    private void Update()
    {
        Reintentar();
    }
   
    public void EncenderPlayer()
    {
        arma.enabled = true;
        playerMovement.enabled = true;
        cameraController1.enabled = true;
    }

    public void Reintentar()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("MenuPrincipal");
            EncenderPlayer();
            cronometro.ReiniciarCronometro();
            
        }
    }
}
