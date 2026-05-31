using UnityEngine;
using UnityEngine.VFX;

public class Customer : MonoBehaviour
{

    int desiredPackage;

    TargetManager targetManager;

    [SerializeField] VisualEffect happyEffect;
    [SerializeField] VisualEffect sadEffect;

    [SerializeField] float despawnDelay;
    float despawnTimer;
    bool isDespawning;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void Initialize(TargetManager inTargetManager, int desiredPackage)
    {
        this.targetManager = inTargetManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDespawning)
        {
            despawnTimer += Time.deltaTime;
            if (despawnTimer > despawnDelay)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Box") && !isDespawning)
        {
            if(other.gameObject.GetComponent<BoxIdentifier>().packageType == desiredPackage)
            {
                happyEffect.Play();

                targetManager.removeActiveTarget(this);
            }
            else
            {
                sadEffect.Play();

                targetManager.removeActiveTarget(this);
            }

            Destroy(other.gameObject);
            isDespawning = true;
        }
    }
}
