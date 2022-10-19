using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector2 movement;
    private void Update()
    {
        if (true)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();

            if (Input.GetButton("Sprint")) movement *= 1.5f;
            rb.MovePosition(rb.position + movement * 5.0f * Time.deltaTime);
        }
    }
    public Vector2 getMovement() { return movement; }
}
