using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move: MonoBehaviour
{
    public float speed = 5;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<ConflictControl>().notgamescene)
        {
            float moveY = 0;
            float moveX = Input.GetAxis("Horizontal") * speed;
            float moveZ = Input.GetAxis("Vertical") * speed;
            if (Input.GetKey(KeyCode.Space))
                moveY = 1 * speed;
            else if (Input.GetKey(KeyCode.LeftAlt))
                moveY = -1 * speed;

            Vector3 move = transform.right * moveX + transform.forward * moveZ + transform.up * moveY;
            move *= Time.deltaTime;
            transform.position += move;
        }

    }
}
