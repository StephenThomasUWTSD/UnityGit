using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
    private Transform player;
	// Use this for initialization
	void Start ()
	{
	    player = GameObject.Find("PlayerFighter").GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	    transform.LookAt(player);
	    transform.position = Vector3.MoveTowards(transform.position, player.position, 2*Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().name == "PlayerFighter")
        {
            Application.LoadLevel(0);
        }
    }
}
