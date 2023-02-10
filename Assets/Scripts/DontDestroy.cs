/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Don't Destroy
Description: Script used to avoid destroying of game objects
============================================================================================================================================= */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
  // Class Method
  // Awake Method
  void Awake()
  {
    // Do not game object playing the background music while switching scenes
    GameObject[] objs = GameObject.FindGameObjectsWithTag("BG Music");

    // find if there is more one instance of a gameobject
    if (objs.Length > 1)
    {
      // If true destroy the copy of game object
      Destroy(this.gameObject);
    }

    // Avoid destroying the game object from the first scene
    DontDestroyOnLoad(this.gameObject);
  }
}
