using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControll : MonoBehaviour
{
    Rigidbody2D rb2D;
    Vector3 lastVelocity;

    public float destroyTime = 1f;
    public float moveSpeed;
    public GameObject netSprite;
    WeaponControll weaponControll;

    public int lvWeapon;

    [HideInInspector]
    public bool s1, s2;
    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        weaponControll = FindObjectOfType<WeaponControll>();
    }

    void Start()
    {
        Destroy(gameObject, destroyTime);
        if(s1)
        {
            transform.Rotate(0.0f, 0.0f, 20f, Space.World);
        }
        if(s2)
        {
            transform.Rotate(0.0f, 0.0f, -20f, Space.World);
        }

        rb2D.AddForce(transform.up * moveSpeed);
        rb2D.AddForce(transform.up * moveSpeed, ForceMode2D.Impulse);

        if(lvWeapon == 0)
        {
            lvWeapon = weaponControll.currentLevel;
        }
    }

    void Update()
    {
        lastVelocity = rb2D.velocity;
    }

    int collision = 0;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Fish")
        {
            col.gameObject.GetComponent<Fish>().Attacked(lvWeapon);
            Instantiate(netSprite, transform.position, transform.rotation);
            Destroy(gameObject);
        }
 
        if(col.gameObject.CompareTag("Enviroment"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, col.transform.up);
            rb2D.velocity = direction * Mathf.Max(speed, 0f);

            collision++;
            if(collision >= 4)
            {
                Destroy(gameObject);
            }
        }

        if(col.gameObject.CompareTag("EnviromentRight"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, col.transform.right);
            rb2D.velocity = direction * Mathf.Max(speed, 0f);

            collision++;
            if(collision >= 4)
            {
                Destroy(gameObject);
            }
        }
    }
}
