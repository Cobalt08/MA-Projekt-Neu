using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Launch projectile
/// </summary>
public class WeaponScript2 : MonoBehaviour
{

    public Transform shotPrefab;

    public float shootingRate = 0.5f;

    private float shootCooldown;

    private Vector2 dir;

    //private Transform randomObject;

    private double deadZone = 0.2;

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

    public void Attack()
    {
        if (CanAttack)
        {
            shootCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position;

            //var horiz = Input.GetAxis("Horiz");
            //var vert = Input.GetAxis("Vert");

            Joystick weaponjoystick = GetComponent<Joystick>();
            float horiz = weaponjoystick.Horizontal;
            float vert = weaponjoystick.Vertical;

            Vector2 tmp = new Vector2(horiz, vert);
            if (tmp.sqrMagnitude > deadZone)
            {
                dir = tmp.normalized;
            }
            shotTransform.position = dir;

            ProjectileMoveScript move = shotTransform.gameObject.GetComponent<ProjectileMoveScript>();
            Player player = GetComponent<Player>();
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
