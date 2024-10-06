using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLogic : MonoBehaviour
{
    public float speed = 10.0f;
    private GameLogic gameLogic;

    public Transform floor;

    public Transform paddle;
    // Start is called before the first frame update
    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();

    }

    // Update is called once per frame
    void Update()
    {
        float maxX = floor.localScale.x * 10f * 0.5f - paddle.localScale.x * 0.5f;

        float dir = Input.GetAxis("Horizontal");

        float posX = transform.position.x + dir * speed * Time.deltaTime;

        float clampedX = Mathf.Clamp(posX, -maxX, maxX);

        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            if (other.name.Contains("1"))
            {
                gameLogic.MakePaddeBigger();
            }
            else if (other.name.Contains("2"))
            {
                gameLogic.MakeGameSlower();
            }
            else if (other.name.Contains("3"))
            {
                gameLogic.HealthUp();
            }
            Debug.Log("PowerUp collected");
            Destroy(other.gameObject);
        }
    }
}
