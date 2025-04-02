using UnityEngine;
using UnityEngine.UI;


public class CamaraSeguridad : MonoBehaviour
{
    [SerializeField] private Camera secondaryCamera; // C�mara secundaria
    [SerializeField] private RawImage secondaryCameraView; // Vista de la c�mara secundaria

    private void Start()
    {
        secondaryCamera.enabled = true; // Inicialmente, la c�mara secundaria est� desactivada
        secondaryCameraView.enabled = true; // Inicialmente, la vista de la c�mara secundaria est� desactivada
    }

}