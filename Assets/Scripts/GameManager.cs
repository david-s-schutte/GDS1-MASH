using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Gameplay variables
    public int patientsRescued;
    [SerializeField] private Text helicopterPatients;
    [SerializeField] private Text hospitalPatients;
    [SerializeField] private Text endText;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text timeRemaining;
    [SerializeField] private Image darkenGame;
    public Button graphicSwitch;
    [SerializeField] private bool isAtariGraphics = false;
    
    //External References
    public GameObject[] soldiers;
    public GameObject[] trees;
    public GameObject[] hospitals;
    public PlayerController player;
    public GameObject bgTiles;

    //Sprites
    public Sprite playerOriginalSprite;
    public Sprite playerAtariSprite;
    public Sprite treeOriginalSprite;
    public Sprite treeAtariSprite;
    public Sprite soldierOriginalSprite;
    public Sprite soldierAtariSprite;
    public Sprite hospitalOriginalSprite;
    public Sprite hospitalAtariSprite;
    public Sprite atariNonactiveSprite;
    public Sprite atariActiveSprite;

    //Sets the helicopterPatients text to match the player's patientCapacity
    public void SetHelicopterPatientsText(int patientCount){
        helicopterPatients.text = "x " + patientCount;
    }
    //Updates patientsRescued and sets the hospitalPatients text to match
    public void SetHospitalPatientsText(int patientCount){
        patientsRescued += patientCount;
        hospitalPatients.text = "x " + patientsRescued;
    }
    //Updates the scoreText from Score Manager
    public void UpdateScoreText(int currentScore)
    {
        scoreText.text = "" + currentScore;
    }
    //Updates the timeRemaining text from Game Timer
    public void UpdateTimeRemaining(float time)
    {
        //Round text
        timeRemaining.text = "" + Mathf.Floor(time);
        //Set text colour based on given time
        if (time < 40.0f && time > 20.0f)
        {
            timeRemaining.color = Color.yellow;
        }
        else if (time < 20.0f && time > 0.0f)
        {
            timeRemaining.color = Color.red;
        }
        //Change the text to a phrase if time ran out
        if(time <= 0.0f)
        {
            timeRemaining.text = "Time Out!";
        }
    }

    void Start()
    {
        //Define soldiers, trees, player and disable the end text
        soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        trees = GameObject.FindGameObjectsWithTag("Tree");
        hospitals = GameObject.FindGameObjectsWithTag("Finish");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        endText.enabled = false;
        darkenGame.enabled = false;
        //Maintain score in UI
        ScoreManager currentScoreManager = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<ScoreManager>();
        if (currentScoreManager)
        {
            UpdateScoreText(currentScoreManager.score);
        }
    }

    void Update()
    {
        //Allows the reset function
        if(Input.GetKeyDown("r")){
            ScoreManager currentScoreManager = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<ScoreManager>();
            if (currentScoreManager)
            {
                currentScoreManager.playerRestart = true;
            }
            if (GameObject.FindGameObjectWithTag("GameSettings").GetComponent<GameSettings>().GetMusicSetting())
            {
                GameObject.FindGameObjectWithTag("GameSettings").GetComponent<AudioSource>().enabled = true;
            } 
            SceneManager.LoadScene(1);
        }

        //Exits to the main menu
        if (Input.GetKeyDown("escape") && !player.playerIsDead)
        {
            SceneManager.LoadScene(0);
        }

        /*Win condition - if patients rescued is equal to the amount of soldiers in the game:
            - displays the winner text
            - disables input on the player*/ 
        if(patientsRescued == soldiers.Length){
            endText.enabled = true;
            ScoreManager currentScoreManager = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<ScoreManager>();
            GameTimer currentTimer = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<GameTimer>();
            endText.text = "Round Completed! \nTime Bonus: " + Mathf.FloorToInt(currentTimer.timeRemaining) *4;
            if (!player.playerIsDead)
            {
                currentScoreManager.score += (Mathf.FloorToInt(currentTimer.timeRemaining) * 4);
            }
            player.playerIsDead = true;
            darkenGame.enabled = true;
            
            Invoke("RestartGame", 1.0f);
            
        }
        /*Lose condition - if the player crashes into a tree:
            - makes sure that the player hasn't already finished the game
            - enables the loser text*/
        else if(player.playerIsDead){
            if(!endText.enabled){
                endText.enabled = true;
                ScoreManager currentScoreManager = GameObject.FindGameObjectWithTag("GameSettings").GetComponent<ScoreManager>();
                darkenGame.enabled = true;
                GameObject.FindGameObjectWithTag("GameSettings").GetComponent<AudioSource>().enabled = false;
                endText.text = "GAME OVER \n You Scored: " + currentScoreManager.score + "\n Press R to Restart";
                player.GetRigidBody().gravityScale = 1.0f;
                player.GetRigidBody().freezeRotation = false;
            }
        }
    }
    //Animates the graphic switch button
    public void AnimateButton()
    {
        if (graphicSwitch.GetComponent<Animator>())
        {
            graphicSwitch.GetComponent<Animator>().enabled = true;
        }
        
    }
    //Stops animating the graphic switch button and resets it to its default sprite
    public void StopAnimateButton()
    {
        if (graphicSwitch.GetComponent<Animator>())
        {
            graphicSwitch.GetComponent<Animator>().enabled = false;
        }
        graphicSwitch.image.sprite = atariNonactiveSprite;
    }

    public void SwitchGraphics()
    {
        //Switch the bool variable that controls the graphics
        isAtariGraphics = !isAtariGraphics;

        //Swap sprites to the Atari graphics when true
        if (isAtariGraphics)
        {
            graphicSwitch.image.sprite = atariActiveSprite;

            for (int i = 0; i < trees.Length; i++)
            {
                trees[i].GetComponent<SpriteRenderer>().sprite = treeAtariSprite;
            }

            for (int i = 0; i < soldiers.Length; i++)
            {
                if (soldiers[i])
                {
                    soldiers[i].GetComponent<SpriteRenderer>().sprite = soldierAtariSprite;
                }
            }

            for (int i = 0; i < hospitals.Length; i++)
            {
                hospitals[i].GetComponent<SpriteRenderer>().sprite = hospitalAtariSprite;
            }

            player.gameObject.GetComponent<SpriteRenderer>().sprite = playerAtariSprite;
            player.gameObject.GetComponent<Animator>().enabled = false;
            bgTiles.SetActive(false);
        }
        //Swap sprites to the original graphics when false
        else
        {
            graphicSwitch.image.sprite = atariNonactiveSprite;

            for (int i = 0; i < trees.Length; i++)
            {
                trees[i].GetComponent<SpriteRenderer>().sprite = treeOriginalSprite;
            }

            for (int i = 0; i < soldiers.Length; i++)
            {
                if (soldiers[i])
                {
                    soldiers[i].GetComponent<SpriteRenderer>().sprite = soldierOriginalSprite;
                }
            }

            for (int i = 0; i < hospitals.Length; i++)
            {
                hospitals[i].GetComponent<SpriteRenderer>().sprite = hospitalOriginalSprite;
            }

            player.gameObject.GetComponent<SpriteRenderer>().sprite = playerOriginalSprite;
            player.gameObject.GetComponent<Animator>().enabled = true;
            bgTiles.SetActive(true);

        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
}
