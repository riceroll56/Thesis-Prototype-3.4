using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{

    public GameObject doorTrigger;
    public GameObject teleportUI;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        teleportUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        teleportUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        teleportUI.SetActive(false);
    }

    public void TeleportToKitchen()
    {
        SceneManager.LoadScene(0);
    }
    public void TeleportToWorld1()
    {
        SceneManager.LoadScene(1);
    }
}
