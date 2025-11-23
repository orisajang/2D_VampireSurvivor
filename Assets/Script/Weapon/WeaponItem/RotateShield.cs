using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateShield : MonoBehaviour
{

    [SerializeField] Transform rotatePoint;
    [SerializeField] float rotateSpeed = 10.0f;

    public void SetRotatePoint(Transform trf)
    {
        rotatePoint = trf;
    }

    private void FixedUpdate()
    {
        if(rotatePoint != null)
        {
            transform.RotateAround(rotatePoint.position, Vector3.forward, rotateSpeed * Time.fixedDeltaTime);
        }
    }
}
