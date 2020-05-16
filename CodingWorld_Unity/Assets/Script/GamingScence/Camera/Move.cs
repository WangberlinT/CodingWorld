using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move: MonoBehaviour
{
    public float speed = 5;
    public float jump_speed = 5;
    private Rigidbody rigidbody;
    private bool isJump = false;
    void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameObject.GetComponent<ConflictControl>().notgamescene)
        {
            float moveY = 0;
            float moveX = Input.GetAxis("Horizontal") * speed;
            float moveZ = Input.GetAxis("Vertical") * speed;

            if (isJump)
                Jump();
            else if (Input.GetKey(KeyCode.LeftAlt))
                moveY = -1 * speed;

            Vector3 move = transform.right * moveX + transform.forward * moveZ + transform.up * moveY;
            move *= Time.fixedDeltaTime;
            transform.position += move;
        }

    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
            isJump = true;
    }

    void Jump()
    {
        rigidbody.velocity = new Vector3(0, jump_speed, 0);
        isJump = false;
    }
}
