/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Logic Gate
Description: Handles movement of logic gates of mover type
============================================================================================================================================= */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGateMover : MonoBehaviour
{
  // Class properties
  [SerializeField] Vector3 movementVector; // A Vector to make forward-backward / upward-downward movement
  [SerializeField][Range(0, 1)] float movementFactor; // A 0-1 factor to control forward-backward / upward-downward movement
  [SerializeField] float period = 2f; // Period of one cycle of movement
  private Vector3 startingPosition; // Vector to store intial position of game object
  private Vector3 movementVectorY = new Vector3(0, 2, 0); // A vector to move 2 units up and down
  private Vector3 movementVectorZ = new Vector3(0, 0, 5); // A vector to move 5 units forward and backward

  // Class Methods
  // Start Method
  private void Start()
  {
    // Call a method to setup intial values of properties
    SetupMovement();
  }

  // Setup Movement Method
  // Called to setup intial values of properties
  private void SetupMovement()
  {
    // Set starting position property to current game object position
    startingPosition = transform.position;

    // Setup the rotation of game object to align to path generator class
    this.gameObject.transform.Rotate(new Vector3(0, 90, 0));

    // Either make the game object move as a upward-downward obstacle
    // Or Forward-Backward motion
    if (Random.Range(0, 2) == 0)
      movementVector = movementVectorY;
    else
      movementVector = movementVectorZ;
  }

  // Fixed Update Method
  // To fire movements independent of frame rate
  private void FixedUpdate()
  {
    // Call method to move the object peridodically
    MoveObject();
  }

  // Move Object Method
  // Method to move the object periodically
  // Code written with help of psudeocode algorithm to move a game object periodically 
  private void MoveObject()
  {
    // calculate cycles based on period
    float cycles = Time.time / period;

    // Calculate value of tau/ 2pi
    const float tau = Mathf.PI * 2;

    // Calculate sine wave from cycle and tau
    float rawSinWave = Mathf.Sin(cycles * tau);

    // Calculate movement factor
    movementFactor = (rawSinWave + 1f) / 2f;

    // calculate offset and move it from the initial position periodically
    Vector3 offset = movementVector * movementFactor;
    transform.position = startingPosition + offset;
  }
}
