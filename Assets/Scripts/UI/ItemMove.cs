using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public Animator animator;

    public void Anim(string animName)
    {
        Destroy(gameObject, 1.2f);
        if(animator)
            animator.SetTrigger(animName);
        else
            print("Animator null");
    }
}
