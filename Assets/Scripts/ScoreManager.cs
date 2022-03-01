using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score;
    private GameObject gameManager;
    private bool gameStarted;

    // Start is called before the first frame update
    void Start()
    {
        gameStarted = false;
        gameManager = GameObject.FindWithTag("GameController");
        Debug.Log("Game Controller in Score Manager = " + gameManager);
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1) && !gameStarted)
        {
            gameManager = GameObject.FindWithTag("GameController");
            Debug.Log("Game Controller in Score Manager = " + gameManager);
            gameStarted = true;
        }
    }

    public void AddToScore(int amtToAdd)
    {
        score += amtToAdd;
        if (gameManager)
        {
            gameManager.GetComponent<GameManager>().UpdateScoreText(score);
        }
        Debug.Log(score);
    }
}
