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
  [SerializeField] GameObject path;
  [SerializeField] GameObject[] platforms;
  [SerializeField] GameObject[] obstacles;
  [SerializeField] GameObject finalPlatform;
  private GameObject PathTraveller;
  public int numOfPlatformsToGenerate = 20;
  private Vector3 obstacleVector = new Vector3(0, 1, 0);
  private int currentPathCount = 0;
  private int pathsLeftToGenerate;

  private bool stopGeneratingPath = false;

  public void Start()
  {
    // Call generate path at Start
    pathsLeftToGenerate = numOfPlatformsToGenerate - currentPathCount;
    GeneratePath();
  }

  private void Update()
  {
    UpdatePath();
  }

  public void GeneratePath()
  {
    // Instantiate a game object to create path
    // The transform and rotation property will be used to generate path of gameobjects of different platform types in the prefabs
    PathTraveller = new GameObject("Path Traveller");
    // Create first n platforms with loop to generate method
    LoopToGeneratePath(pathsLeftToGenerate);
  }

  public void LoopToGeneratePath(int numPath)
  {
    if (stopGeneratingPath == true) { return; }
    for (int i = 0; i < numPath; i++)
    {
      // Get unique platform index from platform types (0 to last index) in the prefabs 
      int platformIndex = Random.Range(0, platforms.Length);
      if (((i == 0) || (i == 1) || (i == numPath - 1)) && (platforms[platformIndex].tag == "platformTSection"))
      {
        LoopToGeneratePath(pathsLeftToGenerate);
        break;
      }

      // Instantiate the platform
      GameObject currentPlatform = Instantiate(platforms[platformIndex], PathTraveller.transform.position, PathTraveller.transform.rotation);
      currentPlatform.transform.parent = path.transform;

      // Add obstacles on platforms of platformZ type
      if (platforms[platformIndex].tag == "platformZ" && i != 0)
      {
        int obstacleIndex = Random.Range(0, obstacles.Length);
        PathTraveller.transform.position += obstacleVector;
        GameObject logicGate = Instantiate(obstacles[obstacleIndex], PathTraveller.transform.position, PathTraveller.transform.rotation);
        PathTraveller.transform.position -= obstacleVector;
        logicGate.transform.parent = path.transform;
      }

      // if platform is of T intersection type
      // break loop to generate path
      // wait for player turn to start generating plaforms again in the direction the player turned
      currentPathCount += 1;
      if (platforms[platformIndex].tag == "platformTSection")
      {
        break;
      }
      // Translate to move 10 units further in z axis
      PathTraveller.transform.Translate(Vector3.forward * -10);
    }
  }

  public void UpdatePath()
  {
    pathsLeftToGenerate = numOfPlatformsToGenerate - currentPathCount;
    if (currentPathCount >= numOfPlatformsToGenerate && stopGeneratingPath == false)
    {
      Transform lastChild = path.transform.GetChild(path.transform.childCount - 1);
      GameObject lastChildObject = lastChild.gameObject;
      if (lastChildObject.tag == "platformTSection")
      {
        Destroy(lastChildObject);
      }

      GameObject finalPlatformOnPath = Instantiate(finalPlatform, PathTraveller.transform.position, PathTraveller.transform.rotation);
      stopGeneratingPath = true;
    }
  }
  public void SetAngleToRotateByPath(float angle)
  {
    PathTraveller.transform.Rotate(new Vector3(0, angle, 0));
    PathTraveller.transform.Translate(Vector3.forward * -25);
    LoopToGeneratePath(pathsLeftToGenerate);
  }
}
