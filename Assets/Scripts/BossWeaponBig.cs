using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boss launches a big projectile
/// </summary>
public class BossWeaponBig : MonoBehaviour
{

    public Transform shotPrefab;
    //public float shootingRate = 6f;
    public Vector2 direction = new Vector2(-1, 0);

    //private float shootCooldown;

    /*
    void Start()
    {
        shootCooldown = 4.5f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        } else
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

        shotTransform.position += new Vector3(-7, -4.5f, 0);

        var tempVector = shotTransform.position;
        tempVector.z = -2;
        shotTransform.position = tempVector;

        move.direction = this.direction;
    }
}
