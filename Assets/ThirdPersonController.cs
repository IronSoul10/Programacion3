using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    CharacterController ctrllr;

    public Transform camara;

    float movX;
    float movZ;

    public float vel;

    public float suavisado = 0.1f;
    public float turnSmooth;

    public bool isGrounded = true;
    private Vector3 velY;

    [SerializeField]
    private float gravedad;
    [SerializeField] 
    private float speedJump;
    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private float radio;
    [SerializeField]
    private Transform groundCheck;

    void Start()
    {
        ctrllr = GetComponent<CharacterController>();
    }


    void Update()
    {
       isGrounded =Physics.CheckSphere(groundCheck.position,radio,whatIsGround);

        movX = Input.GetAxis("Horizontal");
        movZ = Input.GetAxis("Vertical");

        Vector3 mov = new(movX,0,movZ);

        if (isGrounded)
        {
            velY.y = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded == false)
        {
            velY.y = speedJump;
        }

        if (mov.magnitude > 0.1)
        {
            // enontrando el angulo deseado para el movimiento
            float targetAngule = Mathf.Atan2(movX, movZ) * Mathf.Rad2Deg + camara.eulerAngles.y;

            //angulo suave
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngule, ref turnSmooth, suavisado);

            //
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 movimiento = Quaternion.Euler(0, targetAngule, 0) * Vector3.forward;

            ctrllr.Move(movimiento * Time.deltaTime * vel);
        }
        velY.y += Time.deltaTime * gravedad;
        ctrllr.Move(velY*Time.deltaTime);
    }
}
