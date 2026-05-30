using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField, Min(0), Tooltip("Set the movement speed.")]
    private float speed;
    [SerializeField, Tooltip("Set the input to the movement mechanics.")]
    private InputActionReference inputAction;
    [SerializeField, Tooltip("Set the camera rotation script that " +
                             "will be used to set the direction of the movement.")]
    private CameraRotation cameraRotation;

    private Rigidbody _rb;
    private Vector3 _velocity;
    private PlayerInput _playerInput;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions.devices = new[]{Gamepad.all[0]};
    }

    private void Update()
    {
        // Movement
        _velocity = new Vector3(0, 1 * _velocity.y, 0);

        Vector2 inputDirectionVec2 = inputAction.action.ReadValue<Vector2>();
        
        Vector3 inputDirectionVec3 = new Vector3(inputDirectionVec2.x, 0, inputDirectionVec2.y);
        
        UpdateVelocityByCameraDirection(ref inputDirectionVec3);
        
        _velocity += inputDirectionVec3 * speed;
        
        _rb.linearVelocity = _velocity * Time.deltaTime;
    }

    /// <summary>
    /// Updates the given velocity based on the direction where the camera looks at.
    /// </summary>
    private void UpdateVelocityByCameraDirection(ref Vector3 pVelocity)
    {
        if (cameraRotation == null) return;

        Vector3 horizontalDirection = pVelocity.x * cameraRotation.transform.right;
        Vector3 verticalDirection = pVelocity.z * cameraRotation.transform.forward;
        Vector3 direction = horizontalDirection + verticalDirection;

        pVelocity = new Vector3(direction.x, pVelocity.y, direction.z);
    }
}
