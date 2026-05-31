using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField, Tooltip("Set the input action that allows you to rotate the camera.")]
    private InputActionReference inputAction;

    [SerializeField, Min(0), Tooltip("Set the camera rotation speed.")] 
    private float rotationSpeed = 1;
    [SerializeField, Tooltip("Set the camera rotation speed.")] 
    private Transform linkedObjects = null;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Adds rotation to the camera
        Vector2 inputDirectionVec2 = inputAction.action.ReadValue<Vector2>();

        Vector3 currentRotation = transform.rotation.eulerAngles;
        currentRotation.x += -inputDirectionVec2.y * Time.deltaTime * rotationSpeed;
        currentRotation.y += inputDirectionVec2.x * Time.deltaTime * rotationSpeed;
        
        transform.rotation = Quaternion.Euler(currentRotation);

        if (linkedObjects == null) return;
        
        linkedObjects.rotation = Quaternion.Euler(new Vector3(-90, transform.rotation.eulerAngles.y + 90, 0));
    }
}
