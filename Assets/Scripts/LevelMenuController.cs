using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour
{
  public PlayerMovement playerMovement;
  public PlayerCollision playerCollision;
  [SerializeField] GameObject levelMenuPanel;
  [SerializeField] AudioSource audioSource;
  [SerializeField] AudioClip moveClip;
  [SerializeField] AudioClip selectClip;
  [SerializeField] TMPro.TextMeshProUGUI statusTitle;

  public bool isPlayerAlive = true;
  public string statusTitleText = "GAME OVER";

  private void Start()
  {
    playerMovement = playerMovement.GetComponent<PlayerMovement>();
    playerCollision = playerCollision.GetComponent<PlayerCollision>();
  }
  private void Update()
  {
    isPlayerAlive = playerMovement.isAlive;
    statusTitleText = playerCollision.gameStatus;
    if (!isPlayerAlive)
      ShowMenu();
    else
      HideMenu();
  }

  public void PlayAgain()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
  public void BackToMenu()
  {
    SceneManager.LoadScene(0);
  }

  public void ShowMenu()
  {
    levelMenuPanel.SetActive(true);
    statusTitle.GetComponent<TMPro.TextMeshProUGUI>().text = statusTitleText;
  }

  public void HideMenu()
  {
    levelMenuPanel.SetActive(false);
  }

  public void MoveButton()
  {
    audioSource.PlayOneShot(moveClip, 1.0f);
  }

  public void SelectButton()
  {
    audioSource.PlayOneShot(selectClip, 1.0f);
  }
}
