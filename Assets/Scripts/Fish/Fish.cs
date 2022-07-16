using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public string nameFish;
    [HideInInspector]
    public bool isFreeze = false;
    [HideInInspector]
    public float timeFreeze = 0;

    public float minSpeed;
    public float maxSpeed;
    float moveSpeed;
    float curentMoveSpeed;
    public int bonusX;
    float bonus;

    int oneHit;
    int hit;
    Rigidbody2D rb;
    Vector2 movement;
    float newScale;
    bool life = true;
    float size;
    bool changeMove;
    float timechangeMove;

    Player player;
    GameController game;
    FishControll fishControll;

    public GameObject textCash;
    public GameObject textExp;

    public GameObject attack;
    Animator animAttack;
    Animator animator;

    [HideInInspector]
    public GameObject targetGoto;
    [HideInInspector]
    public int randomPoint;

    public bool fishBig = false;

    public GameObject parCash;

    AudioSource audioSource;
    public AudioClip clipDie;

    public bool map;

    void Start()
    {
        moveSpeed = Random.Range(minSpeed, maxSpeed);
        newScale = Random.Range(0.5f, 1f);
        transform.localScale = new Vector3(newScale, newScale, 1);
        size = transform.localScale.x;
        curentMoveSpeed = moveSpeed;
        bonus = Random.Range(bonusX/2, bonusX * 3);

        player = FindObjectOfType<Player>();
        game = FindObjectOfType<GameController>();
        fishControll = FindObjectOfType<FishControll>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        animAttack = attack.gameObject.GetComponent<Animator>();
        gameObject.AddComponent<AudioSource>();
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = player.playerData.Fx;
    }
    
    void Update()
    {
        if(isFreeze)
        {
            curentMoveSpeed = 0;
            timeFreeze -= Time.deltaTime;
            if(timeFreeze <= 0)
            {
                curentMoveSpeed = moveSpeed;
                isFreeze = false;
            }
        }

        if(changeMove && !map)
        {
            timechangeMove += Time.deltaTime;
            if(timechangeMove >= Random.Range(8,12))
            {
                changeMove = false;
                timechangeMove = 0;
                isDie = false;
                ChangeMove();
            }
        }

        if(life)
        {
            if(hit >= Mathf.RoundToInt(bonus * 0.5f))
            {
                moveSpeed = Random.Range(minSpeed, maxSpeed);
                curentMoveSpeed = moveSpeed;
                newScale = Random.Range(size, 1.1f);
                transform.localScale = new Vector3(newScale, newScale, 1);
                life = false;
            }
        }

        if(isDead)
        {
            transform.position = Vector3.Lerp(transform.position, player.weaponControll.transform.position, 0.04f);

            if(Vector2.Distance(transform.position, player.weaponControll.transform.position) < 1)
            {
                Destroy(gameObject);
            }
        }
    }

    void FixedUpdate()
    {
        if(!isDie  && !map && targetGoto != null)
        {
            Vector3 direction = targetGoto.transform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
            direction.Normalize();
            movement = direction;
            MoveTarget(movement);
        }
    }

    void MoveTarget(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * curentMoveSpeed * Time.deltaTime));

        if(Vector2.Distance(transform.position, targetGoto.transform.position) <= 0.1)
        {
            changeMove = true;
            isDie = true;
        }
    }

    void ChangeMove()
    {
        if(Vector2.Distance(transform.position, targetGoto.transform.position) <= 0.1)
        {
            fishControll.CurrentFishDie(nameFish);
            Destroy(gameObject);
        }
    }

    void GetAttack(int _LvWeapon)
    {
        int _cash = _LvWeapon * bonusX;
        int _exp = _cash;
        if(game.itemSkills[0])
        {
            _cash += Mathf.RoundToInt(_cash * 0.5f);
        }
        else if(game.itemSkills[1])
        {
            _cash += Mathf.RoundToInt(_cash * 1f);
        }
        else if(game.itemSkills[2])
        {
            _cash += Mathf.RoundToInt(_cash * 2f);
        }
        if(!isDie)
        {
            isDie = true;
            if(nameFish == "Ca35x")
            {
                player.weaponControll.WeaponSkill(2);
            }
            else if(nameFish == "Ca40x")
            {
                player.weaponControll.WeaponSkill(1);
            }
            else if(nameFish == "Ca50x")
            {
                player.weaponControll.WeaponSkill(0);
            }
            StartCoroutine(Dead(_exp, _cash));
            if(clipDie)
            {
                audioSource.clip=clipDie;
                audioSource.Play();
            }
            else
            {
                Debug.LogWarning("Chưa có âm thanh này");
            }
            player.SliderSkill(_cash);
        } 
    }

    public void Attacked(int lvWeapon)
    {
        if(!isDie)
        {
            oneHit = Random.Range(0, bonusX * 2);
            if(oneHit == 0)
            {
                animAttack.SetTrigger("IsAttacked");
                GetAttack(lvWeapon);
            }
            else
            {
                hit++;
                animAttack.SetTrigger("IsAttacked");
                if(hit >= bonus && !fishBig)
                {
                    GetAttack(lvWeapon);
                }
            }
        }
    }

    public void CallDead(int boom)
    {
        if(!isDie)
        {
            isDie = true;
            StartCoroutine(Dead(boom, boom));
        } 
    }

    bool isDead = false;
    bool isDie = false;
    IEnumerator Dead(int exp, int cash)
    {
        GameObject ParCash = Instantiate(parCash, transform.position, Quaternion.identity);
        Destroy(ParCash, 1f);
        Destroy(gameObject.GetComponent<BoxCollider2D>());
        GameObject _textExp = Instantiate(textExp, transform.position, Quaternion.identity);
        _textExp.GetComponent<TextMesh>().text = "+" + exp;
        GameObject _textCash = Instantiate(textCash, transform.position, Quaternion.identity);
        _textCash.GetComponent<TextMesh>().text = "+" + cash;
        player.playerData.Cash += cash;
        game.AddExp(exp);
        player.SetValueCash();
        animator.SetBool("IsDead", true);
        if(!map)
            fishControll.CurrentFishDie(nameFish);
        CheckFishMission(nameFish);
        yield return new WaitForSeconds(0.7f);
        isDead = true;
        Destroy(gameObject, 3f);
    }

    void CheckFishMission(string NameFish)
    {
        if(NameFish == "Tienca")
            player.playerData.MissCurrent0++;
        else if(NameFish == "Camap")
            player.playerData.MissCurrent1++;
        else if(NameFish == "Cavoi")
            player.playerData.MissCurrent2++;
        else if(NameFish == "Cavoixanh")
            player.playerData.MissCurrent3++;

        player.playerData.MissCurrent4++;
        player.AddData();
    }
}