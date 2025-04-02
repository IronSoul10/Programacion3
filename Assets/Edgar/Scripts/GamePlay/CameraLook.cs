using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float sensibilidadMouse = 100f; 
    public Transform playerBody; // 

    float rotacionX = 0f;

    void Start()
    {
       
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f); // 

        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f); // Rotar la cámara
        playerBody.Rotate(Vector3.up * mouseX); 
    }
}


