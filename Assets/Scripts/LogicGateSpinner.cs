/* ==========================================================================================================================================
Author: Prajwal Belagatti
Class: Logic Gate
Description: Handles movement of Logic Gates of Spinner Type
============================================================================================================================================= */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGateSpinner : MonoBehaviour
{
  [SerializeField] float spinAngle = 3f;
  private void Start()
  {
    SetupSpinner();
  }
  private void SetupSpinner()
  {
    this.gameObject.transform.Rotate(new Vector3(0, 90, 0));
    if (Random.Range(0, 2) == 0)
      spinAngle = -spinAngle;
  }
  private void FixedUpdate()
  {
    SpinObject();
  }

  private void SpinObject()
  {
    this.gameObject.transform.Rotate(new Vector3(0, spinAngle, 0));
  }
}
