using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public Transform snakeBodyPrefab;
    public PlayerDirection playerDirection;
    List<Transform> snakeBodies;
    Vector3 currentPosition,
            boddyAppliedPosition;
    /*
        Working code with Fixed Timestamp 0.2
        Max. Allowed Timestamp 0.33
    */
    void Start()
    {
        Application.targetFrameRate = 40;
        playerDirection = PlayerDirection.Right;
        snakeBodies = new List<Transform>();
        snakeBodies.Add(gameObject.transform);
    }

    private void Update() {
        if(Input.GetKey(KeyCode.D) && playerDirection != PlayerDirection.Left){
            playerDirection = PlayerDirection.Right;
        }else if(Input.GetKey(KeyCode.S) && playerDirection != PlayerDirection.Up){
            playerDirection = PlayerDirection.Down;
        }else if(Input.GetKey(KeyCode.A) && playerDirection != PlayerDirection.Right){
            playerDirection = PlayerDirection.Left;
        }else if(Input.GetKey(KeyCode.W) && playerDirection != PlayerDirection.Down){
            playerDirection = PlayerDirection.Up;
        }
    }

    private void FixedUpdate() {
        playerMovement();
        for(int i = snakeBodies.Count-1; i>0; i--){
            boddyAppliedPosition = snakeBodies[i-1].position;

            if(playerDirection == PlayerDirection.Right){
                boddyAppliedPosition.x -= (float)0.5;
            }else if(playerDirection == PlayerDirection.Left){
                boddyAppliedPosition.x += (float)0.5;
            }else if(playerDirection == PlayerDirection.Up){
                boddyAppliedPosition.y -= (float)0.5;
            }else if(playerDirection == PlayerDirection.Down){
                boddyAppliedPosition.y += (float)0.5;
            }

            // boddyAppliedPosition.x -= (float)0.5;
            snakeBodies[i].position = boddyAppliedPosition;
            // Debug.Log("Body "+i+" Position : "+snakeBodies[i].transform.position,snakeBodies[i]);
        }

        transform.position = currentPosition;
        // Debug.Log("Head Position : "+snakeBodies[0].transform.position,snakeBodies[0]);
    }

    void playerMovement(){
        currentPosition = transform.position;
        if(playerDirection == PlayerDirection.Right){
            currentPosition.x += movementSpeed * Time.deltaTime;
        }else if(playerDirection == PlayerDirection.Left){
            currentPosition.x -= movementSpeed * Time.deltaTime;
        }else if(playerDirection == PlayerDirection.Up){
            currentPosition.y += movementSpeed * Time.deltaTime;
        }else if(playerDirection == PlayerDirection.Down){
            currentPosition.y -= movementSpeed * Time.deltaTime;
        }
    }


    void Grow(){
        Transform bodyObject = Instantiate(snakeBodyPrefab);
        Vector2 appliedPosition = snakeBodies[snakeBodies.Count-1].position;
        appliedPosition.x -= transform.localScale.x;
        bodyObject.position = appliedPosition;
        snakeBodies.Add(bodyObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
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