using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Destroys object after a set time
/// </summary>
public class BossProjectileScript : MonoBehaviour
{
    public int lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(GetComponent<ProjectileMoveScript>());
        Destroy(gameObject, 1);

    }
}
