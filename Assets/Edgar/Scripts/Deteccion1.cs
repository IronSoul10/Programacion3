using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Edgar
{

    /// <summary>
    /// EJERCICIO/TAREA
    /// 
    /// Tienen que hacer que cuando se deje de detectar al objetivo después de X cantidad de tiempo, este regrese a el ultimo punto de patrullaje al que fue
    /// 
    /// </summary>
    public class Deteccion : MonoBehaviour
    {

        public float radioDeDetección;
        public LayerMask layer;

        private Patrullaje patrullador;
        private PerseguirObjetivo perseguir;

        private void Start()
        {
            patrullador = GetComponent<Patrullaje>();
            perseguir = GetComponent<PerseguirObjetivo>();
        }

        private void Update()
        {
            Detectar();
        }

        private void Detectar()
        {
            if (Physics.CheckSphere(transform.position, radioDeDetección, layer))
            {
                patrullador.DejarDePatrullar();
                perseguir.Perseguir();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeDetección);
        }


    }
}
