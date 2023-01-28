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
  [SerializeField] float xAngle = 3f;
  private void FixedUpdate()
  {
    SpinObject();
  }

  private void SpinObject()
  {
    this.gameObject.transform.Rotate(new Vector3(xAngle, 0, 0));
  }
}
