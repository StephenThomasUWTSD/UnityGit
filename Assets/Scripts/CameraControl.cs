﻿using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {


    public Transform target;

    public Vector3 distance = new Vector3(0f, 0f, -5f);

    public float positionDamping = 10.0f;
    public float rotateDamping = 10.0f;

    private Transform thisTransform;

    // Use this for initialization
    void Start () {
        thisTransform = transform; // cache the transform component of this camera so it doesnt need to be looked up each frame
	
	}

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate() {

        if(target == null)
        {
            return;
        }

        //this is the stuff that is controlling the camera position in the world
        Vector3 wantedPosition = target.position + (target.rotation * distance);
        Vector3 currentPosition = Vector3.Lerp(thisTransform.position, wantedPosition, positionDamping * Time.deltaTime);
        thisTransform.position = currentPosition;

        //this is the stuff that is controlling the camera rotation in the world
        Quaternion wantedRotation = Quaternion.LookRotation(target.position - thisTransform.position, target.up);
        
        thisTransform.rotation = wantedRotation;
    }
}
