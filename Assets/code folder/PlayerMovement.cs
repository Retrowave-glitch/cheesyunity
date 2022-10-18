using UnityEngine;
public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1.0f;

    public Rigidbody2D rb;

    private bool bMovable = true;
    private Vector2 movement;
    private void Update()
    {
        if (bMovable)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();

            if (Input.GetButton("Sprint")) movement *= 1.5f;

            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
    }
    public Vector2 getMovement() { return movement; }
}
