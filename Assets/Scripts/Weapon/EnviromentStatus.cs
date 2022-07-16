using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentStatus : MonoBehaviour
{
    WeaponControll weaponControll;
    Animator anim;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        weaponControll = FindObjectOfType<WeaponControll>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    public void CheckStatus()
    {
        for (int i = 0; i < weaponControll.skills.Length; i++)
        {
            if(weaponControll.skills[i])
            {
                if(i == 0)
                {
                    spriteRenderer.color = new Color(255f, 255f, 255f, 255f);
                }
                else if(i == 1)
                {
                    spriteRenderer.color = new Color(0f, 196f, 255f, 255f);
                }
                else if(i == 2)
                {
                    spriteRenderer.color = new Color(152f, 0f, 255f, 255f);
                }
            }
            anim.SetBool("Skill"+(i+1), weaponControll.skills[i]);
        }
    }
}
