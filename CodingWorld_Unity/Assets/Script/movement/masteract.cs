using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masteract : MonoBehaviour
{
    // Start is called before the first frame update
    Movable mb;
    void Awake()
    {
        mb = gameObject.GetComponent<Movable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            mb.head(2, 0.5f);


        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            mb.tail(2, 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            mb.left(2, 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            mb.right(2, 0.5f);

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            mb.jump();
        }
    }
}
