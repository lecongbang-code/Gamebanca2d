using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentStatusBF : MonoBehaviour
{
    WeaponControll weaponControll;
    Animator anim;

    void Start()
    {
        weaponControll = FindObjectOfType<WeaponControll>();
        anim = GetComponent<Animator>();
    }

    public void CheckBoom()
    {
        anim.SetTrigger("IsBoom");
    }

    public void CheckFreeze()
    {
        anim.SetBool("Freeze", weaponControll.isFreeze);
    }
}
