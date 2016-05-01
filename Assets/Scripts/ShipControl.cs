using UnityEditor.AnimatedValues;
using UnityEngine;

public class ShipControl : MonoBehaviour
{
    //public Texture Cross;
    //The purpose of this script is to simulate Newtonian phy
    private float ThrustWeight = 200; //The maximum Thrust provided by the thruster(s) at full throttle
    private float rollWeight = 5; //This float and the next two only serve to adjust sensitivity
    private float pitchWeight = 5;//of the controls, and to allow calibration for more massive ships.
    private float yawWeight = 5;//Set these 3 floats to the mass of the rigidbody for sensitive controls
    private float strafeWeight =200;
    //private float strafe;
   // private float throttle;


    

    // Update is called once per frame
    void Update()
    {
   
    }

    void FixedUpdate()
    {
        //Vector3 jumpVelocity = default(Vector3);
        //GetComponent<Rigidbody>(maxThrust AddForce.VelocityChange);
        float yaw = yawWeight * Input.GetAxis("Yaw");
        float roll = rollWeight * Input.GetAxis("Roll");
        float pitch = pitchWeight * Input.GetAxis("Pitch");
        //float strafe = strafeWeight*Input.GetAxis("Strafe");
        //Vector3 rotation = new Vector3(pitch, roll, yaw);
        float throttle = Input.GetAxis("Throttle");
        //GetComponent<Rigidbody>().AddRelativeTorque(rotation * Time.deltaTime);
        GetComponent<Rigidbody>().AddRelativeTorque(pitch* pitchWeight*Time.deltaTime,yaw *yawWeight*Time.deltaTime,roll* rollWeight*Time.deltaTime);
        GetComponent<Rigidbody>().AddRelativeForce(0,0,throttle*ThrustWeight*Time.deltaTime);
        var strafe =new Vector3(Input.GetAxis("Horizontal") * strafeWeight * Time.deltaTime, Input.GetAxis("Vertical") * strafeWeight * Time.deltaTime, 0);

        GetComponent<Rigidbody>().AddRelativeForce(strafe);
        //Vector3 strafe = new Vector3(pitch, yaw, moveDirection.y * Time.deltaTime);

        //Quaternion.LookRotation(strafe + rotation * Time.deltaTime);

        //GetComponent<Rigidbody>().AddRelativeForce(0, 1, strafe * Time.deltaTime);
        //GetComponent<Rigidbody>().AddRelativeForce(strafe * Time.deltaTime);
        //Quaternion strafe = Quaternion.Lerp(rotation);
        // store rotated direction in variable
        //Quaternion direction = Quaternion.LookRotation(strafe * Time.deltaTime);
        // interpolate (transition) the change gradually
        //transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotation.x * Time.deltaTime);
        //float strafing = Input.GetAxis("Strafing");
        //GetComponent<Rigidbody>().AddRelativeForce(0, 1, strafing * Time.deltaTime);


        // Vector3 left = (Vector3.GetAxis("Strafing"));
        //Vector3 right = (Vector3.Input.GetAxis("Strafing2"));


        //System.Console.WriteLine("input is " + yaw.ToString() + ", " + pitch.ToString() + ", " + roll.ToString());
        //Strafing move Q/E movement     



        //Vector3.Lerp vector3 = transform.position.(rotation,moveDirection,strafing);
        //Quaternion strafe2=new Quaternion(-strafe,strafe,-strafe,strafe);
        //GetComponent<Rigidbody>().AddRelativeForce(Quaternion.Lerp(strafe2,strafe2,strafeWeight));
        //GetComponent<Rigidbody>().AddRelativeForce(Vector2.right*throttle);
    }

}