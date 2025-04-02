using UnityEngine;

public class Target : MonoBehaviour
{
   // [HideInInspector] public int targetsDestroyed;
    private TargetManager targetManager;

    private void Start()
    {
        targetManager = FindFirstObjectByType<TargetManager>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            //targetsDestroyed++;
            targetManager.IncrementTargetCount();
            Destroy(gameObject);
        }
    }
}
