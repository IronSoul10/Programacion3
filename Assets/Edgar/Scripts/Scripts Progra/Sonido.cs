using UnityEngine;


[System.Serializable]
public class Sonido
{

    public string nombre;

    public AudioClip clip;

    [Range(0f, 1f)] 
    public float volumen;

    [Range(0f, 1f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
   
}
