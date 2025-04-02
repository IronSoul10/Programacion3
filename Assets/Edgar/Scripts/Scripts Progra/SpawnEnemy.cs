using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemigoPrefab;
    public Transform puntoInicio; // Cambiado a público para asignar en el Inspector
    public float velEnemigo;
    bool spawnActivo = true;
    [SerializeField] private float posicionZ;

    // [HideInInspector]
    public Vector3 rotacion;

    void Update()
    {
        SpawnEnemigo();
    }

    void SpawnEnemigo()
    {
        if (spawnActivo)
        {
            float randomZ = Random.Range(-posicionZ, posicionZ);
            Vector3 spawnPosition = new Vector3(puntoInicio.localPosition.x, puntoInicio.localPosition.y, puntoInicio.localPosition.z + randomZ);

            GameObject clone = Instantiate(enemigoPrefab, spawnPosition, Quaternion.identity);
            Rigidbody rb = clone.GetComponent<Rigidbody>();
            rb.AddForce(Vector3.forward * velEnemigo);
            Destroy(clone,10f);
            StartCoroutine(ColdDown());
        }
    }

    IEnumerator ColdDown()
    {
        spawnActivo = false;
        yield return new WaitForSeconds(0.1f);
        spawnActivo = true;
    }
}


