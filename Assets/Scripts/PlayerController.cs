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

    //External References
    private Rigidbody2D rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("left")){
            transform.Translate(new Vector2(-1,0) * flySpeed * Time.deltaTime);
            Debug.Log("left");
        }

        if(Input.GetKey("right")){
            transform.Translate(new Vector2(1,0) * flySpeed * Time.deltaTime);
            Debug.Log("right");
        }

        if(Input.GetKey("down")){
            transform.Translate(new Vector2(0,-1) * flySpeed * Time.deltaTime);
            Debug.Log("down");
        }

        if(Input.GetKey("up")){
            transform.Translate(new Vector2(0,1) * flySpeed * Time.deltaTime);
            Debug.Log("up");
        }
    }
}
