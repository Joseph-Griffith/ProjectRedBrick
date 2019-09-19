using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickupItem : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private RectTransform playerInteractPanel;

    public Image itemImage;

    public string Name { get; set; }

    // Use this for initialization
    private void Start()
    {
        Name = name;
    }

    /// <summary>
    /// Picks up this item if there is inventory space and deactivates the intereaction UI
    /// </summary>
    public void Operate()
    {
        if (Managers.Inventory.Items.Count < Managers.Inventory.inventorySlots.Count)
        {
            Managers.Inventory.AddItem(this);
            Debug.Log($"Item collected: {name}");
            Destroy(this.gameObject);
            playerInteractPanel.gameObject.SetActive(false);
        }
        else
            Debug.Log("Inventory full");
    }
}
