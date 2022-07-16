using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControll : MonoBehaviour
{
    public Weapon[] weapons;
    public Weapon[] weaponSkills;

    int Sk;
    public bool[] skills;
    public float timeSkills;

    public Weapon weaponSelect;
    public GameObject crosshairs;

    public Camera Camera;
    public GameObject gun;
    public Transform pointShoot;
    public ParticleSystem parShoot;
    public GameObject effectSprite;
    
    Vector3 target;
    Animator animator;

    public int[] Level;
    [HideInInspector]
    public int up;
    [HideInInspector]
    public int nextUp;

    [HideInInspector]
    public int currentLevel;

    Player player;

    int skill2=5;

    [HideInInspector]
    public bool isFreeze = false;
    float timeFreeze;

    bool isBoom;
    float timeBlookBoom = 11f;

    EnviromentStatus enviromentStatus;
    EnviromentStatusBF enviromentStatusBF;

    SpriteRenderer spriteRenderer;
    GameController gameController;

    void Start()
    {
        player = FindObjectOfType<Player>();
        gameController = FindObjectOfType<GameController>();
        enviromentStatus = FindObjectOfType<EnviromentStatus>();
        enviromentStatusBF = FindObjectOfType<EnviromentStatusBF>();
        animator = GetComponentInChildren<Animator>();

        spriteRenderer = FindObjectOfType<SpriteRenderer>();

        SetWeapon();
        up = nextUp;
        WeaponSelect(Level[up]);
    }

    void Update()
    {
        OnSkill(Sk);

        if(skill2 <= 0)
        {
            skills[1] = false;
            skill2 = 5;
            WeaponSelect(Level[up]);
            player.SetValueBulletSkill(skill2);
            player.textBulletSkill.gameObject.SetActive(false);
            enviromentStatus.CheckStatus();
        }

        if(isFreeze)
        {
            timeFreeze -= Time.deltaTime;
            player.textBlookFreeze.text = timeFreeze.ToString().Substring(0,timeFreeze < 10? 1:2 ) + "s";
            if(timeFreeze <= 0)
            {
                player.blookFreeze.SetActive(false);
                isFreeze = false;
                enviromentStatus.CheckStatus();
                enviromentStatusBF.CheckFreeze();
            }
        }

        if(isBoom)
        {
            timeBlookBoom -= Time.deltaTime;
            player.blookBoom.SetActive(true);
            player.textBlookBoom.text = timeBlookBoom.ToString().Substring(0,timeBlookBoom < 10? 1:2 ) + "s";
            if(timeBlookBoom <= 0)
            {
                isBoom = false;
                timeBlookBoom = 11f;
                player.blookBoom.SetActive(false);
            }
            enviromentStatus.CheckStatus();
        }

        OnEffectSkill();
    }

    void OnEffectSkill()
    {
        if(skills[0])
        {
            effectSprite.SetActive(true);
            effectSprite.GetComponent<SpriteRenderer>().color =  new Color(255f, 255f, 255f, 255f);
            spriteRenderer.color =  new Color(0.4f, 0.5f, 1f, 1f);
        }
        else if(skills[1])
        {
            effectSprite.SetActive(true);
            effectSprite.GetComponent<SpriteRenderer>().color =  new Color(0f, 196f, 255f, 255f);
            spriteRenderer.color =  new Color(0f, 196f, 255f, 255f);
        }
        else if(skills[2])
        {
            effectSprite.SetActive(true);
            effectSprite.GetComponent<SpriteRenderer>().color =  new Color(152f, 0f, 255f, 255f);
            spriteRenderer.color =  new Color(152f, 0f, 255f, 255f);
        }
        else if(gameController.CheckStatusItemSkill() && !skills[0] && !skills[1] && !skills[2])
        {
            effectSprite.SetActive(true);
            effectSprite.GetComponent<SpriteRenderer>().color =  new Color(1f, 0.9f, 0.4f, 1f);
            spriteRenderer.color =  new Color(0.977f, 1f, 0.59f, 1f);
        }
        else
        {
            effectSprite.SetActive(false);
            if(spriteRenderer)
                spriteRenderer.color =  new Color(255f, 255f, 255f, 255f);
            else
            {
                spriteRenderer.color =  new Color(255f, 255f, 255f, 255f);
            }
        }
    }

    void OnSkill(int sk)
    {
        if(skills[sk])
        {
            player.textSkill.gameObject.SetActive(true);
            player.textSkill.text = "00:" + timeSkills.ToString().Substring(0,timeSkills < 10? 1:2 ) + "s";
            timeSkills -= Time.deltaTime;
            if(timeSkills <= 0)
            {
                skills[sk] = false;
                WeaponSelect(Level[up]);
                enviromentStatus.CheckStatus();
            }
        }
        else
        {
            player.textSkill.text = "00:" + 0 + "s";
            player.textSkill.gameObject.SetActive(false);
        }
    }

    public void CheckChangeWeapon(int Up)
    {   
        up = Up;
        WeaponSelect(Level[up]);
    }

    void PointShoot()
    {
        target = Camera.transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        GameObject _crosshairs = Instantiate(crosshairs);
        _crosshairs.transform.position = new Vector2(target.x, target.y);
    }

    public void LookAtPointerDown() 
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, -angle)); 
    }

    public void Shoot()
    {
        gameController.AudioPlay("Shoot");
        if(skills[1])
        {
            skill2 --;
            player.SetValueBulletSkill(skill2);
        }

        if(skills[2])
        {
            CreateBulletSkill();
        }
        else
        {
            CreateBullet();
        }
    }

    public void CreateBullet()
    {
        Instantiate(parShoot, pointShoot.position, pointShoot.rotation);
        PointShoot();
        animator.SetTrigger("IsShoot");
        GameObject bullet = Instantiate(weaponSelect.bulletSprite, pointShoot.position, transform.rotation);
        bullet.GetComponent<BulletControll>().lvWeapon =  weaponSelect.level;
    }

    public void CreateBulletSkill()
    {
        Instantiate(parShoot, pointShoot.position, pointShoot.rotation);
        PointShoot();
        animator.SetTrigger("IsShoot");
        GameObject bullet = Instantiate(weaponSelect.bulletSprite, pointShoot.position, transform.rotation);
        bullet.GetComponent<BulletControll>().lvWeapon =  weaponSelect.level;

        GameObject bullets1 = Instantiate(weaponSelect.bulletSprite, pointShoot.position, transform.rotation);
        bullets1.GetComponent<BulletControll>().s1 =  true;
        bullets1.GetComponent<BulletControll>().lvWeapon =  weaponSelect.level;

        GameObject bullets2 = Instantiate(weaponSelect.bulletSprite, pointShoot.position, transform.rotation);
        bullets2.GetComponent<BulletControll>().s2 =  true;
        bullets2.GetComponent<BulletControll>().lvWeapon =  weaponSelect.level;
    }
  
    public void WeaponSelect(int level)
    {   
        animator.SetTrigger("OnGun");
        player.isSelect = false;
        foreach(Weapon weapon in weapons)
        {
            if(weapon.level == level)
            {
                weaponSelect = new Weapon();
                weaponSelect.level = weapon.level;
                currentLevel = weaponSelect.level;
                weaponSelect.name = weapon.name;
                weaponSelect.image = weapon.image;
                weaponSelect.bulletSprite = weapon.bulletSprite;
                gun.GetComponent<SpriteRenderer>().sprite = weaponSelect.image;
                player.SetValueLevel();
            }
        }
        gameController.AudioPlay("Ongun");
    }

    public void WeaponSkill(int level)
    {   
        for(int i=0; i<skills.Length; i++)
        {
            timeSkills = 30f;
            skills[i] = false;
        }

        animator.SetTrigger("OnGun");
        
        if(weaponSkills[level] != null)
        {
            if(level == 1)
            {
                skill2 = 5;
                player.SetValueBulletSkill(skill2);
            }
            else
            {
                player.textBulletSkill.gameObject.SetActive(false);
            }
            
            Sk = level;
            skills[Sk] = true;
            weaponSelect = new Weapon();
            weaponSelect.level = level==0?0:currentLevel;
            weaponSelect.name = weaponSkills[Sk].name;
            weaponSelect.image = weaponSkills[Sk].image;
            weaponSelect.bulletSprite = weaponSkills[Sk].bulletSprite;
            gun.GetComponent<SpriteRenderer>().sprite = weaponSelect.image;

            enviromentStatus.CheckStatus();
        }
    }

    public bool CheckStatusSkill()
    {
        bool isCheck = false;
        for(int i=0; i<skills.Length; i++)
        {
            if(skills[i])
            {
                isCheck = true;
            }
        }
        return isCheck;
    }

    public void SetWeapon()
    {
        if(player.playerData.Cash >= 1000000)
        {
            // 1500 2000 3000 5000 10000
            nextUp = 10;
        }
        else if(player.playerData.Cash >= 500000)
        {
            // 1000 1500 2000 3000 5000
            nextUp = 9;
        }
        else if(player.playerData.Cash >= 300000)
        {
            // 500 1000 1500 2000 3000
            nextUp = 8;
        }
        else if(player.playerData.Cash >= 200000)
        {
            // 250 500 1000 1500 2000
            nextUp = 7;
        }
        else if(player.playerData.Cash >= 100000)
        {
            // 60 120 250 500 1000
            nextUp = 5;
        }
        else if(player.playerData.Cash >= 50000)
        {
            // 30 60 120 250 500
            nextUp = 4;
        }
        else if(player.playerData.Cash >= 10000)
        {
            // 8 15 30 60 120
            nextUp = 2;
        }
        else
        {
            // 1 4 8 15 30
            nextUp = 0;
        }
    }
    
    int cashTotal;
    GameObject[] allFish;
    public void OnBoom()
    {
        if(!isBoom && player.playerData.Boom > 0)
        {
            gameController.AudioPlay("Boom");
            enviromentStatusBF.CheckBoom();
            isBoom = true;
            cashTotal = 0;
            allFish = GameObject.FindGameObjectsWithTag("Fish");
            isFreeze = false;
            enviromentStatusBF.CheckFreeze();
            player.blookFreeze.SetActive(false);

            if(allFish.Length != 0)
            {
                foreach(var F in allFish)
                {
                    if(F.GetComponent<Fish>())
                    {
                        int Cash = F.GetComponent<Fish>().bonusX;
                        F.GetComponent<Fish>().CallDead(Cash * allFish.Length);
                        cashTotal += Cash * allFish.Length;
                    }
                }
            }
            player.playerData.MissCurrent5++;
            player.playerData.Boom--;
            player.AddData();
            player.SetValueTotalBoom();

            enviromentStatus.CheckStatus();
            print(cashTotal);
        }
    }

    public void OnFreeze()
    {
        if(!isFreeze && player.playerData.Freeze > 0)
        {
            allFish = GameObject.FindGameObjectsWithTag("Fish");
            timeFreeze = 30f;
            isFreeze = true;
            player.blookFreeze.SetActive(true);
            if(allFish.Length != 0)
            {
                foreach(var F in allFish)
                {
                    if(!F.GetComponent<Fish>().isFreeze)
                    {
                        F.GetComponent<Fish>().isFreeze = isFreeze;
                        F.GetComponent<Fish>().timeFreeze = timeFreeze;
                    }
                }
            }
            enviromentStatusBF.CheckFreeze();

            player.playerData.MissCurrent6++;
            player.playerData.Freeze--;
            player.AddData();
            player.SetValueTotalFreeze();
            enviromentStatus.CheckStatus();
        }   
    }
}