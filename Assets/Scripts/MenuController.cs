/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Menu Controller
Description: Class to control Menu options
============================================================================================================================================= */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{

  [SerializeField] GameObject menuPanel;
  [SerializeField] GameObject controlsPanel;
  [SerializeField] GameObject creditsPanel;
  [SerializeField] GameObject backButton;
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip moveClip;
  [SerializeField] AudioClip selectClip;

  // On Click Play Button
  public void PlayGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void MoveButton()
  {
    audioSource.PlayOneShot(moveClip, 1.0f);
  }

  public void SelectButton()
  {
    audioSource.PlayOneShot(selectClip, 1.0f);
  }

  // Show Main Menu
  public void ShowMenu()
  {
    ToggleMenuPanels(menuPanel);
  }
  // Show Game Controls
  public void ShowControls()
  {
    ToggleMenuPanels(controlsPanel);
  }
  // Show Game Credits
  public void ShowCredits()
  {
    ToggleMenuPanels(creditsPanel);
  }
  // On Click Back Button
  // Returns to Main Menu
  public void BackToMenu()
  {
    ToggleMenuPanels(menuPanel);
  }
  // On Click Quit Button
  public void QuitGame()
  {
    Debug.Log("Quit Game!");
    Application.Quit();
  }

  public void OnGoBackToMenu()
  {
    ShowMenu();
    SelectButton();
  }

  /* =======================================================================================================
  Helper Functions to hide all panels and show the relevant/ current panel
  ========================================================================================================== */
  private void ToggleMenuPanels(GameObject currentPanel)
  {
    menuPanel.gameObject.SetActive(false);
    controlsPanel.gameObject.SetActive(false);
    creditsPanel.gameObject.SetActive(false);
    if (currentPanel != menuPanel)
      backButton.gameObject.SetActive(true);
    else
      backButton.gameObject.SetActive(false);
    currentPanel.gameObject.SetActive(true);
  }

}
