
using System.Collections;
using UnityEngine;

namespace Door
{
    // Tipos de puerta: Automatica, Normal, DeLlave, Evento, MultiplesLlaves
    public class Door : MonoBehaviour, IInteractable
    {
        [SerializeField] public TipoDePuerta tipoDePuerta;
        [SerializeField] public bool eventoActivado = false;
        [SerializeField] public bool showKeysNames;
        public SOItem key;
        public SOItem[] keys;

        private InventoryHandler1 inventoryHandler;
        private TargetManager targetManager;
        internal Texture2D names;

     //   [SerializeField] private bool hasKey; // Nueva propiedad para mostrar en el inspector si se tiene la llave.

        private void Awake()
        {
            inventoryHandler = FindFirstObjectByType<InventoryHandler1>();
            targetManager = FindFirstObjectByType<TargetManager>();
        }

        private void Update()
        {
            Automatica();
        }

        public void Interact()
        {
            switch (tipoDePuerta)
            {
                case TipoDePuerta.Normal:
                    {
                        Normal();
                        break;
                    }

                case TipoDePuerta.DeLlave:
                    {
                        DeLlave();
                        break;
                    }

                case TipoDePuerta.Evento:
                    {
                        Evento();
                        break;
                    }

                case TipoDePuerta.MultiplesLlaves:
                    {
                        MultiplesLlaves();
                        break;
                    }
            }
        }

        private void Automatica()
        {
            if (tipoDePuerta == TipoDePuerta.Automatica && Touch())
            {
                StartCoroutine(OpenDoorAutomatic());
            }
        }

        private void Normal()
        {
            Debug.Log("Se abre");
            StartCoroutine(NormalOpen());
        }

        private void Evento()
        {
            //if (targetManager.levelComplete == true)
            //{
            //    eventoActivado = true;
            //    Debug.Log("Se ha activado el evento");
            //    StartCoroutine(NormalOpen());
            //}
            //else
            //{
            //    Debug.Log("No se ha activado el evento");
            //}
        }

        private void MultiplesLlaves()
        {
            foreach (SOItem item in keys)
            {
                if (inventoryHandler.inventory.Contains(item))
                {
                    //hasKey = true; // Actualiza la propiedad hasKey cuando se tiene la llave.
                    Debug.Log("Se abrio con multiples llaves");
                    Destroy(gameObject);
                }
                else
                {
                    //hasKey = false; // Actualiza la propiedad hasKey cuando no se tiene la llave.
                    Debug.Log("No tienes las llaves");
                }
            }
        }

        private void DeLlave()
        {
            Debug.Log("Ver");
            if (inventoryHandler.inventory.Contains(key))
            {
                Debug.Log("Si tengo llave");
                Debug.Log("Se abrio con 1 llave");
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("No tienes la llave");
            }
        }

        [ContextMenu("Show | Hide keys Names")]
        public void ToggleBool()
        {
            showKeysNames = !showKeysNames;
        }

        bool Touch()
        {
            return Physics.CheckSphere(transform.position, 4f, LayerMask.GetMask("Player"));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 4f);
        }

        IEnumerator OpenDoorAutomatic()
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * 2f, 2f);
            Debug.Log("Se abre automaticamente");
            yield return new WaitForSeconds(2f);
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down * 2f, 2f);
        }

        IEnumerator NormalOpen()
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.up * 2f, 2f);
            yield return new WaitForSeconds(2f);
        }
    }

    public enum TipoDePuerta
    {
        Automatica, Normal, DeLlave, Evento, MultiplesLlaves
    }
}


