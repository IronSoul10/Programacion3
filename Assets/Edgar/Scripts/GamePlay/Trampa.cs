using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trampa : MonoBehaviour
{
    [SerializeField] Transform respawn;
    [SerializeField] float radio;
    [SerializeField] LayerMask layer;
    [SerializeField] CharacterController characterController;
    [SerializeField] VidaPlayer player;

    private void Update()
    {
        if (Deteccion())
        {
            StartCoroutine(player.SubtractHealth());
        }
    }

    bool Deteccion()
    {
        return Physics.CheckSphere(transform.position, radio, layer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }
}
