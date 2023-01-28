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

  private void Start()
  {
    playerMovement = this.GetComponent<PlayerMovement>();
  }
  public void OnTriggerEnter(Collider other)
  {
    if (other.tag == "platformTSection" && other is SphereCollider)
      playerMovement.canRotate = true;
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
