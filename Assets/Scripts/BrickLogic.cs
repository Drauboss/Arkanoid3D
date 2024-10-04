using System.Collections.Generic;
using UnityEngine;

public class BrickLogic : MonoBehaviour
{
    public int scoreValue = 0;  // Standardpunkte, falls keine Farbe erkannt wird
    public Material greenMaterial;  // Referenz auf das grüne Material
    public Material greyMaterial;    // Referenz auf das graue Material
    public Material blueMaterial;   // Referenz auf das blaue Material

    public GameLogic gameLogic;  // Referenz auf das GameLogic-Skript (für die Punkteverwaltung)

    // Dictionary zur Speicherung der Materialien und ihrer Wahrscheinlichkeiten
    private Dictionary<Material, float> materialProbabilities;

    private float brickLife = 1.0f;  // Lebensdauer des Bricks

    // Start is called before the first frame update
    void Start()
    {
        // Initialisiere das Dictionary mit Materialien und ihren Wahrscheinlichkeiten
        materialProbabilities = new Dictionary<Material, float>
        {
            { greyMaterial, 0.6f },  // 50% Wahrscheinlichkeit
            { greenMaterial, 0.3f },   // 30% Wahrscheinlichkeit
            { blueMaterial, 0.1f }    // 20% Wahrscheinlichkeit
        };

        // Wähle ein Material basierend auf den Wahrscheinlichkeiten aus
        Material selectedMaterial = GetRandomMaterialBasedOnProbability();
        // Weise dem Brick das ausgewählte Material zu
        GetComponent<Renderer>().material = selectedMaterial;

        gameLogic = FindObjectOfType<GameLogic>();  // Findet die GameLogic im Spiel
        AssignScoreByMaterial();  // Weise Punkte basierend auf dem Material zu
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Methode zur Auswahl eines Materials basierend auf den Wahrscheinlichkeiten
    private Material GetRandomMaterialBasedOnProbability()
    {
        float total = 0;
        foreach (var probability in materialProbabilities.Values)
        {
            total += probability;
        }

        float randomPoint = Random.value * total;

        foreach (var kvp in materialProbabilities)
        {
            if (randomPoint < kvp.Value)
            {
                return kvp.Key;
            }
            else
            {
                randomPoint -= kvp.Value;
            }
        }
        return null;  // Sollte nie erreicht werden, wenn die Wahrscheinlichkeiten korrekt sind
    }

    void AssignScoreByMaterial()
    {
        // Greife auf das Material des Bricks zu
        Material brickMaterial = GetComponent<Renderer>().material;

        // Entferne "(Instance)" aus dem Materialnamen
        string brickMaterialName = brickMaterial.name.Replace(" (Instance)", "");

        // Überprüfe, welches Material dem Brick zugewiesen ist und weise Punkte zu
        if (brickMaterialName == greenMaterial.name)
        {
            scoreValue = 10;  // Grün gibt 10 Punkte
            brickLife = 2.0f;
        }
        else if (brickMaterialName == greyMaterial.name)
        {
            scoreValue = 5;  // Grau gibt 5 Punkte
            brickLife = 1.0f;
        }
        else if (brickMaterialName == blueMaterial.name)
        {
            scoreValue = 20;  // Blau gibt 20 Punkte
            brickLife = 3.0f;
        }
        else
        {
            scoreValue = 1;  // Standardpunkte für andere Materialien
        }
    }


    // Wenn der Brick getroffen wird, rufe diese Methode auf
    public void OnHit()
    {
        gameLogic.AddScore(scoreValue);  // Füge die Punkte hinzu, wenn der Brick getroffen wird

        if (brickLife > 1)
        {
            brickLife -= 1f;
        }
        else
        {
            Debug.Log("Brick destroyed");
            Destroy(gameObject);
        }
    }
}