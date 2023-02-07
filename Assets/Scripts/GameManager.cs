using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  void Awake()
  {
    GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

    if (objs.Length > 1)
    {
      Destroy(this.gameObject);
    }

    DontDestroyOnLoad(this.gameObject);
  }
  private void Start()
  {

  }

  // Update is called once per frame
  private void Update()
  {

  }

  public void WinGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void ResartGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }

  public void LoseGame()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
  }
}
