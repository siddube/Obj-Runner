/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Logic Gate
Description: Handles movement of Logic Gates of Mover Type
============================================================================================================================================= */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGateMover : MonoBehaviour
{
  Vector3 startingPosition;
  [SerializeField] Vector3 movementVector;
  [SerializeField][Range(0, 1)] float movementFactor;
  [SerializeField] float period = 2f;
  private void Start()
  {
    SetupMovement();
  }

  private void SetupMovement()
  {
    startingPosition = transform.position;
    this.gameObject.transform.Rotate(new Vector3(0, 90, 90));
    if (Random.Range(0, 2) == 0)
      movementVector = new Vector3(0, 5, 0);
    else
      movementVector = new Vector3(0, 0, 5);
  }

  private void FixedUpdate()
  {
    MoveObject();
  }

  private void MoveObject()
  {
    float cycles = Time.time / period;

    const float tau = Mathf.PI * 2;
    float rawSinWave = Mathf.Sin(cycles * tau);

    movementFactor = (rawSinWave + 1f) / 2f;

    Vector3 offset = movementVector * movementFactor;
    transform.position = startingPosition + offset;
  }
}
