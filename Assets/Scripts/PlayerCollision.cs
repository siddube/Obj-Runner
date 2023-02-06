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
  public PlayerMovement playerMovement;
  public GameManager gameManager;
  private void Start()
  {
    playerMovement = this.GetComponent<PlayerMovement>();
  }

  public void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Obstacle")
    {
      gameManager.LoseGame();
    }
  }
  public void OnTriggerEnter(Collider other)
  {
    if (other.tag == "platformTSection" && other is SphereCollider)
      playerMovement.canRotate = true;

    if (other.tag == "Finish" && other is SphereCollider)
    {
      gameManager.WinGame();
    }
  }

  public void OnTriggerExit(Collider other)
  {
    if (other.tag == "platformTSection" && other is SphereCollider)
    {
      playerMovement.canRotate = false;
      playerMovement.didRotate = false;
    }
  }
}
