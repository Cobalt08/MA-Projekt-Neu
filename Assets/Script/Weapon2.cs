using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class Weapon2 : MonoBehaviour
{

    public Transform shotPrefab;

    public float shootingRate = 0.5f;

    private float shootCooldown;

    void Start()
    {
        shootCooldown = 0f;
    }

    void Update()
    {
        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(float horiz, float vert)
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position;

            ProjectileMoveScript move = shotTransform.gameObject.GetComponent<ProjectileMoveScript>();
            move.direction = new Vector2(horiz, vert);
        }
    }

    /// <summary>
    /// Is the weapon ready to create a new projectile?
    /// </summary>
    public bool CanAttack
    {
        get
        {
            return shootCooldown <= 0f;
        }
    }
}
