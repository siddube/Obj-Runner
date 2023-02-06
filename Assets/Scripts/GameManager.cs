using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  // Start is called before the first frame update
  private void Start()
  {

  }

  // Update is called once per frame
  private void Update()
  {

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
