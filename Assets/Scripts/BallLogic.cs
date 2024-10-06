using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallLogic : MonoBehaviour
{

    public Vector3 velocity;

    public GameObject paddle;

    public GameObject gameLogic;

    public float speed = 3.0f;

    private bool gameStart = true;

    public GameObject newBall;
    private float maxXVelocity = 6f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 100);
        //when space pressed set velocity only on start of game
        if (Input.GetKeyDown(KeyCode.Space) && gameStart)
        {
            velocity = new Vector3(0.5f, speed, 0);
            gameStart = false;
        }

        //if game has not started follow the paddle
        if (gameStart)
        {
            transform.position = paddle.transform.position + new Vector3(0, 0.5f, 0);
        }

        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            bool isSideWall = other.name.Contains("Left") || other.name.Contains("Right");

            if (isSideWall)
            {
                velocity = new Vector3(-velocity.x, velocity.y, velocity.z);
            }
            else
            {
                velocity = new Vector3(velocity.x, -velocity.y, velocity.z);
            }

        }
        else if (other.CompareTag("Paddle"))
        {
            // Get the position of the ball and the paddle
            Vector3 ballPosition = transform.position;
            Vector3 paddlePosition = other.transform.position;

            // Calculate the relative position of the ball on the paddle
            float relativePosition = (ballPosition.x - paddlePosition.x) / other.bounds.size.x;

            // Adjust the x velocity based on the relative position
            float newVelocityX = relativePosition * maxXVelocity; // maxXVelocity is a predefined maximum x velocity

            velocity = new Vector3(newVelocityX, -velocity.y, velocity.z);
        }
        else if (other.CompareTag("DeadZone"))
        {
            Debug.Log("Ball hit DeadZone");
            Vector3 pos = new Vector3(0, 10.3f, -0.5f);
            velocity = Vector3.zero;
            gameStart = true;
            gameLogic.GetComponent<GameLogic>().LoseLife();
            Instantiate(newBall, pos, Quaternion.identity);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Brick"))
        {
            Debug.Log("Ball hit Brick");
            velocity = new Vector3(velocity.x, -velocity.y, velocity.z);
            other.gameObject.GetComponent<BrickLogic>().OnHit();
        }
    }


}
