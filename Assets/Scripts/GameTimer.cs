using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining;
    private GameObject gameManager;
    private bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        gameManager = GameObject.FindWithTag("GameController");
        Debug.Log("Game Controller in Timer = " + gameManager);
        timeRemaining = 120.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("From Timer: manager = " + gameManager + ", started = " + gameStarted);

        if (gameStarted && Input.GetKeyDown("r"))
        {
            gameStarted = false;
            timeRemaining = 120.0f;
            Debug.Log("Timer Restarted");
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) && !gameStarted)
        {
            gameManager = GameObject.FindWithTag("GameController");
            gameStarted = true;
        }

        if (gameStarted && !gameManager.GetComponent<GameManager>().player.playerIsDead)
        {
            timeRemaining -= Time.deltaTime;
            if(timeRemaining <= 0.0f)
            {
                timeRemaining = 0.0f;
                gameManager.GetComponent<GameManager>().player.playerIsDead = true;
            }
            if (gameManager)
            {
                gameManager.GetComponent<GameManager>().UpdateTimeRemaining(timeRemaining);
            }
        }
    }
}
