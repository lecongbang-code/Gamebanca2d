using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetControll : MonoBehaviour
{
    public bool netPro = false;
    public float destroyTime = 1f;
    public Vector3 Offset = new Vector3(0, 0.5f, 0);
    public Vector3 RandomizeIntensity = new Vector3(0.5f, 0, 0);

    WeaponControll weaponControll;
    [HideInInspector]
    public int lvWeapon;

    float timeAttack = 2f;

    void Awake()
    {
        weaponControll = FindObjectOfType<WeaponControll>();
    }

    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += Offset;
        transform.localPosition += new Vector3(Random.Range(-RandomizeIntensity.x, RandomizeIntensity.x), 
        Random.Range(-RandomizeIntensity.y, RandomizeIntensity.y),
        Random.Range(-RandomizeIntensity.z, RandomizeIntensity.z));
        lvWeapon = weaponControll.currentLevel;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Fish" && netPro)
        {
            timeAttack += Time.deltaTime;
            if(timeAttack > 0.5f)
            {
                col.gameObject.GetComponent<Fish>().Attacked(lvWeapon);
                timeAttack = 0f;
            }
        }
    }
}
