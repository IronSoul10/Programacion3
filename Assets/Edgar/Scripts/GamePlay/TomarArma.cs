using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomarArma : MonoBehaviour
{
    [SerializeField] GameObject arma2;
    void Update()
    {
        Equipar();
    }

    void Equipar()
    {
        if(Take())
        {
            gameObject.SetActive(false);
            arma2.SetActive(true);
        }
    }
    bool Take()
    {
        return Input.GetKeyDown(KeyCode.P);
    }
}
