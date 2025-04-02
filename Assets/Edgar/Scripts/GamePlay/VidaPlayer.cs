using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VidaPlayer : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] Transform spawn;
    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] CharacterController controller;

    
    private void Start()
    {
        UpdateHealth();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemigo"))
        {
           StartCoroutine(SubtractHealth());
        }
    }
    public IEnumerator SubtractHealth()
    {
        health -= 10;
        UpdateHealth();

        if (health <= 0)
        {
            controller.enabled = false;
            transform.position = spawn.position;
            yield return null;
            controller.enabled = true;
            health = 100;
            UpdateHealth();

        }
    }

    void UpdateHealth()
    {
        healthText.text = "" + health.ToString();
    }
}


