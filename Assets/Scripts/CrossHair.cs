using System;
using UnityEngine;
using System.Collections;

public class CrossHair : MonoBehaviour
{
    public Shots transfer;
    
    private float range = 20;
    
    
    //public abstract CrossHair func();
     void Start()
     {
         
     }
    public Texture Cross;
    public void OnGUI()
    {
       //go.transform.position = Hit.point;

        Ray myRay = new Ray(transform.position, transform.forward);
        Vector3 CrossHairPosition = Camera.main.WorldToScreenPoint(myRay.GetPoint(range));
        CrossHairPosition.y = Screen.height - CrossHairPosition.y;

        GUI.Label(new Rect(CrossHairPosition.x - (Cross.width / 2), CrossHairPosition.y - (Cross.height / 2), Cross.width, Cross.height), Cross); //Crosshair is 2D texture
    }

   

   
}



/*
public class GunScript : MonoBehaviour
{

    public Texture CrossHair;
    public float Power = 10;
    private Vector3 scale = new Vector3(0.5f, 0.5f, 0.5f);

    public void CastRay()
    {
        RaycastHit Hit;
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.localScale = scale;

        if (Physics.Raycast(transform.position, transform.forward, out Hit, Power))
        {
            go.transform.position = Hit.point;
            if (Hit.transform.gameObject.tag == "target")
            {
                Destroy(Hit.transform.gameObject);
            }
        }
        else
            go.transform.position = transform.position + transform.forward * Power;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            CastRay();
            Ray myRay = new Ray(transform.position, transform.forward);
            Vector3 CrossHairPosition = Camera.main.WorldToScreenPoint(myRay.GetPoint(Power));

            GUI.Label(new Rect(CrossHairPosition.x - (CrossHair.width / 2), CrossHairPosition.y - (CrossHair.height / 2), CrossHair.width, CrossHair.height), CrossHair); //Crosshair is 2D texture
        }
    }

    void OnGUI()
    {
        Ray myRay = new Ray(transform.position, transform.forward);
        Vector3 CrossHairPosition = Camera.main.WorldToScreenPoint(myRay.GetPoint(Power));
        CrossHairPosition.y = Screen.height - CrossHairPosition.y;

        GUI.Label(new Rect(CrossHairPosition.x - (CrossHair.width / 2), CrossHairPosition.y - (CrossHair.height / 2), CrossHair.width, CrossHair.height), CrossHair); //Crosshair is 2D texture
    }
}
/*
public class CrossHair : MonoBehaviour
{

    public Transform crosshair;
    public Transform endpoint;
    public Vector3 pointCursor;
    public Transform firePoint;
    public Rect labelRect;
    public Texture crosshairtex;
    public float hit;
    public bool hitSomething;
    // Update is called once per frame
    void Update()
    {
        bool foundHit;
       
        RaycastHit hit = new RaycastHit();
        foundHit = Physics.Raycast(transform.position, transform.forward, out hit, 20);
        //Debug.DrawLine(trnsform.position, hit.points);
        crosshair.position = foundHit ? hit.point : endpoint.position;
        if (Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.left), out hit))
        {
            // Use the hit point instead:
            pointCursor = Camera.main.WorldToScreenPoint(hit.point);
        }
        // assign the raycast result to it: 
        hitSomething = Physics.Raycast(firePoint.position, firePoint.TransformDirection(Vector3.left), out hit);
        if (hitSomething)
        {
            pointCursor = Camera.main.WorldToScreenPoint(hit.point);
        }
    }

    void OnGui()
    {
        if (hitSomething) // only draw the crosshair if something hit
        {
            Vector2 vector2 = GUIUtility.ScreenToGUIPoint(new Vector2(pointCursor.x, pointCursor.y));
        }
        GUI.DrawTexture(labelRect, crosshairtex);
    }
}

/*
    void OnGUI()
    {
        Vector2 vector2 = GUIUtility.ScreenToGUIPoint(new Vector2(pointCursor.x, pointCursor.y));
        Rect labelRect = new Rect();
        labelRect.x = vector2.x;
        labelRect.y = vector2.y;
        labelRect.width = cursorTexture.width;
        labelRect.height = cursorTexture.height;

        GUI.DrawTexture(labelRect, cursorTexture);
    }

}


/*
// calculate the hit position:
var t = HitTime(target, targetVel, bulletSpeed);
 if (t< 0){
   // target moving away too fast to be reached
   guiCross.enabled = false;
 } else {
   // target may be hit:
   guiCross.enabled = true;
   // calculate the position where the target will be hit
   var hitPos = target.position + t * targetVel;
// calculate the crosshair position
var crossPos = Camera.main.WorldToViewportPoint(hitPos);
guiCross.transform.position = crossPos;
 }
 
 if (Input.GetButtonDown("Fire1")){
   var missile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
   Destroy(missile, 20); // kill the missile after 20s
missile.AddComponent(Rigidbody);
   missile.rigidbody.position = transform.position;
   missile.rigidbody.useGravity = false;
   
   // use this to shoot at the calculated point:
   var dir = hitPos - transform.position;
missile.rigidbody.velocity = dir.normalized* bulletSpeed;
 }

// Give it an initial forward velocity. The direction is along the z-axis of the missile launcher's transform.
//instantiatedProjectile.velocity = Camera.main.ScreenPointToRay(Input.mousePosition).direction * initialSpeed;;
//Sets up the RayCast to the direction of the cross hairs
/*
 * 
 * 
 * CODE FOR RAYCAST FIRING AT CROSSHAIRhttp://answers.unity3d.com/questions/23683/air-strike-shoot-aiming-at-cross-hairs-in-middle-o.html
var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
var hit : RaycastHit;
         Physics.Raycast(ray, hit);
         //gives position of the RayCast
         instantiatedProjectile.MovePosition(hit.point);
         //gives the initial forward velocity
         instantiatedProjectile.velocity = ray.direction* initialSpeed;
// Ignore collisions between the missile and the character controller
Physics.IgnoreCollision(instantiatedProjectile.collider, transform.root.collider);
         lastShot = Time.time;
         ammoCount--;
     }
     Camera.main.ViewportToScreenPoint(new Vector3(0.5F, 0.5F, 0)).x;
 */

