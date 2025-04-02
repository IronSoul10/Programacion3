using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI reiniciarText;
    private PlayerMovement playerMovement;
    private Arma arma;
    private CameraController1 cameraController1;
    private Cronometro cronometro;

    private void Start()
    {
        reiniciarText.enabled = false;
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
        Time.timeScale = 1;
    }

    public void Reintentar()
    {
        if (cronometro.tiempoRestante <= 0)
        {
            reiniciarText.enabled = true;
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                EncenderPlayer();
                cronometro.ReiniciarCronometro();
            }
        }

    }

}
