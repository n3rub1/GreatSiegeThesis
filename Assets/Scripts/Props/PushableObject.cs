using UnityEngine;

public class PushableObject : MonoBehaviour
{
    public float pushSpeed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            Vector2 moveDir = new Vector2(moveX, moveY).normalized;

            if (moveDir != Vector2.zero)
            {
                rb.velocity = moveDir * pushSpeed;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rb.velocity = Vector2.zero;
        }
    }
}
