using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public float Health { get; set; }
    public float DodgeCharges { get; set; }
    public float Attack { get; set; }
    public float Armour { get; set; }
    public float Block { get; set; }
    public float MagicResistance { get; set; }
    public bool WeaponSheathed { get; set; }

    public RectTransform interactPanel;
    public Text weaponSheathedText;

    // Use this for initialization
    private void Start()
	{
        this.Health = 100f;
        this.DodgeCharges = 3f;
        this.Attack = 0f;
        this.Armour = 0f;
        this.Block = 0f;
        this.MagicResistance = 0f;
	}

    // Update is called once per frame
    private void Update()
    {
        if (WeaponSheathed)
            weaponSheathedText.text = "Weapon Sheathed";
        else
            weaponSheathedText.text = "Weapon Unsheathed";
    }

    /// <summary>
    /// Displays the interact UI if in range of an interactable object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Interactable")
        {
            interactPanel.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Disables the interact UI when leaving the range of an interactable object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        interactPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// Kills the player
    /// </summary>
    private void Die()
    {
        // Kill the player

        // TODO: Flesh out the Die function
    }
}
