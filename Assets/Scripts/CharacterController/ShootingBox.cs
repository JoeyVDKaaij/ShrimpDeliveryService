using UnityEngine;
using UnityEngine.InputSystem;

public class ShootingBox : MonoBehaviour
{
    [SerializeField, Tooltip("Set up the button action reference that allows for the player to shoot.")]
    private InputActionReference shootingButton;

    [SerializeField, Tooltip("Set up the projectile to shoot.")] 
    private Rigidbody projectile;
    [SerializeField, Min(0), Tooltip("Set up the force in which the projectile will be shot at.")]
    private float projectileForce = 10;
    
    // Links the shooting button with the shooting function.
    void Start()
    {
        ShootingButtonSetup();
    }

    private void ShootingButtonSetup()
    {
        if (shootingButton == null) return;

        shootingButton.action.performed += Shoots;
        shootingButton.action.Enable();
    }

    /// <summary>
    /// Shoots a projectile at a set speed.
    /// </summary>
    private void Shoots(InputAction.CallbackContext cxt)
    {
        if (projectile == null) return;
        
        Vector3 projectileLocation = transform.position + transform.forward;
        
        Rigidbody projectileInstance = Instantiate(projectile, projectileLocation, transform.rotation);
        
        projectileInstance.AddForce(transform.forward * projectileForce, ForceMode.Impulse);
    }
}
