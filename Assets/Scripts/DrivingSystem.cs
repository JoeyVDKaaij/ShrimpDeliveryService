using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct Wheel
{
    public Transform transform;
    public bool powered;
    public bool steering;
}

public class DrivingSystem : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Wheel[] wheels;

    [SerializeField] float enginePower;
    [SerializeField] float forwardFriction;
    [SerializeField] float sidewaysFriction;

    [SerializeField] float suspensionLength;
    [SerializeField] float wheelSize;

    [SerializeField] float steeringAngle;

    [SerializeField] float suspensionRestDist;
    [SerializeField] float springStrength;
    [SerializeField] float springDamper;

    [SerializeField] float tyresMaxGrip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        int i = 0;

        foreach (Wheel wheel in wheels)
        {
            Transform t = wheel.transform;

            RaycastHit hit;

            bool rayHit = Physics.Raycast(t.position, -t.up, out hit, suspensionLength + wheelSize);

            //suspension
            if (rayHit)
            {
                Debug.DrawRay(t.position, -t.up * hit.distance, Color.yellow);

                Vector3 springDir = t.up;
                Vector3 tireWorldVel = rb.GetPointVelocity(t.position);

                float offset = suspensionRestDist - hit.distance;
                float vel = Vector3.Dot(springDir, tireWorldVel);
                float force = (offset * springStrength) - (vel * springDamper);

                rb.AddForceAtPosition(springDir * force, t.position);

                //wheel.mesh.localPosition = new Vector3(0, -hit.distance + wheelSize, 0);
            }
            else
            {
                Debug.DrawRay(t.position, -t.up * wheelSize, Color.red);
            }

            //steering
            if (wheel.steering)
            {
                float steeringInput = 0;
                
                if (Input.GetKey(KeyCode.D)) steeringInput++;
                if (Input.GetKey(KeyCode.A)) steeringInput--;

                t.localRotation = Quaternion.Euler(0, steeringInput * steeringAngle, 0);
            }


            if (!Input.GetKey(KeyCode.Space))
            {
                if (rayHit)
                {
                    Vector3 steeringDir = t.right;
                    Vector3 tireWorldVel = rb.GetPointVelocity(t.position);

                    float steeringVel = Vector3.Dot(steeringDir, tireWorldVel);
                    float desiredVelChange = -steeringVel * 1;
                    float desiredForce = rb.mass * desiredVelChange;

                    float totalSteeringForce;
                    if (desiredForce >= 0) totalSteeringForce = Mathf.Min(desiredForce, tyresMaxGrip);
                    else totalSteeringForce = Mathf.Max(desiredForce, -tyresMaxGrip);

                    rb.AddForceAtPosition(steeringDir * totalSteeringForce, t.position);
                }
            }


            //acceleration
            if (rayHit && wheel.powered)
            {
                Vector3 accelDir = t.forward;

                //if(accelInput > 0)
                if (Input.GetKey(KeyCode.W))
                {
                    //float carSpeed = Vector3.Dot(transform.forward, rb.velocity);
                    //float normalizedSpeed = Mathf.Clamp01(Mathf.Abs(carSpeed) / carTopSpeed);
                    //float availableTorque = testTorque;

                    rb.AddForceAtPosition((accelDir * (enginePower * 1000) * Time.fixedDeltaTime), t.position);

                }
                //braking
                if (Input.GetKey(KeyCode.S) || (Input.GetKey(KeyCode.Space)))
                {
                    Vector3 brakingDir = rb.transform.forward;
                    Vector3 tireWorldVel = rb.GetPointVelocity(t.position);

                    float brakingVel = Vector3.Dot(brakingDir, tireWorldVel);
                    float desiredVelChange = -brakingVel;
                    float desiredForce = rb.mass * desiredVelChange;

                    float totalBrakingForce;
                    if (desiredForce >= 0) totalBrakingForce = Mathf.Min(desiredForce, tyresMaxGrip);
                    else totalBrakingForce = Mathf.Max(desiredForce, -tyresMaxGrip);

                    //if (Mathf.Abs(desiredForce) > carData.tyresMaxGrip) { wheel.trail.emitting = true; wheel.smokeEffect.Play(); }

                    rb.AddForceAtPosition(brakingDir * totalBrakingForce, t.position);
                }
            }



            i++;

        }
    }
}
