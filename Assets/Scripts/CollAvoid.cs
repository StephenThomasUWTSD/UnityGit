using UnityEngine;
using System.Collections;

public class CollAvoid : MonoBehaviour {
    float targetYouAreFollowing;

    // Use this for initialization
    void Start () {
       

    }
   
    // Update is called once per frame
    void Update ()
    {
        Vector3 target = transform.position / targetYouAreFollowing; 
        Vector3 player = transform.position;

        target = target - player;

        //normalize it to get direction
        target = target.normalized;

        //now make a new raycast hit
        //and draw a line from the AI out some distance in the ‘forward direction

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 800f))
        {

            //check that its not hitting itself
            //then add the normalised hit direction to your direction plus some repulsion force -in my case // 400f

            if (hit.transform != transform)
            {
                Debug.DrawLine(transform.position, hit.point, Color.red);

                target += hit.normal * 400f;
            }

        }

        //now make two more raycasts out to the left and right to make the cornering more accurate and reducing collisions more

        Vector3 leftR = transform.position;
        Vector3 rightR = transform.position;

        leftR.x -= 2;
        rightR.x += 2;

        if (Physics.Raycast(leftR, transform.forward, out hit, 800f))
        {
            if (hit.transform != transform)
            {
                Debug.DrawLine(leftR, hit.point, Color.red);
                target += hit.normal * 400f;
            }

        }
        if (Physics.Raycast(rightR, transform.forward, out hit, 800f))
        {
            if (hit.transform != transform)
            {
                Debug.DrawLine(rightR, hit.point, Color.red);

                target += hit.normal * 400f;
            }

        }

        // then set the look rotation toward this new target based on the collisions

        Quaternion torotation = Quaternion.LookRotation(target);

        //then slerp the rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, torotation, Time.deltaTime * 100f);

        //finally add some propulsion to move the object forward based on this rotation
        //mine is a little more complicated than below but you hopefully get the idea…

        transform.position += transform.forward * 20f * Time.deltaTime;

    }
}


