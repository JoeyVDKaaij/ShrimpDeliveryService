using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

[System.Serializable]
public struct Package
{
    public Material boxMaterial;
    public int packageType;
}

public class PhysicalPackage
{
    public int packageType;
    public GameObject gameObject;

    public PhysicalPackage(GameObject spawnedObject, Package type)
    {
        gameObject = spawnedObject;
        packageType = type.packageType;
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = type.boxMaterial;
        gameObject.GetComponent<BoxIdentifier>().packageType = type.packageType;
    }
}

public class PackageSpawner : MonoBehaviour
{

    [SerializeField] Package[] possiblePackages;
    public List<PhysicalPackage> spawnedPackages;

    [SerializeField] GameObject emptyPackage;

    [SerializeField] int numPackagesToSpawn;
    [SerializeField] float delayBetweenPackages;
    float timeSinceLastPackage;

    [SerializeField] TargetManager targetManager;
    
    bool isSpawning;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnedPackages = new List<PhysicalPackage>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawning)
        {
            timeSinceLastPackage += Time.deltaTime;

            if (timeSinceLastPackage > delayBetweenPackages && spawnedPackages.Count <= numPackagesToSpawn)
            {
                SpawnPackage();
                timeSinceLastPackage = 0;
            }
        }
    }

    void SpawnPackage()
    {
        PhysicalPackage newPackage = new PhysicalPackage(Instantiate(emptyPackage), possiblePackages[UnityEngine.Random.Range(0, possiblePackages.Length)]);
        spawnedPackages.Add(newPackage);
        newPackage.gameObject.transform.position = transform.position;
        targetManager.AddUnspawnedPackage(newPackage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LoadZone"))
        {
            isSpawning = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("LoadZone"))
        {
            isSpawning = false;
        }
    }
}
