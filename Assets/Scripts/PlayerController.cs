using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Transform snakeBodyPrefab;
    public PlayerDirection playerDirection;
    List<Transform> snakeBodies;
    Vector3 currentPosition;
    /*
        Working code with Fixed Timestamp 0.2
        Max. Allowed Timestamp 0.33
    */
    void Start()
    {
        playerDirection = PlayerDirection.Right;
        snakeBodies = new List<Transform>();
        snakeBodies.Add(gameObject.transform);
    }

    private void Update() {
        // playerMovement();
        bodyMovement();
    }

    void bodyMovement(){
        Debug.Log("Vector2 : "+Vector2.right);
        int i = snakeBodies.Count-1;
        for(; i>0; i--){
            snakeBodies[i].position = snakeBodies[i-1].position;
        }
        transform.position = new Vector3(
            transform.position.x + Vector2.right.x,
            transform.position.y + Vector2.right.y,
            0f
        );
    }

    void playerMovement(){
        currentPosition = transform.position;
        if(playerDirection == PlayerDirection.Right){

            currentPosition.x += movementSpeed * Time.deltaTime;

        }else if(playerDirection == PlayerDirection.Left){

            currentPosition.x -= movementSpeed * Time.deltaTime;

        }else if(playerDirection == PlayerDirection.Up){

            currentPosition.y += movementSpeed * Time.deltaTime;

        }else if(playerDirection == PlayerDirection.Up){

            currentPosition.y -= movementSpeed * Time.deltaTime;

        }
    }

    void Grow(){
        Transform bodyObject = Instantiate(snakeBodyPrefab);
        bodyObject.position = snakeBodies[snakeBodies.Count-1].position;
        snakeBodies.Add(bodyObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Collided");
        if(other.gameObject.name == "Food"){
            Destroy(other.gameObject);
            Grow();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Trigger Collided");
        if(other.gameObject.name == "Food"){
            Destroy(other.gameObject);
            Grow();
        }
    }
}

public enum PlayerDirection{
    Up,
    Down,
    Right,
    Left
}