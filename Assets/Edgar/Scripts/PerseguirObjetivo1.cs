
using UnityEngine;
using UnityEngine.AI;


namespace Edgar

{
    /// <summary>
    /// /// EJERCICIO/TAREA
    /// 
    /// Hacer que deje de perseguir al objetivo
    /// </summary>
    public class PerseguirObjetivo : MonoBehaviour
    {

        public Transform objetivo;
        public float velocidad;

        private NavMeshAgent agent;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Perseguir()
        {
            agent.speed = velocidad;
            agent.destination = objetivo.position;
        }

    }

}