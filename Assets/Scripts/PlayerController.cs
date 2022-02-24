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
    private bool playerIsDead = false;

    //External References
    private Rigidbody2D rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //If the player isn't dead
        if(!playerIsDead){
            //Move them left if they press the left key
            if(Input.GetKey("left")){
                transform.Translate(new Vector2(-1,0) * flySpeed * Time.deltaTime);
                Debug.Log("left");
            }
            //Move them right if they press the right key
            if(Input.GetKey("right")){
                transform.Translate(new Vector2(1,0) * flySpeed * Time.deltaTime);
                Debug.Log("right");
            }
            //Move them down if they press the down key
            if(Input.GetKey("down")){
                transform.Translate(new Vector2(0,-1) * flySpeed * Time.deltaTime);
                Debug.Log("down");
            }
            //Move them up if they press the up key
            if(Input.GetKey("up")){
                transform.Translate(new Vector2(0,1) * flySpeed * Time.deltaTime);
                Debug.Log("up");
            }
        }  
    }

    void OnCollisionEnter2D(Collision2D other){
        //If the player collides with a tree they die
        if(other.gameObject.tag == "Tree"){
            playerIsDead = true;
            Debug.Log("u r die");
        }
        //If the player collides with a soldier they need to be picked up
        if(other.gameObject.tag == "Soldier" && patientCapacity <= patientLimit){
            patientCapacity++;
            Destroy(other.gameObject);
            Debug.Log("patientCapacity = " + patientCapacity);
        }
    }
}
