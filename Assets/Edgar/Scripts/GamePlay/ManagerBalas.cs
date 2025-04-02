using TMPro;
using UnityEngine;

public class ManagerBalas : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI contadorTextoArma1;
    [SerializeField] public TextMeshProUGUI contadorTextoArma2;
    [SerializeField] public int municionActualArma1 = 100;
    [SerializeField] public int municionActualArma2 = 100;

    private void Start()
    {
        ActualizarContadores();
    }

    public void DispararArma1()
    {
        if (municionActualArma1 > 0)
        {
            municionActualArma1--;
            ActualizarContadores();
        }
    }

    public void DispararArma2()
    {
        if (municionActualArma2 > 0)
        {
            municionActualArma2--;
            ActualizarContadores();
        }
    }

    public void RecargarArma1()
    {
        municionActualArma1 = 100;
        ActualizarContadores();
    }

    public void RecargarArma2()
    {
        municionActualArma2 = 100;
        ActualizarContadores();
    }

    public void ActualizarContadorArma1(int cantidad)
    {
        municionActualArma1 = cantidad;
        contadorTextoArma1.text = " " + municionActualArma1;
    }

    public void ActualizarContadorArma2(int cantidad)
    {
        municionActualArma2 = cantidad;
        contadorTextoArma2.text = " " + municionActualArma2;
    }

    public void ActualizarContadores()
    {
        contadorTextoArma1.text = " " + municionActualArma1;
        contadorTextoArma2.text = " " + municionActualArma2;
    }
}
