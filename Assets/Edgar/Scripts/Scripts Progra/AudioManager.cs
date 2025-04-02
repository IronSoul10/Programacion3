using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager AudioInstance;
    public Sonido[] musica;

    private void Awake()
    {
        if (AudioInstance == null)
        {
            AudioInstance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }

        foreach (Sonido s in musica)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volumen;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string nombre)
    {
        foreach (Sonido s in musica)
        {
            if (s.nombre == nombre)
            {
                
                s.source.Play();
                return;
            }

          
        }
    }

    public void Stop(string nombre)
    {
        foreach (Sonido s in musica)
        {
            if (s.nombre == nombre)
            {
                s.source.Stop();
                return;
            }

             
        }
    }
}


