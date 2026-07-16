using UnityEngine;

public class OrbitCamera : MonoBehaviour
{

    [SerializeField] Transform target;
    [SerializeField] Transform targetParent;

    [SerializeField] float smoothing;

    [SerializeField] float screenShakeSpeedMult;
    [SerializeField] float screenShakeAngleMult;
    [SerializeField] float maxShakingIntensity;
    [SerializeField] Rigidbody carRB;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position, smoothing);
        transform.rotation = target.rotation;

        float slidingAngle = Vector3.Angle(carRB.transform.forward, carRB.linearVelocity);
        //sliding backwards
        if (slidingAngle > 90) slidingAngle = 90 - (slidingAngle - 90);

        float slidingIntensity = Mathf.Max(((carRB.linearVelocity.magnitude - 15) * screenShakeSpeedMult) * (slidingAngle * screenShakeAngleMult), 0);

        float screenShakeMagnitude = Mathf.Min(Mathf.Max((carRB.linearVelocity.magnitude - 30) * screenShakeSpeedMult, 0) + slidingIntensity, maxShakingIntensity);

        //Debug.Log(((carRB.velocity.magnitude - 15) * screenShakeSpeedMult) + " - " + (slidingAngle * screenShakeAngleMult) + " - " + slidingIntensity);

        if (screenShakeMagnitude > 0)
        {



            transform.position += new Vector3(Random.Range(-screenShakeMagnitude, screenShakeMagnitude), Random.Range(-screenShakeMagnitude, screenShakeMagnitude), Random.Range(-screenShakeMagnitude, screenShakeMagnitude));
            Vector3 newRot = transform.rotation.eulerAngles + new Vector3(Random.Range(-screenShakeMagnitude, screenShakeMagnitude), Random.Range(-screenShakeMagnitude, screenShakeMagnitude), Random.Range(-screenShakeMagnitude, screenShakeMagnitude));
            transform.rotation.eulerAngles.Set(newRot.x, newRot.y, newRot.z);
        }
    }
}

