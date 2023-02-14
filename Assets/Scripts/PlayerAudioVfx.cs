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
  public PlayerMovement playerMovement; // Reference to Player Movement Class
  [SerializeField] ParticleSystem trailParticles; // Reference to trail particle fx
  [SerializeField] ParticleSystem collisionParticles; // Reference to collision particle fx
  [SerializeField] ParticleSystem successParticles; // Reference to success particle fx
  [SerializeField] AudioSource audioSource; // Reference to audio source component
  [SerializeField] AudioClip collisionClip; // Reference to collision sound clip
  [SerializeField] AudioClip successClip; // Reference to success sound clip

  // Class Methods
  // Awake Method
  private void Awake()
  {
    // Pause the emitting of trail, collision and success particles 
    collisionParticles.Pause();
    successParticles.Pause();
    trailParticles.Pause();
  }

  // Start Method
  private void Start()
  {
    // Get reference to script components from player game object
    playerMovement = this.GetComponent<PlayerMovement>();
  }

  // Update Method
  private void Update()
  {
    // If the player can move after countdown start playing trail paricles
    if (playerMovement.canMove == true)
    {
      // Play partices
      trailParticles.Play();
    }
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
