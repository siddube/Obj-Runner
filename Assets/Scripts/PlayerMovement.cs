/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Player Movement
Description: Handles player movement
============================================================================================================================================= */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  // Class Properties
  [SerializeField] PathGenerator path; // Referece to path generator class
  [SerializeField] float sidewardSpeed = 1f; // Float value to set left and right sideward speed on editor
  [SerializeField] float jumpSpeed = 1f; // Float value to set jump speed on editor
  [SerializeField] float forwardSpeed = 1f; // Float value to set forward speed on editor
  public Rigidbody rb; // Reference to rigid body of game object
  public bool canRotate = false; // Bool to denote if player can rotate
  public bool didRotate = false; // Bool to denote if player did rotate and prevent more than one rotation inside T section platform sphere collider
  public bool isAlive = true; // Bool to move player and stop player movement if the level ended
  private bool isGrounded = true; // Bool to check if the player is grounded or jumping
  public bool canMove = false; // Bool to check if player can move forward

  private Vector3 sidewardVector = new Vector3(0f, 0f, 0f); // Vector for left and right sidewards movement

  // Enum to set forward direction of player game object
  private enum MoveForwardDir
  {
    up, right, down, left
  }

  private MoveForwardDir forwardDir = MoveForwardDir.up; // Set initial movement to up in forward direction

  // Class Methods
  // Start Method
  private void Start()
  {
    // Setup rigidbody component
    SetupRigdbody();
  }

  // Fixed Update Method
  // To move player independent of frame rate
  private void FixedUpdate()
  {
    // Process sidewards movement from Keyboard input
    ProcessMovement();
    // Move the player forwards at constant velocity
    MoveForwards();
  }

  // Setup Rigid Body Method
  // Setup rigid body component properties
  private void SetupRigdbody()
  {
    // Setup rigidbody component to a reference to be used in rest of the class
    rb = GetComponent<Rigidbody>();
    // Freeze rotation and handle it through code alone
    // Ignores Physics
    rb.freezeRotation = true;
  }

  // Process Movement Method
  // Process sidwards and jump movement
  public void ProcessMovement()
  {
    // If isAlive property is false stop accepting input
    if (!isAlive) { return; }

    // Add forces to the left and right movement with sidewardVector variable
    rb.AddForce(sidewardVector * sidewardSpeed, ForceMode.Acceleration);

    // Call a method to handle vertical movement of player
    ProcessVerticalMovement();
  }

  // Move Forawrds Method
  // Move the player forwards at constant velocity
  private void MoveForwards()
  {
    // If isAlive property is false stop moving player forwards
    // Only move forward after countdown stops and game starts handled by canMoveForwad
    if (!isAlive || !canMove) { return; }

    // Add constant forward vector
    // Multiply by -1 as we move in -ve z axis
    this.gameObject.transform.Translate(Vector3.forward * -1 * forwardSpeed);
  }

  // Process Vertical Movement Method 
  public void ProcessVerticalMovement()
  {
    // End level if player is below the platform
    if (this.gameObject.transform.position.y < -1)
      isAlive = false;

    // Calculate if isGrounded should be true or false based on the y-axis position of player
    // Set true if it is below 1.1
    // Set false if it above 1.2  
    if (this.gameObject.transform.position.y < 1.1 && this.gameObject.transform.position.y > 0)
      isGrounded = true;
    if (this.gameObject.transform.position.y > 1.2)
      isGrounded = false;
  }

  // On Jump Method
  // Called on pressing spacebar as input
  public void OnJump()
  {
    // Allow movement from input after game starts and canMove property is true
    if (!canMove) { return; }

    // Jump on pressing Spacebar on keyboard
    // Return if the player is already jumping
    if (!isGrounded) { return; }

    // Set isGrounded property to false to avoid multiple jumps on multiple spacebar keypress 
    isGrounded = false;

    // Add impuse force to rigd body to make the body jump
    rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
  }

  // On Move Method
  // Called on pressing left and right arrow as input
  public void OnMove(InputValue input)
  {
    // Allow movement from input after game starts and canMove property is true
    if (!canMove) { return; }
    // Get a Vector2 value of input controls
    Vector2 xInput = input.Get<Vector2>();
    // Set sidewardVector and sideward position
    SetSidewardDirection(xInput);
  }

  // Shoot On Win Method
  // Called if player completed level
  public void ShootOnWin()
  {
    // Accelerate player to invoke "success" game feel
    this.gameObject.transform.Translate(Vector3.forward * -1 * forwardSpeed * 100);
  }

  // On Rotate Right Method
  // Called when player presses C on keyboard
  public void OnRotateRight()
  {
    // Call helper function and pass right and -90f as arguments to denote player turned right
    ProcessRotation(-90f, "right");
  }

  // On Rotate Left Method
  // Called when player presses Z on keyboard
  public void OnRotateLeft()
  {
    // Call helper function and pass left and 90f as arguments to denote player turned left
    ProcessRotation(90f, "left");
  }

  /* =======================================================================================================
    Helper Functions to rotate player based on input and generate path in the direction of turn
    ========================================================================================================== */
  public void ProcessRotation(float angle, string direction)
  {
    // Return if the player is not on a T section platform and is able to turn
    // Or has already turned
    if (!canRotate || didRotate) { return; }

    // Set didRotate property to true to prevent further rotation
    didRotate = true;

    // Set canRotate property to false to prevent further rotation
    canRotate = false;

    // Unfreeze rotation to rotate player
    rb.freezeRotation = false;

    // Rotate player using Rotate Method on trasform of the game object
    this.gameObject.transform.Rotate(0f, angle, 0f, Space.Self);

    // Call Set Angle ToRotate By Path Method on Path Generator Class 
    // Start generating world based on the direction the player turned
    path.SetAngleToRotateByPath(angle);

    // Set new forward direction by passing right as parameter to denote player turned right
    ToggleForwardDirection(direction);

    // Freeze player rotation again
    rb.freezeRotation = true;
  }

  /* =======================================================================================================
  Helper Functions to set forward and sideward direction based on input
  ========================================================================================================== */
  private void SetSidewardDirection(Vector2 xInput)
  {
    // Set sidewardVector to be used in Process Movement function
    // If current forward direction is up or down from initial rotation
    // Create vector from input and use it for the x-axis value
    // Else if current forward direction is left or right create vector from input and use it for the z-axis value
    // Use postive xInput or negative xInput based on forward direction enum
    // Both vectors help create sidewards movement of the player on the platforms
    if (forwardDir == MoveForwardDir.up)
      sidewardVector = new Vector3(-xInput.x, 0, 0);
    else if (forwardDir == MoveForwardDir.down)
      sidewardVector = new Vector3(xInput.x, 0, 0);
    else if (forwardDir == MoveForwardDir.right)
      sidewardVector = new Vector3(0, 0, -xInput.x);
    else if (forwardDir == MoveForwardDir.left)
      sidewardVector = new Vector3(0, 0, xInput.x);
  }

  private void ToggleForwardDirection(string rotator)
  {
    // Check if the function was called from right or left rotation
    // If right change the forward direction clockwise
    if (rotator == "right")
    {
      if (forwardDir == MoveForwardDir.up)
        forwardDir = MoveForwardDir.right;
      else if (forwardDir == MoveForwardDir.right)
        forwardDir = MoveForwardDir.down;
      else if (forwardDir == MoveForwardDir.down)
        forwardDir = MoveForwardDir.left;
      else if (forwardDir == MoveForwardDir.left)
        forwardDir = MoveForwardDir.up;
    }

    // If left change the forward direction anti-clockwise rotation
    if (rotator == "left")
    {
      if (forwardDir == MoveForwardDir.up)
        forwardDir = MoveForwardDir.left;
      else if (forwardDir == MoveForwardDir.left)
        forwardDir = MoveForwardDir.down;
      else if (forwardDir == MoveForwardDir.down)
        forwardDir = MoveForwardDir.right;
      else if (forwardDir == MoveForwardDir.right)
        forwardDir = MoveForwardDir.up;
    }
  }
}
