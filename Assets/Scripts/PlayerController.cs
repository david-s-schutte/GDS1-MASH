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
    public AudioSource sfx;
    public AudioSource helicopterNoise;
    public AudioClip dropoffClip;
    public AudioClip pickupClip;
    public AudioClip crashClip;
    private SpriteRenderer renderer;
    private Animator animator;

    //Overall Variables
    public GameObject gameSettings;

    void Awake()
    {
        //Initialise external variables
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
        gameSettings = GameObject.FindGameObjectWithTag("GameSettings");
    }

    private void Start()
    {
        //Disable SFX if opted into on the menu
        if (gameSettings)
        {
            sfx.enabled = gameSettings.GetComponent<GameSettings>().GetSFXSetting();
            helicopterNoise.enabled = gameSettings.GetComponent<GameSettings>().GetSFXSetting();
        }
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
        }
        else{
            animator.enabled = false;
            helicopterNoise.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        //If the player collides with a tree they die
        if(other.gameObject.tag == "Tree"){
            playerIsDead = true;
            sfx.clip = crashClip;
            sfx.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (!playerIsDead)
        {
            //If the player collides with a soldier they need to be picked up
            if (other.gameObject.tag == "Soldier" && patientCapacity < patientLimit && !playerIsDead)
            {
                patientCapacity++;
                Destroy(other.gameObject);
                sfx.clip = pickupClip;
                sfx.Play();
                gameManager.SetHelicopterPatientsText(patientCapacity);
                gameSettings.GetComponent<ScoreManager>().AddToScore(100);

            }
            //IF the player collides with a hospital reset the patient capacity
            else if (other.gameObject.tag == "Finish")
            {
                if(patientCapacity > 0)
                {
                    sfx.clip = dropoffClip;
                    sfx.Play();
                    gameManager.SetHospitalPatientsText(patientCapacity);
                    gameSettings.GetComponent<ScoreManager>().AddToScore(100 * patientCapacity);
                    patientCapacity = 0;
                    gameManager.SetHelicopterPatientsText(patientCapacity);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!playerIsDead){
            //IF the player collides with a hospital reset the patient capacity
            if (collision.gameObject.tag == "Finish"){
                sfx.clip = pickupClip;
            }
        }
    }

    public Rigidbody2D GetRigidBody(){
        return rb;
    }
}
