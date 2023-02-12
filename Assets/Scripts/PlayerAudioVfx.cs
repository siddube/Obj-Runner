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
  // Class properties
  [SerializeField] ParticleSystem trailParticles; // Reference to trail particle fx
  [SerializeField] ParticleSystem collisionParticles; // Reference to collision particle fx
  [SerializeField] ParticleSystem successParticles; // Reference to success particle fx
  [SerializeField] AudioSource audioSource; // Reference to audio source component
  [SerializeField] AudioClip collisionClip; // Reference to collision sound clip
  [SerializeField] AudioClip successClip; // Reference to success sound clip

  // Class Methods
  // Start Method
  private void Awake()
  {
    // Pause the emitting of collision and success particles 
    collisionParticles.Pause();
    successParticles.Pause();
  }

  // Play Collission Vfx Method
  // Called when gamer loses a level
  public void PlayCollisionVfx()
  {
    // Play collission audio clip
    audioSource.PlayOneShot(collisionClip, 1.0f);

    // Hide the sphere mesh of player game object
    this.gameObject.GetComponent<Renderer>().enabled = false;

    // Play collision particles
    collisionParticles.Play();

    // Clear and stop trail particles
    trailParticles.Clear();
    trailParticles.Stop();
  }

  // Play Success Vfx Method
  // Called when gamer wins a level
  public void PlaySuccessVfx()
  {
    // Play success audio clip
    audioSource.PlayOneShot(successClip, 1.0f);

    // Play success particle
    successParticles.Play();

    // Clear and stop trail particles
    trailParticles.Clear();
    trailParticles.Stop();
  }
}
