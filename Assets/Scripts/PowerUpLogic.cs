using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLogic : MonoBehaviour
{

    public GameLogic gameLogic;  // Referenz auf das GameLogic-Skript (für die Punkteverwaltung)
    public Vector3 velocity = new Vector3(0, -1, 0);
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
    }
}
