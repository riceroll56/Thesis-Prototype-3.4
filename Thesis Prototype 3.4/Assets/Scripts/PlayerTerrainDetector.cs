using UnityEngine;

public class PlayerTerrainDetector : MonoBehaviour
{
    public string currentTerrain = "None";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Grass"))
        {
            currentTerrain = "Grass";
        }
        else if (other.CompareTag("Sand"))
        {
            currentTerrain = "Sand";
        }
        else if (other.CompareTag("River"))
        {
            currentTerrain = "River";
        }
        else if (other.CompareTag("Ocean"))
        {
            currentTerrain = "Ocean";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(currentTerrain))
        {
            currentTerrain = "Grass";
        }

    }
}