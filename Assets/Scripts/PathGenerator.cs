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
  // Class properties
  [SerializeField] GameObject path; // Reference to path game object
  [SerializeField] GameObject[] platforms; // Reference to array of different platform gameobjects we can possibly generate
  [SerializeField] GameObject[] obstacles; // Reference to array of different obstacles gameobjects we can possibly generate
  [SerializeField] GameObject finalPlatform; // Reference to final platform gameobject that completes the level
  private GameObject PathTraveller; // An empty game object that travels the path to generate path for the player
  public int numOfPlatformsToGenerate = 20; // Total number of platforms before we generate the final platform
  private Vector3 obstacleVector = new Vector3(0, 1, 0); // A vector to denote starting position of obstacle when we instantiate obstacles
  private int currentPathCount = 0; // Total number of platforms already generated on the path
  private int pathsLeftToGenerate; // Paths left to generate based on currentPathCount and numOfPlatformsToGenerate
  private bool stopGeneratingPath = false; // Bool to stop generating more platforms on the path

  // Class Methods
  // Start Method
  public void Start()
  {
    // Calculate platforms left to generate on path
    pathsLeftToGenerate = numOfPlatformsToGenerate - currentPathCount;
    // Call generate path method
    GeneratePath();
  }

  // Update Method
  private void Update()
  {
    // Call Update Path Method to keep a check of platforms to be generated
    UpdatePath();
  }

  public void GeneratePath()
  {
    // Instantiate a game object to create path
    // The transform and rotation property will be used to generate path of gameobjects of different platform types in the prefabs
    PathTraveller = new GameObject("Path Traveller");
    // Create first n platforms on the path with loop to generate method
    LoopToGeneratePath(pathsLeftToGenerate);
  }

  // Loop To Generate Path Method
  // A looping method to generate path out of available array of paltforms
  // Is called when we reach a T section platform to generate path based on the direction rotated by the player
  public void LoopToGeneratePath(int numPath)
  {
    // If stopGeneratingPath bool is true, stop creating further platforms on path
    if (stopGeneratingPath == true) { return; }

    // Loop from 0 to numPath provided as an argument to LoopToGeneratePath Method
    for (int i = 0; i < numPath; i++)
    {
      // Get a random/ unique platform index from available platforms types (0 to last index) and instantiate them from their prefabs 
      int platformIndex = Random.Range(0, platforms.Length);

      // if the first or second iteration of the loop creates a T section platform from previous index break out of the loop
      // This helps balance level difficulty and generate better paths. 
      if (((i == 0) || (i == 1) || (i == numPath - 1)) && (platforms[platformIndex].tag == "platformTSection"))
      {
        LoopToGeneratePath(pathsLeftToGenerate);
        break;
      }

      // Else instantiate further platforms
      GameObject currentPlatform = Instantiate(platforms[platformIndex], PathTraveller.transform.position, PathTraveller.transform.rotation);

      // Set the position of the newly created platform to equal path transform and its position and rotation values
      currentPlatform.transform.parent = path.transform;

      // If the platform generated is of of platformZ type, add obstacles on platforms
      // We avoid adding obstacles on other platforms and immediately on the first platform after rotate to balance level difficulty 
      if (platforms[platformIndex].tag == "platformZ" && i != 0)
      {
        // Choose an obstacle from the array of available obstacles
        int obstacleIndex = Random.Range(0, obstacles.Length);

        // Offset the PathTraveller game object's position to set initial position of obstacle
        PathTraveller.transform.position += obstacleVector;

        // Instantiate the obstacle
        GameObject logicGate = Instantiate(obstacles[obstacleIndex], PathTraveller.transform.position, PathTraveller.transform.rotation);

        // Correct the offset on the PathTraveller game object's position
        PathTraveller.transform.position -= obstacleVector;

        // Make the obstacle a child of path game object
        logicGate.transform.parent = path.transform;
      }

      // If platform is of T intersection type
      // Break the loop to stop generating path
      // Start generating plaforms again in the direction the player took
      currentPathCount += 1;
      if (platforms[platformIndex].tag == "platformTSection")
      {
        break;
      }
      // Translate to move 10 units further in z axis to move the path forward
      PathTraveller.transform.Translate(Vector3.forward * -10);
    }
  }

  // Update Path Method
  // Called to keep a check of platforms to be generated
  public void UpdatePath()
  {
    // Calculate platforms left to generate on path
    pathsLeftToGenerate = numOfPlatformsToGenerate - currentPathCount;

    // Call CheckLastPlatformAndInstantiateFinishPlatform Method to check if we should generate last/ level finishing platform
    CheckLastPlatformAndInstantiateFinishPlatform();
  }

  // Set Angle To Rotate By Path Method
  // Rotates the path and further generation of platforms in the direction the player turned
  public void SetAngleToRotateByPath(float angle)
  {
    // Call Destory Old Platforms Method to destroy old platforms
    DestoryOldPlatforms();

    // Call Rotate and Translate Method on path traveller game object's transform
    PathTraveller.transform.Rotate(new Vector3(0, angle, 0));
    PathTraveller.transform.Translate(Vector3.forward * -25);

    // Start generating paths again with pathsLeftToGenerate as argument to LoopToGeneratePath Method
    LoopToGeneratePath(pathsLeftToGenerate);
  }

  // Check Last Platform And Instantiate Finish Platform
  // Check if we should generate last/ level finishing platform
  private void CheckLastPlatformAndInstantiateFinishPlatform()
  {

    // If current number of platforms is greater than total number of platforms on the level, create the last platform
    if (currentPathCount >= numOfPlatformsToGenerate && stopGeneratingPath == false)
    {
      // Get the transform of last child on path game object
      Transform lastChild = path.transform.GetChild(path.transform.childCount - 1);

      // If the last game object before the final platform is a T section platform delete the T section platform
      // This helps preventing the creation of a T section platform before the final platform
      GameObject lastChildObject = lastChild.gameObject;
      if (lastChildObject.tag == "platformTSection")
      {
        Destroy(lastChildObject);
      }

      // Instantiate the final platform game object from the final platform prefab
      GameObject finalPlatformOnPath = Instantiate(finalPlatform, PathTraveller.transform.position, PathTraveller.transform.rotation);

      // Set stopGeneratingPath bool to true to stop generating path after the final platform
      stopGeneratingPath = true;
    }
  }

  // Destory Old Platforms Method
  // Used to destroy platfroms from array of game objects in path after rotation by player
  // Used to prevent criss-cross of new platforms generated by older platforms after rotation by player
  private void DestoryOldPlatforms()
  {
    // Destory the start and initial platform created as runway for the level to start
    Destroy(GameObject.FindGameObjectWithTag("Start"));
    Destroy(GameObject.FindGameObjectWithTag("initPlatform"));

    // Get count of platforms to be deleted after rotation by player
    int delCount = path.transform.childCount;

    // Set iteration variable to 0
    int iter = 0;

    // Loop through path game object and delete all the children platforms and obstacles
    foreach (Transform p in path.transform.GetComponentInChildren<Transform>())
    {
      // Increment iter
      iter++;

      // Delete all the game objects till he last two gameobjects
      // This is to avoid deletion of Tsection platform the player is currently on
      // And add a buffer on one more platform
      if (iter <= delCount - 2)
      {
        // Destroy game object
        Destroy(p.gameObject);
      }
    }
  }
}
