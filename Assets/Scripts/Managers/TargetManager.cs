using UnityEngine;
using System.Collections.Generic;

public class TargetManager : MonoBehaviour
{

    [SerializeField] GameObject targetPrefab;

    [SerializeField] Transform[] possibleSpawnLocations;

    [SerializeField] int numSimultaneousTargets;

    List<GameObject> activeTargets;

    [SerializeField] PackageSpawner packageSpawner;

    List<PhysicalPackage> unspawnedPackages;

    int numCurrentPackages;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeTargets = new List<GameObject>();
        unspawnedPackages = new List<PhysicalPackage>();
    }

    public void AddUnspawnedPackage(PhysicalPackage newPackage)
    {
        unspawnedPackages.Add(newPackage);
    }

    public void removeActiveTarget(Customer customer)
    {
        activeTargets.Remove(customer.gameObject);
    }

    public void removeActiveTarget(Customer customer, int wrongPackage)
    {
        
        activeTargets.Remove(customer.gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        numCurrentPackages = packageSpawner.spawnedPackages.Count;

        while (activeTargets.Count < numSimultaneousTargets && activeTargets.Count < numCurrentPackages && unspawnedPackages.Count > 0)
        {
            int wantedIndex = Random.Range(0, unspawnedPackages.Count);
            PhysicalPackage wantedPackage = unspawnedPackages[wantedIndex];

            GameObject newTarget = Instantiate(targetPrefab);
            newTarget.transform.position = possibleSpawnLocations[Random.Range(0, possibleSpawnLocations.Length)].position;
            activeTargets.Add(newTarget);
            newTarget.GetComponent<Customer>().Initialize(this, wantedPackage.packageType);

            unspawnedPackages.RemoveAt(wantedIndex);
        }
    }
}
