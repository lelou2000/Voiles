using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCon : MonoBehaviour
{
    public GameObject boat;
    public Manager manager;
    public MusicManager musicManager;
    bool contact = false;
    bool boo = false;
    Collider cd;
    float  time = 0;
    Rigidbody rb;

    Movement mv;

    // Start is called before the first frame update
    void Start()
    {
        cd = boat.GetComponent<Collider>();
        rb = boat.GetComponent<Rigidbody>();
        mv = boat.GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        //recover all the Gameobjects with the tag "stone"
        GameObject[] stones = GameObject.FindGameObjectsWithTag("Finish");
        foreach(GameObject obj in stones)
        {
            if(cd.bounds.Intersects(obj.GetComponent<Collider>().bounds))
            {
                boo = true;
            }
            //detect if the boat is in the collider of the stone
            if (boo)
            {
                mv.disableMovemement();
                boat.GetComponent<Rigidbody>().AddForce(new Vector3(-StoneMovement.speed, 0, 0));
                
                if (rb.velocity.magnitude > StoneMovement.speed)
                    {
                        rb.velocity = rb.velocity.normalized * StoneMovement.speed;
                    }
                manager.StPause();
                if(boat.transform.position.x <-10)
                {
                    contact = true;
                    boat.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    time = Time.time;
                    boo = false;
                }
                
                
            }
        }
        if(contact)
            {
                StartCoroutine(manager.slowWave(2.5f));
                StartCoroutine(musicManager.restartMusic(2.5f));
                if(GameObject.FindGameObjectsWithTag("Finish").Length == 0)
                {
                    boat.GetComponent<Rigidbody>().AddForce(new Vector3(2, 0, 0));
                    if(boat.transform.position.x >0)
                    {
                        mv.enableMovemement();
                        boat.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        manager.StStart();
                        contact = false;
                    }
                }
            }
        
    }
}
