/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Path Generator
Description: Procedurally generated path for Orb to run on. Each round of play is different in terms of path generated in the level 
============================================================================================================================================= */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : MonoBehaviour
{
  [SerializeField] GameObject[] platforms;
  private GameObject PathTraveller;
  public int numOfPlatformsToGenerate = 20;
  public void Start()
  {
    // Call generate path at Start
    GeneratePath();
  }

  public void GeneratePath()
  {
    // Instantiate a game object to create path
    // The transform and rotation property will be used to generate path of gameobjects of different platform types in the prefabs
    PathTraveller = new GameObject("Path Traveller");
    // Create first n platforms
    for (int i = 0; i < numOfPlatformsToGenerate; i++)
    {
      // Get unique platform index from platform types (0 to last index) in the prefabs 
      int platformIndex = Random.Range(0, platforms.Length);
      // Instantiate the platform
      GameObject currentPlatform = Instantiate(platforms[platformIndex], PathTraveller.transform.position, PathTraveller.transform.rotation);
      // if platform is of T intersection type
      // Rotate the Path Traveller either in +90 or -90
      if (platforms[platformIndex].tag == "platformTSection")
      {
        if (Random.Range(0, 2) == 0)
          PathTraveller.transform.Rotate(new Vector3(0, 90, 0));
        else
          PathTraveller.transform.Rotate(new Vector3(0, -90, 0));
        PathTraveller.transform.Translate(Vector3.forward * -10);
      }
      // Translate to move 10 units further in z axis
      PathTraveller.transform.Translate(Vector3.forward * -10);
    }
  }
}