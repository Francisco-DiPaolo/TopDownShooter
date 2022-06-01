using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{

    [Header("Shooting")]
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] GameObject bulletIcePrefab;
    [SerializeField] float bulletForce = 20f;
    [SerializeField] float bulletForceIce = 15f;
    private bool canShot;

    public string tipoPowerUp;
    void Start()
    {
        canShot = true;

        tipoPowerUp = "tiroSimple";
    }

    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            switch (tipoPowerUp)
            {
                case "tiroSimple":
                    Shooting(bulletPrefab, bulletForce);
                    break;

                case "tiroIce":
                    Shooting(bulletIcePrefab, bulletForceIce);
                    break;

                default:
                    break;
            }
        }
    }

    void Shooting(GameObject bulletPrefab, float bulletForce)
    {
        if (!canShot) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
        rbBullet.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        StartCoroutine(ShotCD(0.1f));

        Destroy(bullet, 2);
    }

    public IEnumerator ShotCD(float delay)
    {
        canShot = false;
        yield return new WaitForSeconds(delay);
        canShot = true;
    }
}
