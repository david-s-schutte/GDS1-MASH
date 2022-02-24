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
    public AudioClip crashClip;
    private SpriteRenderer renderer;
    private Animator animator;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player isn't dead or hasn't won yet
        if(!playerIsDead){
            //Get the input of the player
            float horizontalDirection = Input.GetAxis("Horizontal");
            float verticalDirection = Input.GetAxis("Vertical");
            //Move the player in the given direction
            transform.Translate(new Vector2(horizontalDirection, verticalDirection) 
                                * flySpeed * Time.deltaTime);
            //Update the player's animation based on input
            renderer.flipX = horizontalDirection < 0;
            Debug.Log(horizontalDirection);
            animator.SetFloat("flySpeed", horizontalDirection);
        }  
    }

    void OnCollisionEnter2D(Collision2D other){
        //If the player collides with a tree they die
        if(other.gameObject.tag == "Tree"){
            playerIsDead = true;
            pickupSFX.clip = crashClip;
            pickupSFX.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        //If the player collides with a soldier they need to be picked up
        if(other.gameObject.tag == "Soldier" && patientCapacity < patientLimit && !playerIsDead){
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

    public Rigidbody2D GetRigidBody(){
        return rb;
    }
}
