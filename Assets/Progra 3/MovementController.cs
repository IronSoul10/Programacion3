using UnityEngine;

/// <summary>
/// Un movimiento que requiere fisica se debe de hacer con rigidbody
/// 
/// Un movimiento que unicamente es lateral, salto, y ya, Usas CharacterController
/// 
/// Un movimiento con Transform puedes hacer de todo, pero requiere más trabajo
/// 
/// Inputs
/// Ridigbody
/// 3 Velocidades
/// 
/// 
/// EJERCICIO
/// Deben de crear un controlador de camara
/// siguiendo las nomenclaturas, la modularidad de los metodos
/// y lógica de nombres usadas en este script
/// 
/// Al rotar cuando giras la cámara, debes de poder moverte hacia adelante de donde estas viendo
/// </summary>
public class MovementController : MonoBehaviour
{
    public float crouchSpeed = 3;
    public float walkSpeed = 5;
    public float runSpeed = 7;
    private Rigidbody rb;

    public bool puedePasar = false;
    public bool esMayorDeEdad = false;
    public bool tieneIne = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    
    void Prueba()
    {
        // el simbolo de dos puntos " : " tambien se puede usar como else if

        //1
        Debug.Log(esMayorDeEdad ? puedePasar = true : tieneIne ? puedePasar = true : "No puedes pasar");
        
        //2
        Debug.Log(esMayorDeEdad ? "puedes pasar, eres mayor de edad" : tieneIne ? "puedes pasar, tienes Ine" : "No puedes pasar"); 

        //3
        bool tengoLos2 = esMayorDeEdad && tieneIne;
        Debug.Log(tengoLos2 ? "Puedes pasar " : "No puedes pasar");

        //4
        Debug.Log(esMayorDeEdad && tieneIne ? "Puedes pasar " : "No puedes pasar");


    }

    private void FixedUpdate()
    {
        Move();
        Prueba();

    }

    private void Move()
    {
        rb.linearVelocity = new Vector3(HorizontalMove(), 0, VerticalMove()) * ActualSpeed();
    }

    private float ActualSpeed()
    {
        return IsRunning() ? runSpeed : IsCrouching() ? crouchSpeed : walkSpeed;
    }

    private float HorizontalMove()
    {
        return Input.GetAxis("Horizontal");
    }

    private float VerticalMove()
    {
        return Input.GetAxis("Vertical");
    }
    private bool IsRunning()
    {
        return Input.GetKey(KeyCode.LeftShift);
    }

    private bool IsCrouching()
    {
        return Input.GetKey(KeyCode.LeftControl);
    }
}
