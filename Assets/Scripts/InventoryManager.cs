using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public RectTransform inventoryPanel;
    public List<RectTransform> inventorySlots = new List<RectTransform>();

    public ManagerStatus Status { get; private set; }
    public List<PickupItem> Items { get; private set; } = new List<PickupItem>();

    public void Startup()
    {
        Debug.Log("Inventory Manager starting...");
        Status = ManagerStatus.Started;
    }

    // Update is called once per frame
    private void Update()
    {
        // Show/Hide the inventory panel when 'I' is pressed
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.gameObject.SetActive(!inventoryPanel.gameObject.activeSelf);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    public void AddItem(PickupItem item)
    {
        // TODO : Finish inventory system

        Items.Add(item);
    }
}
