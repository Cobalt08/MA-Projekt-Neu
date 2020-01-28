using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnAllActive : MonoBehaviour
{
    public GameObject explosion;

    public Switch[] switches;
    private bool allActive = false;

    // Update is called once per frame
    void Update()
    {
        allActive = true;
        foreach(Switch sw in switches)
        {
            if (!sw.isActive)
            {
                allActive = false;
            }
        }

        if (allActive)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
