using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneMovement : MonoBehaviour
{
    Rigidbody rb;
    public static float speed = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector3(-0.5f, 0,0 ));
        //limit speed to 2f
        if (rb.velocity.magnitude > speed)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
        //delete the object when it goes out of the screen
        if (transform.position.x < -8.3f)
        {
            Destroy(gameObject);
        }
    }
}
