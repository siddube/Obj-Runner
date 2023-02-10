/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Logic Gate
Description: Handles movement of logic gates of spinner type
============================================================================================================================================= */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGateSpinner : MonoBehaviour
{
  // Class properties
  [SerializeField] float spinAngle = 3f; // Float value to spin on each call in Fixed Update Method

  // Class Methods
  // Start Method
  private void Start()
  {
    // Call a method to setup intial values of properties
    SetupSpinner();
  }

  // Setup Spinner Method
  // Called to setup intial values of properties
  private void SetupSpinner()
  {
    // Setup the rotation of game object to align to path generator class
    this.gameObject.transform.Rotate(new Vector3(0, 90, 0));

    // Either make the game object rotate clockwards or anti-clockwards
    if (Random.Range(0, 2) == 0)
      spinAngle = -spinAngle;
  }

  // Fixed Update Method
  // To spin independent of frame rate
  private void FixedUpdate()
  {
    // Call method to spin the object
    SpinObject();
  }

  // Spin Object Method
  // Method to spin the object
  private void SpinObject()
  {
    // Use Rotate method on transform to spin the object 
    this.gameObject.transform.Rotate(new Vector3(0, spinAngle, 0));
  }
}
