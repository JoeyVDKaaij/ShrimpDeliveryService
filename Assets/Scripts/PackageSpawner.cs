using UnityEngine;
using System.Collections.Generic;
using System;

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
        gameObject.GetComponent<MeshRenderer>().material = type.boxMaterial;
    }
}

public class PackageSpawner : MonoBehaviour
{

    [SerializeField] Package[] possiblePackages;
    List<PhysicalPackage> spawnedPackages;

    [SerializeField] GameObject emptyPackage;

    [SerializeField] int numPackagesToSpawn;
    [SerializeField] float delayBetweenPackages;
    float timeSinceLastPackage;
    



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnedPackages = new List<PhysicalPackage>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastPackage += Time.deltaTime;

        if (timeSinceLastPackage > delayBetweenPackages && spawnedPackages.Count <= numPackagesToSpawn) { 
            SpawnPackage();
            timeSinceLastPackage = 0;
        }
    }

    void SpawnPackage()
    {
        spawnedPackages.Add(new PhysicalPackage(Instantiate(emptyPackage), possiblePackages[UnityEngine.Random.Range(0, possiblePackages.Length)]));
        spawnedPackages[spawnedPackages.Count - 1].gameObject.transform.position = transform.position;
    }
}
