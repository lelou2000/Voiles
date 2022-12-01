using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonStone : MonoBehaviour
{
    public GameObject obj;
    int time = 10;

    // Update is called once per frame
    void Update()
    {
        //create instance of obj bewteen z = -3.5 and z = 3.5 every 5 seconds
        if (Time.time > time)
        {
            Instantiate(obj, new Vector3(0, 0, Random.Range(-3.5f, 3.5f)), Quaternion.identity, transform);
            time += 5;
        }     
        
    }
}
