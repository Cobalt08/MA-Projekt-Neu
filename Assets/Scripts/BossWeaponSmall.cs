using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boss launches a samll projectile
/// </summary>
public class BossWeaponSmall : MonoBehaviour
{
    public Transform shotPrefab;
    //public float shootingRate = 6f;

    private float shootCooldown;
    private Transform target1;
    private Transform target2;
    private bool targetSwitch;
    private Vector3 targetVector;

    void Start()
    {
        //shootCooldown = 1.5f;
        target1 = GameObject.Find("Arc").transform;
        target2 = GameObject.Find("Tic").transform;
    }

    /*
    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        else
        {
            Attack();
        }
    }
    */

    public void Attack()
    {
        //shootCooldown = shootingRate;

        var shotTransform = Instantiate(shotPrefab) as Transform;

        shotTransform.position = transform.position;

        BossProjectileMoveScript move = shotTransform.gameObject.GetComponent<BossProjectileMoveScript>();

        shotTransform.position += new Vector3(-7, 2.5f, 0);

        var tempVector = shotTransform.position;
        tempVector.z = -2;
        shotTransform.position = tempVector;

        if (targetSwitch)
        {
            targetVector = target1.position - transform.position;
            move.direction = targetVector;
            targetSwitch = false;
        }
        else
        {
            targetVector = target2.position - transform.position;
            move.direction = targetVector;
            targetSwitch = true;
        }
    }
}
