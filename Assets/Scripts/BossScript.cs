using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the lives of the boss
/// </summary>
public class BossScript : MonoBehaviour
{

    public int lives = 3;
    public float roaringRate = 6f;
    public float shootingRate = 3f;
    public AudioClip roar;

    private float roarCooldown;
    private AudioSource source;
    private float shootCooldown;
    private BossWeaponBig weapon1;
    private BossWeaponSmall weapon2;
    private bool weaponSwitch;

    void Start()
    {
        source = GetComponent<AudioSource>();
        weapon1 = GetComponent<BossWeaponBig>();
        weapon2 = GetComponent<BossWeaponSmall>();
        weaponSwitch = true;
        roarCooldown = 1f;
    }

    void Update()
    {
        if (roarCooldown > 0)
        {
            roarCooldown -= Time.deltaTime;
        }
        else
        {
            //Roar();
        }

        if (shootCooldown > 0)
        {
            shootCooldown -= Time.deltaTime;
        }
        else
        {
            shootCooldown = shootingRate;

            if (weaponSwitch)
            {
                weapon1.Attack();
                weaponSwitch = false;
            }
            else
            {
                weapon2.Attack();
                weaponSwitch = true;
            }
        }
    }

    void Roar()
    {
        roarCooldown = roaringRate;
        source.PlayOneShot(roar);
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        ProjectileMoveScript shot = otherCollider.gameObject.GetComponent<ProjectileMoveScript>();
        if (shot != null)
        {
            if (this.lives < 1)
            {
                Rigidbody2D rigidbodyComponent = GetComponent<Rigidbody2D>();
                rigidbodyComponent.constraints = RigidbodyConstraints2D.None;
                Destroy(this.gameObject, 3);
            }
            else
            {
                this.lives -= 1;
            }
        }
    }
}
