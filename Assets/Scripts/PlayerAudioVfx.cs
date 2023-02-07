/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Player Audio Vfx
Description: Handles player audio and vfx particles
============================================================================================================================================= */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioVfx : MonoBehaviour
{
  [SerializeField] ParticleSystem trailParticles;
  [SerializeField] ParticleSystem collisionParticles;
  [SerializeField] ParticleSystem successParticles;

  private void Start()
  {
    collisionParticles.Pause();
    successParticles.Pause();
  }


  private void Update()
  {

  }

  public void playCollisionVfx()
  {
    this.gameObject.GetComponent<Renderer>().enabled = false;
    collisionParticles.Play();
    trailParticles.Clear();
    trailParticles.Stop();
  }

  public void playSucessVfx()
  {
    successParticles.Play();
    trailParticles.Clear();
    trailParticles.Stop();
  }
}
