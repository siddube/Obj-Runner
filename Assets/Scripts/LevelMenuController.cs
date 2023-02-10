﻿/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Level Menu Controller
Description: Class to control in level menu options
============================================================================================================================================= */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
  // Class properties
  public PlayerMovement playerMovement; // Reference to player movement class
  public PlayerCollision playerCollision; // Reference to player collision class
  [SerializeField] GameObject levelMenuPanel; // Reference to level menu panel gameobject
  [SerializeField] AudioSource audioSource; // Reference to audio source component
  [SerializeField] AudioClip moveClip; // Reference to move sound clip
  [SerializeField] AudioClip selectClip; // Reference to select sound clip
  [SerializeField] TMPro.TextMeshProUGUI statusTitle; // Reference to text mesh pro UI text

  public bool isPlayerAlive = true; // Bool used to access player alive/ active status from player game object
  public string statusTitleText = "GAME OVER"; // String used to set text in UI after player loses or wins the game

  // Class Methods
  // Start Method
  private void Start()
  {
    // Get reference to script components from player game object
    playerMovement = playerMovement.GetComponent<PlayerMovement>();
    playerCollision = playerCollision.GetComponent<PlayerCollision>();
  }

  // Update Method
  private void Update()
  {
    // Check if player is still alive
    isPlayerAlive = playerMovement.isAlive;

    // Get the value to display win or lose status on level menu
    // Assign string to UI canvas' Text Mesh Pro component
    statusTitleText = playerCollision.gameStatus;

    // Show or hide level menu based on player alive status
    if (!isPlayerAlive)
      ShowMenu();
    else
      HideMenu();
  }

  // Play Again Method
  // Called when play again button is clicked in level menu
  public void PlayAgain()
  {
    // load current level scene again
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  // Back To Menu Method
  // Called when menu button is clicked in level menu
  public void BackToMenu()
  {
    // load menu scene
    SceneManager.LoadScene(0);
  }

  // Show Menu Method
  // Called when player either loses or wins the level
  public void ShowMenu()
  {
    // Set level menu panel gameobject to true
    levelMenuPanel.SetActive(true);

    // Set status title of text mesh pro component on UI
    statusTitle.GetComponent<TMPro.TextMeshProUGUI>().text = statusTitleText;
  }

  // Hide Menu Method
  // Called if gamer decides to play the level again
  public void HideMenu()
  {
    // Set level menu panel gameobject to false
    levelMenuPanel.SetActive(false);
  }

  // Move Button Method
  // Called on moving from button to button in level menu
  public void MoveButton()
  {
    // Play move audio clip on audio source
    audioSource.PlayOneShot(moveClip, 1.0f);
  }

  // Select Button Method
  // Called on click foor a button
  public void SelectButton()
  {
    // Play select audio clip on audio source
    audioSource.PlayOneShot(selectClip, 1.0f);
  }
}
