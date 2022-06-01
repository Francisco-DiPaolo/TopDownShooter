using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] float vidaEnemie;
    Spawner spawner;
    GameController gameController;

    public bool boolYaSpawnearon = false;
    bool boolYaTermina;
    public Vector3 sizeChange;

    [SerializeField] int scoreSpawn;
    [SerializeField] int scoreScale;

    SpriteRenderer spriteRenderer;
    public bool canTakeDamage;

    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        gameController = FindObjectOfType<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boolYaTermina = false;
        canTakeDamage = true;
    }

    void Update()
    {
        if (vidaEnemie <= scoreSpawn && !boolYaSpawnearon)
        {
            StartCoroutine(DamageCD(1));

            spawner.boolTree = true;
            gameObject.transform.localScale = gameObject.transform.localScale + sizeChange;
        }
        else if (vidaEnemie <= scoreScale && !boolYaTermina)
        {
            StartCoroutine(DamageCD(1));

            gameObject.transform.localScale = gameObject.transform.localScale + sizeChange;
            boolYaTermina = true;
        }
        else if (vidaEnemie <= 0)
        {
            Destroy(gameObject);
            gameController.boolUltimateFaseBoss = true;
        }

        if (!canTakeDamage)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }
    }

    private void Destruir()
    {
        if (vidaEnemie <= 0)
        {
            Destroy(gameObject);
            gameController.EnemyDie();
        }
    }

    private void RecibirDaño()
    {
        vidaEnemie -= 100;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("BulletIce"))
        {
            RecibirDaño();
            Destroy(collision.gameObject);
        }
    }

    public IEnumerator DamageCD(float delay)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(delay);
        canTakeDamage = true;
        spriteRenderer.enabled = true;
    }
}
