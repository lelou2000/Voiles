using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public MusicManager musicManager;
    public Manager manager;
    public GameObject gameCamera;
    public GameObject titleScreen;
    public GameObject tutorialScreen;
    public Movement boatMovement;

    // Events
    private bool gameLaunched = false;
    private bool gameStarted = false;

    void Awake()
    {
        titleScreen.SetActive(true);
        tutorialScreen.SetActive(false);
        boatMovement.disableMovemement();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameLaunched) {
            if (Input.GetButton("Submit")) {
                launchGame();
            }
        }
        else if (!gameStarted && (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0 )) {
            startGame();
        }
    }

    void launchGame() {
        musicManager.playStart();
        gameCamera.GetComponent<Camera>().switchToGameView();
        titleScreen.SetActive(false);
        tutorialScreen.SetActive(true);
        boatMovement.enableMovemement();
        StartCoroutine(musicManager.windFade(2f));
        gameLaunched = true;
    }
    
    void startGame () {
        tutorialScreen.SetActive(false);
        manager.StStart();
        StartCoroutine(musicManager.playMusic(2f));
        gameStarted = true;
    }
}
