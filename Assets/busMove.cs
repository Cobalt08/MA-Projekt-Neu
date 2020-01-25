using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busMove : MonoBehaviour
{

    
    public Switch[] switches;

    private bool allActive = false;
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animation>();
        allActive = true;
        foreach (Switch sw in switches)
        {
            if (!sw.isActive)
            {
                allActive = false;
                
            }
        }

        if (allActive)
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
        foreach(AnimationState state in anim)
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
