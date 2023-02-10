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
  public PlayerAudioVfx playerAudioVfx;
  public string gameStatus = "GAME OVER";
  private void Start()
  {
    playerMovement = this.GetComponent<PlayerMovement>();
    playerAudioVfx = this.GetComponent<PlayerAudioVfx>();
  }

  public void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Obstacle")
    {
      playerMovement.isAlive = false;
      gameStatus = "GAME OVER";
      playerAudioVfx.playCollisionVfx();
    }
  }
  public void OnTriggerEnter(Collider other)
  {
    if (other.tag == "platformTSection" && other is SphereCollider)
      playerMovement.canRotate = true;

    if (other.tag == "Finish" && other is SphereCollider)
    {
      playerMovement.isAlive = false;
      gameStatus = "LEVEL COMPLETE";
      playerAudioVfx.playSucessVfx();
      playerMovement.shootOnWin();
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
