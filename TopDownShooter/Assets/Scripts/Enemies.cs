using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemies : MonoBehaviour
{
    [SerializeField] float vidaEnemie;
    NavMeshAgent navMeshAgent;
    Transform destination;

    GameController gameController;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject enemieCold;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        destination = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        spriteRenderer = FindObjectOfType<SpriteRenderer>();

        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;

        enemieCold.SetActive(false);
    }

    private void Update()
    {
        Destruir();

        if (playerMovement.boolNav) InvokeRepeating(nameof(SetDestination), 0f, 0.5f);
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

    private void RecibirDañoIce()
    {
        vidaEnemie -= 75;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            RecibirDaño();
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("BulletIce"))
        {
            RecibirDañoIce();
            Destroy(collision.gameObject);

            enemieCold.SetActive(true);
            navMeshAgent.speed = 0.7f;
        }
    }

    private void SetDestination()
    {
        navMeshAgent.SetDestination(destination.position);
    }
}
