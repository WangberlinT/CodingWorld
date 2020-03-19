using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loopact : MonoBehaviour
{
    Movable mo;
    bool s = false;

    // Start is called before the first frame update
    void Awake()
    {
        mo = gameObject.GetComponent<Movable>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(moveSeq());
        if (Input.GetKeyUp(KeyCode.P))
        {
            s = !s;
            if (!s)
            {
                iTween.Stop(gameObject);
            }
            Debug.Log(s);
        }
    }

    IEnumerator moveSeq()
    {
        while (s)
        {
            mo.MoveTo(mo.Forward(2), 2);
            yield return new WaitForSeconds(1.1f);
            mo.MoveTo(mo.Left(2), 2);
            yield return new WaitForSeconds(1.1f);
        }
        
        

    }

}
