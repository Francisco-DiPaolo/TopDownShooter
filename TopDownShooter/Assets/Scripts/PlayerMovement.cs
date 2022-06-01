using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 5f;
    Vector2 movement;

    private Rigidbody2D rb;

    [Header("Camera")]
    [SerializeField] Camera cam;
    Vector2 mousePos;

    [Header("Player")]
    public int vidaPlayer;
    public bool canTakeDamage;
    SpriteRenderer spriteRenderer;

    public bool boolNav;

    [Header("Texto")]
    public Text textCartel;
    protected GameController gameController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        boolNav = false;

        textCartel.text = "";

        spriteRenderer = GetComponent<SpriteRenderer>();
        canTakeDamage = true;

        gameController = FindObjectOfType<GameController>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (!canTakeDamage)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
        }

        if (vidaPlayer <= 0)
        {
            gameController.Lose();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;

        // Atan2 --> El valor de retorno es el ángulo entre el eje x y un vector 2D que comienza en cero y termina en (x,y).
        // Por lo tanto tenemos que darle los dos ejes, y luego nos devolvera el angulo en radianes,
        // a los cuales los pasamos a deg, con Mathf.Rad2Deg.
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemie"))
        {
            LoseHealth();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemie"))
        {
            boolNav = true;
        }

        if (collision.gameObject.CompareTag("Cartel"))
        {
            textCartel.text = "' No molestar  -.- '";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cartel"))
        {
            textCartel.text = "";
        }
    }

    private void LoseHealth()
    {
        vidaPlayer--;
        gameController.SetTextLife();

        StartCoroutine(DamageCD(2));
    }

    public IEnumerator DamageCD(float delay)
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(delay);
        canTakeDamage = true;
        spriteRenderer.enabled = true;
    }
}
