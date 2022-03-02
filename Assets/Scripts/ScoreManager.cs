using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //Gameplay variables
    public int score;
    private GameObject gameManager;
    private bool gameStarted;
    public bool playerRestart;

    // Start is called before the first frame update
    void Start()
    {
        //Initialise gameplay variables
        gameStarted = false;
        gameManager = GameObject.FindWithTag("GameController");
        score = 0;
        playerRestart = false;
        //Add OnSceneReload as a delegate of sceneLoaded
        SceneManager.sceneLoaded += OnSceneReload;
    }

    // Update is called once per frame
    void Update()
    {
        //If the current scene is the main game start the game and initialise the controller
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) && !gameStarted)
        {
            gameManager = GameObject.FindWithTag("GameController");
            gameStarted = true;
        }
    }

    //Add to the current score and update the UI text
    public void AddToScore(int amtToAdd)
    {
        score += amtToAdd;
        if (gameManager)
        {
            gameManager.GetComponent<GameManager>().UpdateScoreText(score);
        }
    }

    //Reset references whenever the scene is reloaded
    void OnSceneReload(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene() == scene)
        {
            gameStarted = false;
            gameManager = GameObject.FindWithTag("GameController");
            if (playerRestart)
            {
                score = 0;
                playerRestart = false;
            }
        }
    }
}
