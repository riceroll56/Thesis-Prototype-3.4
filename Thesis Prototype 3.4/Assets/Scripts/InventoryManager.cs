using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryManager : MonoBehaviour
{
    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Inventory.Instance != null)
        {
            Inventory.Instance.slotUIs = FindObjectsByType<InventorySlot>(
                FindObjectsSortMode.None
            );
            Inventory.Instance.UpdateUI();
        }
    }
}
