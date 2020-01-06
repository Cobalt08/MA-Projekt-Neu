using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownDisplay : MonoBehaviour
{
    public Image imageCooldown;
    public float cooldownBig = 5;
    public float cooldownSmall = 2;
    bool isCooldownBig;
    bool isCooldownSmall;
    private AttackButton attackB;
    private Weapon2 weapon2;

    void Start()
    {
        attackB = FindObjectOfType<AttackButton>();
        weapon2 = FindObjectOfType<Weapon2>();
    }
    void Update()
    {
        if (attackB.Pressed)
        {
            isCooldownBig = true;
        }

        if (isCooldownBig)
        {
            imageCooldown.fillAmount += 1 / cooldownBig * Time.deltaTime;

            if(imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldownBig = false;
            }
        }

        if (!weapon2.CanAttack)
        {
            isCooldownSmall = true;
        }

        if (isCooldownSmall)
        {
            imageCooldown.fillAmount += 1 / cooldownSmall * Time.deltaTime;

            if (imageCooldown.fillAmount >= 1)
            {
                imageCooldown.fillAmount = 0;
                isCooldownSmall = false;
            }
        }
    }
}
