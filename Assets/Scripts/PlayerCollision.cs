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
  public GameManager gameManager;
  private IEnumerator coroutine;
  private void Start()
  {
    playerMovement = this.GetComponent<PlayerMovement>();
    playerAudioVfx = this.GetComponent<PlayerAudioVfx>();
  }

  public void OnCollisionEnter(Collision other)
  {
    if (other.gameObject.tag == "Obstacle")
    {
      playerAudioVfx.playCollisionVfx();
      coroutine = LoseGame(2.0f);
    }
  }
  public void OnTriggerEnter(Collider other)
  {
    if (other.tag == "platformTSection" && other is SphereCollider)
      playerMovement.canRotate = true;

    if (other.tag == "Finish" && other is SphereCollider)
    {
      playerAudioVfx.playSucessVfx();
      playerMovement.shootOnWin();
      coroutine = WinGame(3.0f);
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

  private IEnumerator LoseGame(float waitTime)
  {
    yield return new WaitForSeconds(waitTime);
    gameManager.LoseGame();
  }
  private IEnumerator WinGame(float waitTime)
  {
    yield return new WaitForSeconds(waitTime);
    gameManager.WinGame();
  }

}
