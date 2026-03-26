using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownPlayer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.5f;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 input;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        rb.gravityScale = 0f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        input = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (input.sqrMagnitude > 1f)
            input.Normalize();

        bool isMoving = input.sqrMagnitude > 0.01f;
        anim.SetBool("isMoving", isMoving);

        if (isMoving)
        {
            anim.SetFloat("moveX", input.x);
            anim.SetFloat("moveY", input.y);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = input * moveSpeed;
    }
}
