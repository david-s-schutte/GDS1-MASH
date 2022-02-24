using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Gameplay Variables
    [SerializeField] private float flySpeed = 4.0f;
    public int patientCapacity = 0;
    private int patientLimit = 3;
    GameObject[] patients;
    public bool playerIsDead = false;

    //External References
    private Rigidbody2D rb;
    [SerializeField] private GameManager gameManager;
    public AudioSource pickupSFX;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player isn't dead or hasn't won yet
        if(!playerIsDead){
            //Move them left if they press the left key
            if(Input.GetKey("left")){
                transform.Translate(new Vector2(-1,0) * flySpeed * Time.deltaTime);
            }
            //Move them right if they press the right key
            if(Input.GetKey("right")){
                transform.Translate(new Vector2(1,0) * flySpeed * Time.deltaTime);
            }
            //Move them down if they press the down key
            if(Input.GetKey("down")){
                transform.Translate(new Vector2(0,-1) * flySpeed * Time.deltaTime);
            }
            //Move them up if they press the up key
            if(Input.GetKey("up")){
                transform.Translate(new Vector2(0,1) * flySpeed * Time.deltaTime);
            }
        }  
    }

    void OnCollisionEnter2D(Collision2D other){
        //If the player collides with a tree they die
        if(other.gameObject.tag == "Tree"){
            playerIsDead = true;
        }
        //If the player collides with a soldier they need to be picked up
        else if(other.gameObject.tag == "Soldier" && patientCapacity < patientLimit){
            patientCapacity++;
            Destroy(other.gameObject);
            pickupSFX.Play();
            gameManager.SetHelicopterPatientsText(patientCapacity);
        }
        //IF the player collides with a hospital reset the patient capacity
        else if(other.gameObject.tag == "Finish"){
            gameManager.SetHospitalPatientsText(patientCapacity);
            patientCapacity = 0;
            gameManager.SetHelicopterPatientsText(patientCapacity);
        }
    }
}
