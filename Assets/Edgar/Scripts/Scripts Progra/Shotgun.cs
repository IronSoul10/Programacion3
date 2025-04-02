using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class Shotgun : Weapon
    {
        float angle;
        [SerializeField] float angleRange;
        bool reload= false;

        protected internal override void Shoot()
        {
            if (actualAmmo > 0)
            {
                base.Shoot();
                BulletDispersion();
                actualAmmo--;
                Debug.Log("Escopetazo");
            }
            if (actualAmmo == 0)
            {
                Reload();
                magazineSize--;
            }
            if (magazineSize == 0 && reload)
            {
                actualAmmo = -1;
                magazineSize = 0;
                Debug.Log("Se acabaron las reservas");
            }
        }

        protected internal void BulletDispersion()
        {
            angle = Random.Range(-angleRange, angleRange);
            Quaternion Perdigon = Quaternion.Euler(new Vector3(angle, angle, 0));
        }
        protected internal override void Reload()
        {
            StartCoroutine(ShotgunReload());
        }

        IEnumerator ShotgunReload()
        {
            float reloadTimeElapsed = 0f;

            while (reloadTimeElapsed < reloadTime)
            {
                reloadTimeElapsed += Time.deltaTime;
                actualAmmo = Mathf.FloorToInt(Mathf.Lerp(0, maxAmmo, reloadTimeElapsed / reloadTime));
                yield return null; 
                actualAmmo = maxAmmo;
                reload = true; // compruebo si esta recargando para el magnazine size
            }
        }
    }


///<sumary>///
///Mathf.FlorToInt me asegura que el resultado sea un entero
///Mathf.Lerp intepola desde 0 hasta munMax en un tiempo
///

