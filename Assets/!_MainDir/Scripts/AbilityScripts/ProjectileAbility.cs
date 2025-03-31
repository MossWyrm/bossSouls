using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Ability_Projectile", menuName = "Abilities/Projectile")]
public class ProjectileAbility : Ability
{
    public GameObject abilityPrefab;
    public bool keepUpright;
    public int speed;
    public float lifeTime;
    public int instanceBank;
    private Vector3 _destination;
    [FormerlySerializedAs("_spawnedObjects")] [ReadOnly] public List<GameObject> spawnedObjects = new List<GameObject>();
    

    public override void CleanUp()
    {
        foreach (var t in spawnedObjects)
        {
            Destroy(t.gameObject);
        }
        spawnedObjects.Clear();
    }

    public override void Prepare(GameObject toParent)
    {
        base.Prepare(toParent);
        for (var i = 0; i < instanceBank; i++)
        {
            InstantiateNewProjectile();
        }
    }
    
    public override void ActivateAbility()
    {
        base.ActivateAbility();
        UseProjectile();
    }

    private GameObject InstantiateNewProjectile()
    {
        var projectileObj = Instantiate(abilityPrefab, parentObject.transform.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<SpawnedAbility>()?.Initialize(lifeTime);
        projectileObj.SetActive(false);
        spawnedObjects.Add(projectileObj);
        return projectileObj;
    }
    private void UseProjectile()
    {
        GameObject projectileObj = spawnedObjects.FirstOrDefault(t => !t.activeInHierarchy);
        if (projectileObj == null)
        {
            projectileObj = InstantiateNewProjectile();
        }
        projectileObj.SetActive(true);
        projectileObj.GetComponent<SpawnedAbility>()?.Activate();
        projectileObj.transform.position = parentObject.transform.position;
        Ray ray = new Ray(parentObject.transform.position, parentObject.transform.forward);
        _destination = ray.GetPoint(1000);
        RotateToDestination(projectileObj, _destination, keepUpright);
        projectileObj.GetComponent<Rigidbody>().linearVelocity = parentObject.transform.forward * speed;
        Debug.DrawRay(parentObject.transform.position,parentObject.transform.forward*100, Color.red,3);
    }

    //TODO 
    private void RotateToDestination(GameObject obj, Vector3 destination, bool uprightOnly)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if (uprightOnly)
        {
            rotation.x = 0;
            rotation.y = 0;
        }

        // obj.transform.rotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);
        obj.transform.LookAt(obj.transform.position + direction);
    }
}