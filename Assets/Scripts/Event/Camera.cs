using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    
    public float transitionSpeed;
    public Transform currentView;

    public Transform gameView;


    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, transitionSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, currentView.rotation, transitionSpeed * Time.deltaTime);
    }

    public void switchToGameView()
    {
        currentView = gameView;
    }
}