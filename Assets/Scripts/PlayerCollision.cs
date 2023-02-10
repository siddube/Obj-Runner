/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Player Collision
Description: Handles player collision
============================================================================================================================================= */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
  // Class properties
  public PlayerMovement playerMovement; // Reference to Player Movement Class
  public PlayerAudioVfx playerAudioVfx; // Reference to Player Audio Vfx  Class
  public string gameStatus = "GAME OVER"; // String to display on level end and denote the status/ result

  // Start Method
  private void Start()
  {
    // Get reference to script components from player game object
    playerMovement = this.GetComponent<PlayerMovement>();
    playerAudioVfx = this.GetComponent<PlayerAudioVfx>();
  }

  // On Collision Enter Method
  public void OnCollisionEnter(Collision other)
  {
    // If the other game object collider was of obstacle type, end level with lose results 
    if (other.gameObject.tag == "Obstacle")
    {
      // Set isAlive property to false to stop movement in forwards direction
      playerMovement.isAlive = false;

      // Set status string to "GAME OVER"
      gameStatus = "GAME OVER";

      // Call Play Collision Vfx method on Player Audio Vfx instance of Player Audio Vfx class
      playerAudioVfx.PlayCollisionVfx();
    }
  }

  // On Triggr Enter Method
  public void OnTriggerEnter(Collider other)
  {
    // If the other game object collider entered is a sphere collider of T section platform set canRotate property of Player Movement class to true
    // This enables the player to rotate
    if (other.tag == "platformTSection" && other is SphereCollider)
      playerMovement.canRotate = true;

    // If the other game object collider was of finish platform type, end level with win results
    if (other.tag == "Finish" && other is SphereCollider)
    {
      // Set isAlive property to false to stop movement in forwards direction
      playerMovement.isAlive = false;

      // Set status string to "GAME OVER"
      gameStatus = "LEVEL COMPLETE";

      // Call Play Success Vfx method on Player Audio Vfx instance of Player Audio Vfx class
      playerAudioVfx.PlaySuccessVfx();

      // Call ShootOnWin Method to accelerate player game object 
      playerMovement.ShootOnWin();
    }
  }

  // On Triggr Enter Method
  public void OnTriggerExit(Collider other)
  {
    // If the other game object collider exited is a sphere collider of T section platform set canRotate property of Player Movement class to false
    // This disables the player to rotate
    if (other.tag == "platformTSection" && other is SphereCollider)
    {
      // Set canRotate property of Player Movement Class to false
      playerMovement.canRotate = false;
      // Set didRotate property to false after exiting the collider
      // This will help player game object to rotate on next T section collider
      playerMovement.didRotate = false;
    }
  }
}
