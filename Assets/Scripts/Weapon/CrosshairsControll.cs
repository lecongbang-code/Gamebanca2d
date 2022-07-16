using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairsControll : MonoBehaviour
{
    public float timeDestroy;
    
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }
}
