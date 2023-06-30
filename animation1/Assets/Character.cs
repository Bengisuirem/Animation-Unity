using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed = 3.4f;
    public float jumpHeight = 12.5f;
    public float gravityScale = 1.5f;
    private Rigidbody2D body;
    private Animator anim;
    private bool Grounded;

    private void Awake()
    {
        //nesne oluştur
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

        //flip player when moving right-left
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;

        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.W) && Grounded)
            Jump();

        //set animator parameters
        anim.SetBool("isRun", horizontalInput != 0);
        anim.SetBool("Grounded", Grounded);
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("Jump");
        Grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ground")
            Grounded = true;
    }

}
