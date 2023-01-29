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
  [SerializeField] PathGenerator path;
  [SerializeField] float sidewardSpeed = 1f;
  [SerializeField] float jumpSpeed = 1f;
  [SerializeField] float forwardSpeed = 1f;
  public Rigidbody rb;
  public bool canRotate = false;
  public bool didRotate = false;

  private Vector3 sidewardVector = new Vector3(0f, 0f, 0f);
  private enum MoveForwardDir
  {
    up, right, down, left
  }

  private MoveForwardDir forwardDir = MoveForwardDir.up;

  private void Start()
  {
    // Setup rigidbody component
    SetupRigdbody();
  }

  private void FixedUpdate()
  {
    // Process sidewards movement from Keyboard input
    ProcessMovement();
    // Move the player forwards at constant velocity
    MoveForwards();
  }

  private void SetupRigdbody()
  {
    // Setup rigidbody component to a reference to be used in rest of the class
    rb = GetComponent<Rigidbody>();
    // Freeze rotation and handle it through code alone
    // Ignores Physics
    rb.freezeRotation = true;
  }

  public void ProcessMovement()
  {
    // Add forces to the left and right movement with sidewardVector variable
    rb.AddForce(sidewardVector * sidewardSpeed, ForceMode.Acceleration);
  }

  private void MoveForwards()
  {
    // Add constant forward vector
    // Multiply by -1 as we move in -ve z axis
    this.gameObject.transform.Translate(Vector3.forward * -1 * forwardSpeed);
  }

  public void OnJump()
  {
    // Jump on pressing Spacebar on keyboard
    // Return if the player is already jumping
    // Checks by calculating the player's Y position of the transform 
    if (this.gameObject.transform.position.y > 1.2) { return; }
    rb.AddForce(transform.up * jumpSpeed, ForceMode.Impulse);
  }

  public void OnMove(InputValue input)
  {
    // Get a Vector2 value of input controls
    Vector2 xInput = input.Get<Vector2>();
    // Set sidewardVector and sidewardDirection based on the player's forward direction
    SetSidewardDirection(xInput);
  }

  public void OnRotateRight()
  {
    if (!canRotate || didRotate) { return; }
    didRotate = true;
    canRotate = false;
    // Rotate player to the right on pressing C
    // Unfreeze rotation to rotate player
    rb.freezeRotation = false;
    // Rotate player
    this.gameObject.transform.Rotate(0f, -90f, 0f, Space.Self);
    path.SetAngleToRotateByPath(-90f);
    // Set new forward direction by passing right as parameter to denote player turned right
    ToggleForwardDirection("right");
    // Freeze player rotation again
    rb.freezeRotation = true;

  }
  public void OnRotateLeft()
  {
    if (!canRotate || didRotate) { return; }
    didRotate = true;
    canRotate = false;
    // Rotate player to the left on pressing Z
    // Unfreeze rotation to rotate player
    rb.freezeRotation = false;
    // Rotate player
    this.gameObject.transform.Rotate(0f, 90f, 0f, Space.Self);
    path.SetAngleToRotateByPath(90f);
    // Set new forward direction by passing left as parameter to denote player turned left
    ToggleForwardDirection("left");
    // Freeze player rotation again
    rb.freezeRotation = true;
  }


  /* =======================================================================================================
  Helper Functions to set forward and sideward direction based on input
  ========================================================================================================== */
  private void SetSidewardDirection(Vector2 xInput)
  {
    // Set sidewardVector to be used in Process Movement function
    // If the player is moving up or down from initial rotation
    // Create vector from input and use it for the x-axis value
    // Else if left or right create vector from input and use it for the z-axis value
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
