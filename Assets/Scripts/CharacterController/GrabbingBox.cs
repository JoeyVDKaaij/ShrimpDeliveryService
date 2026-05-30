using UnityEngine;
using UnityEngine.InputSystem;

public class GrabbingBox : MonoBehaviour
{

    [SerializeField] private InputActionReference GrabAction;
    [SerializeField] private InputActionReference ThrowAction;

    [SerializeField] CameraRotation cameraScript;

    [SerializeField] float holdingDistance;
    [SerializeField] float forceToApply;
    [SerializeField] float dampingForce;

    [SerializeField] float throwForce;

    Vector3 targetPosition;

    GameObject selectedBox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (GrabAction == null) return;

        GrabAction.action.performed += SelectBox;
        GrabAction.action.Enable();

        if (ThrowAction == null) return;

        ThrowAction.action.performed += ThrowBox;
        ThrowAction.action.Enable();
    }

    void SelectBox(InputAction.CallbackContext cxt)
    {
        RaycastHit hit;

        if (Physics.Raycast(cameraScript.transform.position, transform.TransformDirection(cameraScript.transform.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.CompareTag("Box"))
            {
                if (hit.collider.gameObject != selectedBox)
                {
                    selectedBox = hit.collider.gameObject;
                }
                else
                {
                    selectedBox = null;
                }
            }
            else
            {
                selectedBox = null;
            }
        }
        else
        {
            selectedBox = null;
        }
    }

    void ThrowBox(InputAction.CallbackContext cxt)
    {
        if (selectedBox != null)
        {
            selectedBox.GetComponent<Rigidbody>().AddForce(cameraScript.transform.forward * throwForce);
            selectedBox = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedBox != null)
        {
            targetPosition = cameraScript.transform.position + cameraScript.transform.forward * holdingDistance;

            Rigidbody rb = selectedBox.GetComponent<Rigidbody>();

            rb.AddForce((targetPosition - selectedBox.transform.position) * forceToApply);

            rb.AddForce(-rb.linearVelocity * dampingForce);
        }
    }
}
