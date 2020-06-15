using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public bool thirdPerson;
    //public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            target = GameObject.FindGameObjectWithTag("PlayerHead").transform;


        }
        catch (NullReferenceException ex)
        {
            Debug.Log("PlayerHead nu a fost gasit.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPos = new Vector3(target.position.x, target.position.y, target.position.z); 
        Vector3 targetCameraPosition = target.position;
        transform.position = targetCameraPosition;
 
    }
}
