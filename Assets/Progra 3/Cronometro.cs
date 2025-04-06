using System.Collections;
using TMPro;
using UnityEngine;

public class Cronometro : MonoBehaviour
{
    [SerializeField] public bool enMarcha = false;
    [SerializeField] public float tiempoInicial = 60f; // Tiempo inicial en segundos
    [SerializeField] public TextMeshProUGUI cronometroTexto; // Referencia al componente de texto en el canvas
    [SerializeField] private TextMeshProUGUI reiniciarText;
    [SerializeField] private TextMeshProUGUI cronometroText;
    [SerializeField] private TextMeshProUGUI contadorText;
    [SerializeField] private GameObject CanvasLeaderBoard;
    [HideInInspector] public float tiempoRestante;

    private PlayerMovement playerMovement;
    private Arma arma;
    private CameraController1 cameraController1;
    private PlayFabManager playFabManager;


    void Start()
    {
        playerMovement = FindFirstObjectByType<PlayerMovement>();
        arma = FindFirstObjectByType<Arma>();
        cameraController1 = FindFirstObjectByType<CameraController1>();
        tiempoRestante = tiempoInicial;
        ActualizarCronometro(tiempoRestante);
        playFabManager = FindFirstObjectByType<PlayFabManager>();
    }

    private void Update()
    {
        CuentaAtras();
    }

    public void CuentaAtras()
    {
        if (enMarcha == true)
        {
            tiempoRestante -= Time.deltaTime;
            Debug.Log("Cuenta atras");
            if (tiempoRestante <= 0)
            {
                Cursor.lockState = CursorLockMode.None; // Desbloquear el cursor
                tiempoRestante = 0;
                enMarcha = false;
                CanvasLeaderBoard.SetActive(true); //realizar cuando el tiempo llegue a cero
                reiniciarText.enabled = true;
                cronometroText.enabled = false;
                StartCoroutine(Espera());
                contadorText.enabled = false;
                DesactivarPlayer();

            }
            ActualizarCronometro(tiempoRestante);
        }
    }

    IEnumerator Espera()
    {
        yield return new WaitForSeconds(2f);
        playFabManager.RequestLeaderboard(); // llamar al leaderboard
        StopCoroutine(Espera());

    }

    public void ReiniciarCronometro()
    {
        enMarcha = false;
        tiempoRestante = tiempoInicial;
        ActualizarCronometro(tiempoRestante);
    }

    private void ActualizarCronometro(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60F); // Dividir el tiempo en minutos
        int segundos = Mathf.FloorToInt(tiempo % 60F);
        int milisegundos = Mathf.FloorToInt((tiempo * 100F) % 100F);
        cronometroTexto.text = string.Format("{0:00}:{1:00}:{2:00}", minutos, segundos, milisegundos);
    }

    private void DesactivarPlayer()
    {
        playerMovement.enabled = false;
        arma.enabled = false;
        cameraController1.enabled = false;

    }
}
