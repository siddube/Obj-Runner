/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Menu Controller
Description: Class to control main menu options
============================================================================================================================================= */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
  // Class properties
  [SerializeField] GameObject menuPanel; // Reference to main menu panel gameobject
  [SerializeField] GameObject controlsPanel; // Reference to controls menu panel gameobject
  [SerializeField] GameObject tutorialPanel; // Reference to credits tutorial panel gameobject
  [SerializeField] GameObject creditsPanel; // Reference to credits menu panel gameobject
  [SerializeField] GameObject backButton; // Reference to back button gameobject on the menu
  [SerializeField] AudioSource audioSource; // Reference to audio source component
  [SerializeField] AudioClip moveClip; // Reference to move sound clip
  [SerializeField] AudioClip selectClip; // Reference to select sound clip

  // Class Methods
  // Play Game Method
  // Called when play button is clicked in level menu
  public void PlayGame()
  {
    // load level scene by getting current index (0) and move to next index
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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

  // Show Menu Method
  // Called to show menu
  public void ShowMenu()
  {
    // Use helper function to hide all other panels and show main menu panel
    ToggleMenuPanels(menuPanel);
  }

  // Show Controls Method
  // Called to show game controls by clicking controls button
  public void ShowControls()
  {
    // Use helper function to hide all other panels and show game controls panel
    ToggleMenuPanels(controlsPanel);
  }

  // Show Tutorial Method
  // Called to show game tutorials by clicking controls button
  public void ShowTutorial()
  {
    ToggleMenuPanels(tutorialPanel);
  }

  // Show Credits Method
  // Called to show game credits by clicking credits button
  public void ShowCredits()
  {
    // Use helper function to hide all other panels and show game credits panel
    ToggleMenuPanels(creditsPanel);
  }

  // Back To Menu Mathod
  // Called to return to main menu panel
  public void BackToMenu()
  {
    // Use helper function to hide all other panels and show main menu panel
    ToggleMenuPanels(menuPanel);
  }

  // Quit Game Method
  // Called to quit game
  public void QuitGame()
  {
    // Log quit game as webGL build does not support quit
    Debug.Log("Quit Game!");

    // Quit application if ported to PC/ mobile/ console
    Application.Quit();
  }

  // On Go Back To Menu Method
  // Called on clicking backspace under sub menu to return to main menu
  public void OnGoBackToMenu()
  {
    // Call Show Menu Method
    ShowMenu();

    // Call Select Button Method
    SelectButton();
  }

  /* =======================================================================================================
  Helper Function to hide all panels and show the relevant/ current panel
  ========================================================================================================== */
  private void ToggleMenuPanels(GameObject currentPanel)
  {
    // Hide menu and all the sub menus 
    menuPanel.gameObject.SetActive(false);
    controlsPanel.gameObject.SetActive(false);
    tutorialPanel.gameObject.SetActive(false);
    creditsPanel.gameObject.SetActive(false);

    // Show back button when on a sub menu
    // Hide back button when on the main menu
    if (currentPanel != menuPanel)
      backButton.gameObject.SetActive(true);
    else
      backButton.gameObject.SetActive(false);

    // Use currentPanel argument to show appropriate menu
    currentPanel.gameObject.SetActive(true);
  }
}
