using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    private Shot shot;
    private void Start()
    {
        shot = FindObjectOfType<Shot>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PowerUpCD(8f));
        }
    }

    public IEnumerator PowerUpCD(float delay)
    {
        shot.tipoPowerUp = "tiroIce";
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(delay);
        shot.tipoPowerUp = "tiroSimple";

        Destroy(gameObject, 5);
    }
}
