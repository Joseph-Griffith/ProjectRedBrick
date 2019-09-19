using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerMotor : MonoBehaviour
{

    public Vector3 movement = Vector3.zero;
    public float travelMoveSpeed = 12f;
    public float combatMoveSpeed = 8f;
    public float dodgeSpeed = 20f;
    public float dodgeTimer = 0f;
    public float dodgeTimeLength = 1.5f;
    public float timeBetweenDodge = 1f;

    private PlayerController player;
    private Rigidbody rb;
    private Vector3 direction = Vector3.zero;

    public bool IsDodging { get; set; }

    // Use this for initialization
    private void Start()
	{
        player = GetComponent<PlayerController>();

        rb = GetComponent<Rigidbody>();

        player.WeaponSheathed = true;

        IsDodging = false;
	}

    // Update is called once per frame
    private void Update()
    {
        // Has to be called here because it may miss the button press if done in fixed update
        if (Input.GetKeyDown(KeyCode.V))
        {
            player.WeaponSheathed = !player.WeaponSheathed;
        }
    }

    // FixedUpdate is framerate independant
    private void FixedUpdate()
	{
        movement = Vector3.zero;

        // If we are not dodging, then allow input based movement
        if (!IsDodging)
        {
            if (player.WeaponSheathed)
            {
                TravelMove(movement);
            }

            if (!player.WeaponSheathed)
            {
                CombatMove(movement);
            }
        }

        // If we are dodging, disable input movement
        else if (IsDodging)
        {
            Dodge();
        }
    }

    /// <summary>
    /// Moves the player relative to user input
    /// </summary>
    /// <param name="movement"></param>
    private void TravelMove(Vector3 movement)
    {
        float moveVertical = Input.GetAxisRaw("Vertical");
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        // Calculate the movement vector and normalize to stop diagonal movement multiplying
        movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (movement.sqrMagnitude > 0.1f) // This stops us auto-rotating back to our original position once we stop moving
        {
            // Get the GFX of the player
            CombatDodgeRotation child = GetComponentInChildren<CombatDodgeRotation>();

            // Store the rotation of the GFX
            Quaternion childRotation = child.transform.rotation;

            // Save the current camera rotation
            Quaternion tmp = Camera.main.transform.rotation;
            // Zero the x and z rotation angles
            Camera.main.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            // Transform the movement direction from local to global coordinates
            movement = Camera.main.transform.TransformDirection(movement);
            // Set the cameras rotation back to its original value
            Camera.main.transform.rotation = tmp;

            // Slerp the transforms rotation to face the direction we are moving
            this.transform.rotation = Quaternion.LookRotation(movement);

            // Set the GFX rotation back to its original
            child.transform.rotation = childRotation;
        }

        // Move the player
        this.transform.Translate(movement * Time.deltaTime * travelMoveSpeed, Space.World);
        //rb.AddForce(this.transform.position + movement * Time.deltaTime * travelMoveSpeed); // Good for Ice movement

        // If the dodge button is pressed whilst moving and we are allowed to dodge...
        if (Input.GetButtonDown("Jump") && movement.sqrMagnitude > 0.1f && player.DodgeCharges > 0)
        {
            // Dodge
            Dodge();
        }
            
    }

    /// <summary>
    /// Moves the player but faces in the direction of the current mouse position
    /// </summary>
    /// <param name="movement"></param>
    private void CombatMove(Vector3 movement)
    {
        float moveVertical = Input.GetAxisRaw("Vertical");
        float moveHorizontal = Input.GetAxisRaw("Horizontal");

        // Calculate the movement vector and normalize to stop diagonal movement multiplying
        movement = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (movement.sqrMagnitude > 0.1f) // This stops us auto-rotating back to our original position once we stop moving
        {
            // Save the current camera rotation
            Quaternion tmp = Camera.main.transform.rotation;
            // Zero the x and z rotation angles
            Camera.main.transform.eulerAngles = new Vector3(0, Camera.main.transform.eulerAngles.y, 0);
            // Transform the movement direction from local to global coordinates
            movement = Camera.main.transform.TransformDirection(movement);
            // Set the cameras rotation back to its original value
            Camera.main.transform.rotation = tmp;

            // Set the transform rotation to the direction we are moving
            this.transform.rotation = Quaternion.LookRotation(movement);
            
        }

        // Move the player
        this.transform.Translate(movement * Time.deltaTime * combatMoveSpeed, Space.World);
        //rb.AddForce(this.transform.position + movement * Time.deltaTime * travelMoveSpeed); // Good for Ice movement

        if (Input.GetButtonDown("Jump") && movement.sqrMagnitude > 0.1f && player.DodgeCharges > 0)
        {
            // Check if we are dodging
            Dodge();
        }
    }


    private void Dodge()
    {
        IsDodging = true;

        // Complete dodge over time
        if (dodgeTimeLength > 1f)
        {
            //Move the player over time
            this.transform.position += this.transform.forward * dodgeSpeed * Time.deltaTime;

            // Reduce the dodgeTimeLength by the amount of time that has passed
            dodgeTimeLength -= dodgeTimeLength * Time.deltaTime;
        }

        // If the dodge has finished
        if (dodgeTimeLength <= 1)
        {
            // Set IsDodging to false
            IsDodging = false;

            // Reset dodgeTimeLength
            dodgeTimeLength = 1.5f;
        }
    }
}
