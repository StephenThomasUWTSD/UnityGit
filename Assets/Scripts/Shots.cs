using UnityEngine;
using System.Collections;
using System;

public class Shots : MonoBehaviour
{
   public CrossHair transfer;
    public float shotSpeed;

	// Use this for initialization
	void Start ()
	{
	    transfer.rico();
        GetComponent<Rigidbody>().velocity = transform.forward*shotSpeed;
        Destroy(gameObject, 20); // 20sec
    }

   
}
