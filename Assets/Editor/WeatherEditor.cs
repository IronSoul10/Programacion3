using UnityEditor;
using UnityEngine;
using Weather;

[CustomEditor(typeof(WeatherApi))]
public class WeatherEditor : Editor
{
    private WeatherApi weatherApi;
    private bool[] foldouts; // Array para manejar los foldouts de cada pa�s

    private void OnEnable()
    {
        // Inicializa el array de foldouts con el tama�o del array de pa�ses
        weatherApi = (WeatherApi)target;
        foldouts = new bool[weatherApi.countries.Length]; // Inicializa el array de foldouts con el tama�o del array de pa�ses
    }

    public override void OnInspectorGUI()
    {
        // Obtiene la referencia al script WeatherApi
        WeatherApi weatherApi = (WeatherApi)target;

        // Dibuja el campo de pa�ses
        EditorGUILayout.LabelField("Countries", EditorStyles.boldLabel); // Dibuja el label de pa�ses
        SerializedProperty countries = serializedObject.FindProperty("countries"); // Obtiene la propiedad de pa�ses
        for (int i = 0; i < countries.arraySize; i++) // Itera sobre los pa�ses
        {
            SerializedProperty country = countries.GetArrayElementAtIndex(i); // Obtiene el pa�s actual

            EditorGUILayout.PropertyField(country.FindPropertyRelative("name"), new GUIContent("Name")); // Dibuja el campo de nombre
            EditorGUILayout.PropertyField(country.FindPropertyRelative("latitude"), new GUIContent("Latitude")); // Dibuja el campo de latitud
            EditorGUILayout.PropertyField(country.FindPropertyRelative("longitude"), new GUIContent("Longitude")); // Dibuja el campo de longitud

            // Crea un foldout para cada pa�s
            foldouts[i] = EditorGUILayout.Foldout(foldouts[i], "Weather Data"); // Dibuja el foldout de datos del clima

            if (foldouts[i]) // Si el foldout est� abierto
            {
                // Verifica si este pa�s es el actual
                if (i == weatherApi.currentCountryIndex) // Si el pa�s es el actual
                {
                    EditorGUILayout.LabelField("Time Zone", weatherApi.data.name); // Dibuja la zona horaria
                    EditorGUILayout.LabelField("Actual Temperature", weatherApi.data.actualTemp.ToString()); // Dibuja la temperatura actual
                    EditorGUILayout.LabelField("Wind Speed", weatherApi.data.windSpeed.ToString()); // Dibuja la velocidad del viento
                    EditorGUILayout.LabelField("Humidity", weatherApi.data.humidity.ToString()); // Dibuja la humedad
                }
                else
                {
                    EditorGUILayout.LabelField("Weather data not available for this country."); // Dibuja un mensaje de que no hay datos
                }
            }
        }

        // Aplica los cambios
        serializedObject.ApplyModifiedProperties();
    }
}
