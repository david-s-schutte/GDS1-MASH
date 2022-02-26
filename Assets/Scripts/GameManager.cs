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
        helicopterPatients.text = "Soldiers in Helicopter: " + patientCount;
    }
    //Updates patientsRescued and sets the hospitalPatients text to match
    public void SetHospitalPatientsText(int patientCount){
        patientsRescued += patientCount;
        hospitalPatients.text = "Soldiers in Hospital: " + patientsRescued;
    }

    void Start()
    {
        //Define soldiers, trees, player and disable the end text
        soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        trees = GameObject.FindGameObjectsWithTag("Tree");
        hospitals = GameObject.FindGameObjectsWithTag("Finish");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        endText.enabled = false;
    }

    void Update()
    {
        //Allows the reset function
        if(Input.GetKeyDown("r")){
            SceneManager.LoadScene(1);
        }

        /*Win condition - if patients rescued is equal to the amount of soldiers in the game:
            - displays the winner text
            - disables input on the player*/ 
        if(patientsRescued == soldiers.Length){
            endText.enabled = true;
            endText.text = "You Win!";
            player.playerIsDead = true;
        }
        /*Lose condition - if the player crashes into a tree:
            - makes sure that the player hasn't already finished the game
            - enables the loser text*/
        else if(player.playerIsDead){
            if(!endText.enabled){
                endText.enabled = true;
                endText.text = "GAME OVER";
                player.GetRigidBody().gravityScale = 1.0f;
                player.GetRigidBody().freezeRotation = false;
            }
        }
    }

    public void AnimateButton()
    {
        if (graphicSwitch.GetComponent<Animator>())
        {
            graphicSwitch.GetComponent<Animator>().enabled = true;
        }
        
    }

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

            for(int i = 0; i < trees.Length; i++)
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
}
