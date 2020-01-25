using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class elevatorScript : MonoBehaviour
{

    public Switch[] switches;

    private bool anyActive = false;
    private Animation anim;
    // Start is called before the first frame update
    void Update()
    {
        anim = GetComponent<Animation>();
        anyActive = false;
        foreach (Switch sw in switches)
        {
            if (sw.isActive)
            {
                anyActive = true;

            }
        }

        if (anyActive)
        {
            this.startAnimation();
        }
        else
        {
            this.stopAnimation();
        }
    }

    void startAnimation()
    {
        foreach (AnimationState state in anim)
        {
            state.speed = 1F;
        }
    }

    void stopAnimation()
    {
        foreach (AnimationState state in anim)
        {
            state.speed = 0F;
        }

    }
    
}
