using UnityEngine;
using System.Collections;
using JetBrains.Annotations;

public class Laser : MonoBehaviour {
    public GameObject Shot;
    public Transform ShotSpawn;
    public float fireRate;
    private float nextFire;
   
    // Use this for initialization
    void Start () {
        //contact.fullon();
    }
    
    // Update is called once per frame
    public void Update ()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            GameObject cross = GameObject.Find("CrossHair");
            //GameObject CrossHairPosition = cross.GetComponent<CrossHair>;//public
            
          
            nextFire = Time.time + fireRate;
            GameObject clone = Instantiate(Shot, ShotSpawn.position, ShotSpawn.rotation) as GameObject;

        }
    }
}
        //GameObject thePlayer = GameObject.Find("ThePlayer");
//PlayerScript playerScript = thePlayer.GetComponent<PlayerScript>();