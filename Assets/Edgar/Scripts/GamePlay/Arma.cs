using UnityEngine;

public class Arma : MonoBehaviour
{
    [SerializeField] float velocidadBala;
    [SerializeField] GameObject balaPrefab;
    [SerializeField] Transform puntoTiro;

    [SerializeField] public int municionActual = 100;
    //[SerializeField] int capacidadMaxima = 100;

    private ManagerBalas contador;
    

    private void Start()
    {
        contador = FindAnyObjectByType<ManagerBalas>();
        AudioManager.AudioInstance.Stop("Disparo");
    }

    void Update()
    {
        AccionarArma();
    }
    public void AccionarArma()
    {
        if (JalaGatillo())
        {
            Disparar();

        }
    }
    bool JalaGatillo()
    {
        return Input.GetKey(KeyCode.Mouse0);
    }

    public void Disparar()
    {
        if (JalaGatillo() && municionActual > 0)
        {
            //AudioManager.AudioInstance.Play("Disparo");
            GameObject clone = Instantiate(balaPrefab, puntoTiro.position, puntoTiro.rotation);
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddForce(puntoTiro.forward * velocidadBala, ForceMode.Impulse);
            Destroy(clone, 3);
            municionActual--;
           // ActualizarHUD();
        }
        //if (municionActual <= 0)
        //{
        //    AudioManager.AudioInstance.Stop("Disparo");

        //}
    }

    public void MunicionActualEnArma()
    {
        municionActual = 100;

    }
    //public void ActualizarHUD()
    //{
    //    contador.ActualizarContadorArma1(municionActual);
    //}
   
}
  

