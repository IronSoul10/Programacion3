using UnityEngine;
using UnityEngine.UI;


public class CamaraSeguridad : MonoBehaviour
{
    [SerializeField] private Camera secondaryCamera; // Cámara secundaria
    [SerializeField] private RawImage secondaryCameraView; // Vista de la cámara secundaria

    private void Start()
    {
        secondaryCamera.enabled = true; // Inicialmente, la cámara secundaria está desactivada
        secondaryCameraView.enabled = true; // Inicialmente, la vista de la cámara secundaria está desactivada
    }

}