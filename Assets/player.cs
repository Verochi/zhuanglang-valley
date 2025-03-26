using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;//ÒÆËÙ
    public Animator anime;
    private Rigidbody2D rb;
    private Vector2 movement;

    private void Awake()
    {
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        movement = new Vector2(x, y).normalized;//¹éÒ»»¯

        if (movement.magnitude > 0)
        {
            anime.SetBool("iswalking", true);
            anime.SetFloat("horizontal", x);
            anime.SetFloat("vertical", y);
        }
        else
        {
            anime.SetBool("iswalking", false);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }
}
