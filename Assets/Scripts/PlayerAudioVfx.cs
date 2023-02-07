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
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip collisionClip;
  [SerializeField] AudioClip successClip;
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
    audioSource.PlayOneShot(collisionClip, 1.0f);
    this.gameObject.GetComponent<Renderer>().enabled = false;
    collisionParticles.Play();
    trailParticles.Clear();
    trailParticles.Stop();
  }

  public void playSucessVfx()
  {
    audioSource.PlayOneShot(successClip, 1.0f);
    successParticles.Play();
    trailParticles.Clear();
    trailParticles.Stop();
  }
}
