using UnityEngine;

[System.Serializable]
public struct WeatherData
{
    [SerializeField] public string name;
    [SerializeField] public float actualTemp;
    [SerializeField] public float windSpeed;
    [SerializeField] public float humidity;
}

[System.Serializable]
public struct Country // Hice que la clase Country sea serializable para que pueda ser vista en el inspector
{
    [SerializeField] public string name; //Nombre del país
    [SerializeField] public float latitude; //Latitud
    [SerializeField] public float longitude; //Longitud
}

