using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 1.0f;

    public Rigidbody2D rb;

    private bool bMovable = true;
    Vector2 movement;

    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    // Update is called once per frame
    private void Update()
    {
        if (bMovable)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement.Normalize();
            if (Input.GetButton("Sprint")) movement *= 1.5f;
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }
}
