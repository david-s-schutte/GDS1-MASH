using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    //Gameplay variables
    public float timeRemaining;
    private GameObject gameManager;
    private bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise gameplay variables
        gameStarted = false;
        gameManager = GameObject.FindWithTag("GameController");
        timeRemaining = 120.0f;
        //Add OnSceneReload as a delegate of sceneLoaded
        SceneManager.sceneLoaded += OnSceneReload;
    }

    // Update is called once per frame
    void Update()
    {
        //If the active scene is the main game start the game and find the controller
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) && !gameStarted)
        {
            gameManager = GameObject.FindWithTag("GameController");
            gameStarted = true;
        }

        //If the game has started and the player isn't dead
        if (gameStarted && !gameManager.GetComponent<GameManager>().player.playerIsDead)
        {
            //Take time off of the timer
            timeRemaining -= Time.deltaTime;
            //Kill the player if the timer reaches 0
            if(timeRemaining <= 0.0f)
            {
                timeRemaining = 0.0f;
                gameManager.GetComponent<GameManager>().player.playerIsDead = true;
            }
            //Update the UI to match time remaining
            if (gameManager)
            {
                gameManager.GetComponent<GameManager>().UpdateTimeRemaining(timeRemaining);
            }
        }
    }
    
    //Reset references everytime the scene is reloaded
    void OnSceneReload(Scene scene, LoadSceneMode mode)
    {
        if(SceneManager.GetActiveScene() == scene)
        {
            gameStarted = false;
            gameManager = GameObject.FindWithTag("GameController");
            timeRemaining = 120.0f;
        }
    }
}
