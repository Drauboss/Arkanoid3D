using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleLogic : MonoBehaviour
{
    public float speed = 10.0f;

    public Transform floor;

    public Transform paddle;
    // Start is called before the first frame update
    void Start()
    {

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
}
