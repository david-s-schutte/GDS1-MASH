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
    
    //External References
    public GameObject[] soldiers;
    public PlayerController player;
    public Sprite atariButtonSprite;

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
        //Define soldiers, player and disable the end text
        soldiers = GameObject.FindGameObjectsWithTag("Soldier");
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        endText.enabled = false;
    }

    void Update()
    {
        //Allows the reset function
        if(Input.GetKeyDown("r")){
            SceneManager.LoadScene(0);
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
                endText.text = "Game Over";
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
        graphicSwitch.image.sprite = atariButtonSprite;
    }
}
