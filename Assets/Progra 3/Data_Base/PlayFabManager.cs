using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.UI;


public class PlayFabManager : MonoBehaviour
{

    #region Variables
    [Header("PLAYFAB SETTINGS")]
    [SerializeField] private string titleID = "3020F";
    [SerializeField] private string secretKey = "ERSDCC7P8IQQGMYBWCB5OWT83UHCOIRTJT1YFIOAI8ABA3HNRR";

    [Header("Create Account Inputs")]
    [SerializeField] private TMP_InputField newUsernameInput;
    [SerializeField] private TMP_InputField setPasswordInput;

    [Header("Log In Inputs")]
    [SerializeField] private TMP_InputField usernameInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private UnityEvent onLogin;

    [Header("User Info")]
    [SerializeField] private TMP_Text userDisplayNameText;
    [SerializeField] private Image userProfilePicture;

    [Header("Leaderboard")]
    [SerializeField] private GameObject leaderboardPanel; // Panel del canvas de la tabla de clasificación
    [SerializeField] private GameObject userLeadboardPrefab; // Prefab de la tabla de clasificación
    [SerializeField] private Transform userLeadboardParent; // Padre de los elementos de la tabla de clasificación

    //Image Profile
    private Texture2D avatarTexture;
    private Sprite avatarSprite;
    private float avatarWidth = 100;
    private float avatarHeight = 100;

    private string userDisplayName;

    private Cronometro cronometro;

    #endregion

    private void Start()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId) || string.IsNullOrEmpty(PlayFabSettings.DeveloperSecretKey))
        {
            PlayFabSettings.TitleId = titleID;
            PlayFabSettings.DeveloperSecretKey = secretKey;
            Cursor.lockState = CursorLockMode.None;
            Pause();
        }
        cronometro = FindFirstObjectByType<Cronometro>();
        DontDestroyOnLoad(userProfilePicture.gameObject);

    }
    private void Update()
    {
        Resume();
    }

    #region Pausar y reanudar
    void Pause()
    {
        Time.timeScale = 0;
    }
    void Resume()
    {
        Time.timeScale = 1;
    }
    #endregion

    #region Crear cuenta
    public void RegisterUser() // Registra un nuevo usuario en PlayFab con el nombre de usuario y la contraseña
    {
        if (string.IsNullOrEmpty(newUsernameInput.text) || string.IsNullOrEmpty(setPasswordInput.text))
        {
            Debug.LogWarning("Alguno de los campos esta vacion");
            return;
        }

        var request = new RegisterPlayFabUserRequest()
        {
            DisplayName = newUsernameInput.text,
            Username = newUsernameInput.text,
            Password = setPasswordInput.text,
            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucces, PlayfabErrorMessage);
    }

    private void OnRegisterSucces(RegisterPlayFabUserResult result) // Se llama cuando el registro es exitoso
    {
        Debug.Log("USUARIO REGISTRADO CORRECTAMENTE");
    }
    #endregion

    #region Iniciar sesion  
    public void LogInUser() // Inicia sesión en PlayFab con el nombre de usuario y la contraseña
    {
        if (string.IsNullOrEmpty(usernameInput.text) || string.IsNullOrEmpty(passwordInput.text))
        {
            Debug.LogWarning("ALGUNO DE LOS CAMPOS ESTA VACIO");
            return;
        }

        var request = new LoginWithPlayFabRequest()
        {
            Username = usernameInput.text,
            Password = passwordInput.text,
        };

        Cursor.lockState = CursorLockMode.None;
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSucces, PlayfabErrorMessage);
    }

    private void OnLoginSucces(LoginResult result) // Se llama cuando el inicio de sesión es exitoso
    {
        Debug.Log("SESION INICIADA CORRECTAMENTE");
        onLogin?.Invoke();
        GetPlayerProfile(); // Obtiene el perfil del jugador después de iniciar sesión
        Resume();
        cronometro.enabled = true; // Inicia la cuenta regresiva del cronómetro
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor en el centro de la pantalla
    }
    #endregion

    #region Obtener datos del jugador
    public void GetPlayerProfile() // Obtiene el perfil del jugador después de iniciar sesión
    {
        var request = new GetPlayerProfileRequest()
        {
            ProfileConstraints = new PlayerProfileViewConstraints()
            {
                ShowAvatarUrl = true,
                ShowDisplayName = true
            }
        };
        PlayFabClientAPI.GetPlayerProfile(request, OnGetProfileInfoSucces, PlayfabErrorMessage);
    }

    private IEnumerator ShowAvatar(string avatarUrl, Image avatarImage) // Descarga el avatar del jugador y lo muestra en la pantalla de inicio de sesión
    {
        Debug.Log("Iniciando corrutina ShowAvatar para URL: " + avatarUrl); // Log al inicio de la corrutina

        // Verifica si la URL es válida
        if (string.IsNullOrEmpty(avatarUrl))
        {
            Debug.LogWarning("URL de avatar no válida: " + avatarUrl);
            yield break;
        }

        UnityWebRequest webRequest = UnityWebRequestTexture.GetTexture(avatarUrl);

        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            avatarTexture = ((DownloadHandlerTexture)webRequest.downloadHandler).texture;
            avatarSprite = Sprite.Create(avatarTexture, new Rect(0, 0, avatarTexture.width, avatarTexture.height), new Vector2(0.5f, 0.5f));

            if (avatarImage != null)
            {
                avatarImage.sprite = avatarSprite;
                avatarImage.rectTransform.sizeDelta = new Vector2(avatarWidth, avatarHeight); // Establece el tamaño de la imagen a 100x100 píxeles
                Debug.Log("Avatar obtenido correctamente de la API.");
            }
            else
            {
                Debug.LogWarning("El componente Image no está asignado.");
            }
        }
        else
        {
            Debug.Log("Error al obtener el avatar: " + webRequest.error);
        }

        Debug.Log("Corrutina ShowAvatar completada para URL: " + avatarUrl); // Log al final de la corrutina
    }

    private void OnGetProfileInfoSucces(GetPlayerProfileResult result) // Consigue la informacion del usuario
    {
        userDisplayName = result.PlayerProfile.DisplayName;
        userDisplayNameText.text = userDisplayName;

        string avatarUrl = result.PlayerProfile.AvatarUrl;
        Debug.Log("Descargando Avatar URL: " + avatarUrl + "Del Usuario:" + userDisplayName); // Imprime la URL en la consola para depuración
        StartCoroutine(ShowAvatar(avatarUrl, userProfilePicture));
    }
    #endregion

    #region Tabla de clasificacion
    public void UpdateLeaderBoard(int value) // Actualiza la tabla de clasificación con la puntuación del jugador // Se manda a llamar desde el script de la puntuación
    {
        var request = new UpdatePlayerStatisticsRequest()
        {
            Statistics = new List<StatisticUpdate>()
            {
                new StatisticUpdate()
                {
                    StatisticName = "Estadistica",
                    Value = value
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderUpdateSuccess, PlayfabErrorMessage); // Llama a la función OnLeaderUpdateSuccess si la actualización es exitosa
    }
    private void OnLeaderUpdateSuccess(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Se actualizo el leaderboard correctamente");
    }
    public void RequestLeaderboard() // Obtiene la tabla de clasificación
    {
        var request = new GetLeaderboardRequest()
        {
            StatisticName = "Estadistica",
            StartPosition = 0,
            MaxResultsCount = 10,
            ProfileConstraints = new PlayerProfileViewConstraints() // Permite mostrar la URL del avatar en la tabla de clasificación
            {
                ShowDisplayName = true, // Incluye el nombre de usuario
                ShowAvatarUrl = true // Include Avatar URL

            }
        };
        PlayFabClientAPI.GetLeaderboard(request, DisplayLeaderboard, PlayfabErrorMessage); // Llama a la función DisplayLeaderboard si la solicitud es exitosa
    }
    private void DisplayLeaderboard(GetLeaderboardResult result) // Muestra la tabla de clasificación en la consola
    {
        foreach (Transform child in userLeadboardParent) // Elimino todos los elementos de la tabla de clasificación antes de mostrar la nueva tabla de clasificación
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < result.Leaderboard.Count; i++) // Recorre la lista de jugadores en la tabla de clasificación
        {
            var score = result.Leaderboard[i]; // Obtiene la puntuación del jugador en la posición i
            GameObject userLeadboardInstance = Instantiate(userLeadboardPrefab, userLeadboardParent); // Instancia el prefab de la tabla de clasificación

            // Ajusta la posición de cada instancia para que tenga separación vertical
            RectTransform rectTransform = userLeadboardInstance.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -i * 80f); // Establece la posición de la instancia en función de i

            TextMeshProUGUI[] texts = userLeadboardInstance.GetComponentsInChildren<TextMeshProUGUI>(); // Obtiene los componentes TextMeshProUGUI del prefab
            texts[0].text = score.DisplayName; // Establece el nombre del jugador en el primer componente TextMeshProUGUI
            texts[1].text = score.StatValue.ToString(); // Establece la puntuación del jugador en el segundo componente TextMeshProUGUI

            Transform thirdChild = userLeadboardInstance.transform.GetChild(2); // Obtiene el tercer hijo del prefab
            Image avatarImage = thirdChild.GetComponent<Image>(); // Obtiene el componente Image del tercer hijo del prefab
            if (avatarImage != null)
            {
                string avatarUrl = score.Profile.AvatarUrl;
                Debug.Log("Descargando Avatar URL en leadBoard: " + avatarUrl + " para " + score.DisplayName);
                StartCoroutine(ShowAvatar(avatarUrl, avatarImage));
            }
            else
            {
                Debug.LogWarning("El tercer hijo del prefab no contiene un componente Image.");
            }

            Debug.Log(string.Format("Nombre: {0} | Highscore: {1}", score.DisplayName, score.StatValue));
        }
    }
    #endregion

    private void PlayfabErrorMessage(PlayFabError error)
    {
        Debug.LogWarning(error.ErrorMessage);
    }
}
