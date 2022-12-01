using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 2.5f;
    public float rotateSpeed = 15f;
    public float rotationAmount = 8f;

    public float leftBorn = 2f;
    public float rightBorn = -2.5f;

    public static  bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) {
            if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") < 0) && transform.position.z < leftBorn)
            {    
                rb.velocity = new Vector3(0, 0, 1) * speed;
                transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, -rotationAmount, 0)), rotateSpeed*Time.deltaTime);
            }
            else if ((Input.GetAxis("Vertical") < 0 || Input.GetAxis("Horizontal") > 0) && transform.position.z > rightBorn)
            {       
                rb.velocity = new Vector3(0, 0, -1) * speed;
                transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, rotationAmount, 0)), rotateSpeed*Time.deltaTime);
            }
            else {
                transform.rotation =  Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), rotateSpeed*Time.deltaTime);
            }

            if (transform.position.z > leftBorn) {
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, leftBorn), rotateSpeed*Time.deltaTime);
            }
            else if (transform.position.z < rightBorn){
                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y, rightBorn), rotateSpeed*Time.deltaTime);
            }
        }
    }

    public void disableMovemement () {
        canMove = false;
    }
    public void enableMovemement () {
        canMove = true;
    }
}
