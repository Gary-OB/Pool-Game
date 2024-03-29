﻿using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{

    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;

    void Start()
    {
        offset = target.transform.position - transform.position;
    }

    void LateUpdate()
    {

        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        target.transform.Rotate(vertical, horizontal, 0);

        float desiredAngleY = target.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngleY, 0);
        transform.position = target.transform.position - (rotation * offset); ;

        transform.LookAt(target.transform);

    }
}