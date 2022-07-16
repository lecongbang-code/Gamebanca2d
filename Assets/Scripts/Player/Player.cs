using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    public TextCounter NumberCounter;
    public TextCounter textLevelWeapon;
    public TextMeshProUGUI textLevelPlayer;

    public GameObject viewWeapon;

    public PlayerData playerData{get; private set;}

    float timeShoot = 0f;
    float timeAddCash = 0f;
    float timeResetShoot = 0.23f;

    public WeaponControll weaponControll;

    public TextMesh textSkill;
    public GameObject[] blookItem;
    public Text[] textItem;
    public TextMeshProUGUI[] textTotalItem;

    public GameObject blookBoom;
    public Text textBlookBoom;
    public TextMeshProUGUI textTotalBoom;

    public GameObject blookFreeze;
    public Text textBlookFreeze;
    public TextMeshProUGUI textTotalFreeze;

    public TextMesh textBulletSkill;

    public Slider sliderPower;
    public Button btnBlookPower;
    public Button btnPower;

    public Transform parentRubyUp;
    public GameObject textRubyUp;

    int fps;

    void Awake()
    {
        playerData = PlayerPersistence.LoadData();
    }

    private void Start()
    {
        fps = playerData.Fps == 1 ? 60:30;
        Application.targetFrameRate = fps;

        weaponControll = FindObjectOfType<WeaponControll>();
        SetValueCash();
        SetValueLevel();
        SetValueTotalItem();
        SetValueTotalBoom();
        SetValueTotalFreeze();
        SliderSkill(0);
    }

    public void SetValueTotalItem()
    {
        textTotalItem[0].text = playerData.X1.ToString();
        textTotalItem[1].text = playerData.X2.ToString();
        textTotalItem[2].text = playerData.X3.ToString();
    }

    public void SetValueTotalBoom()
    {   
        textTotalBoom.text = playerData.Boom.ToString();
    }

    public void SetValueTotalFreeze()
    {
        textTotalFreeze.text = playerData.Freeze.ToString();
    }

    public void SetValueLevel()
    {
        textLevelWeapon.Value = weaponControll.weaponSelect.level;
    }

    public void SetValueCash()
    {
        NumberCounter.Value = playerData.Cash;
    }

    public void SetValueLevelPlayer(int level)
    {
        textLevelPlayer.text = level.ToString();
    }

    public void SetValueBulletSkill(int skill2)
    {
        textBulletSkill.gameObject.SetActive(true);
        textBulletSkill.text = skill2.ToString();
    }

    void Update()
    {
        if(timeShoot <= 1f)
        {
            timeShoot+=Time.deltaTime;
        }

        if(weaponControll.skills[2])
        {
            timeResetShoot = 0.2f;
        }
        else
        {
            timeResetShoot = 0.23f;
        }
        // AutoAddCash();
    }

    public void SliderSkill(int _cash)
    {   
        sliderPower.maxValue = 100 + playerData.Level;
        if(playerData.Power >= 100 + playerData.Level)
        {
            btnPower.gameObject.SetActive(true);
            btnBlookPower.gameObject.SetActive(false);
        }
        else
        {
            playerData.Power++;
            playerData.CashPower += _cash;
            btnPower.gameObject.SetActive(false);
            btnBlookPower.gameObject.SetActive(true);
            AddData();
        }
        sliderPower.value = playerData.Power;
    } 

    int CheckCashLv(int cashLv)
    {
        if(cashLv >= 1000000)
        {
            // 1500 2000 3000 5000 10000
            cashLv = 2000;
        }
        else if(cashLv >= 500000)
        {
            // 1000 1500 2000 3000 5000
            cashLv = 1500;
        }
        else if(cashLv >= 300000)
        {
            // 500 1000 1500 2000 3000
            cashLv = 1000;
        }
        else if(cashLv >= 200000)
        {
            // 250 500 1000 1500 2000
            cashLv = 500;
        }
        else if(cashLv >= 100000)
        {
            // 60 120 250 500 1000
            cashLv = 250;
        }
        else if(cashLv >= 50000)
        {
            // 30 60 120 250 500
            cashLv = 120;
        }
        else if(cashLv >= 10000)
        {
            // 8 15 30 60 120
            cashLv = 30;
        }
        else
        {
            // 1 4 8 15 30
            cashLv = 8;
        }
        return cashLv;
    }

    public void ButtonPower()
    {
        playerData.Ruby ++;
        playerData.Power = 0;
        btnPower.gameObject.SetActive(false);
        btnBlookPower.gameObject.SetActive(true);
        sliderPower.value = 0;
        weaponControll.currentLevel = CheckCashLv(playerData.CashPower);
        playerData.CashPower = 0;
        weaponControll.WeaponSkill(2);
        SetValueLevel();
        AddData();
        InsTextRubyUp(1);
    }

    public void InsTextRubyUp(int rubyValue)
    {
        GameObject TextRuby = Instantiate(textRubyUp, parentRubyUp);
        TextRuby.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+"+rubyValue.ToString();
        Destroy(TextRuby, 2f);
    }

    void AutoAddCash()
    {
        if(timeAddCash <= 1f && timeShoot >= 1f)
        {
            timeAddCash += Time.deltaTime;
            if(timeAddCash >= 0.8f)
            {
                AddCash(1);
                timeAddCash = 0;
            }
        }
    }

    int _up;
    public void CallShoot()
    {
        weaponControll.LookAtPointerDown();

        if(timeShoot >= timeResetShoot && playerData.Cash >= weaponControll.weaponSelect.level)
        {
            weaponControll.Shoot();
            playerData.Cash -= weaponControll.weaponSelect.level;
            AddData();
            SetValueCash();
            timeShoot = 0;
            viewWeapon.SetActive(false);
        }
        else if (timeShoot >= timeResetShoot && playerData.Cash < weaponControll.weaponSelect.level && playerData.Cash >= 1)
        {
            weaponControll.up--;
            int level = weaponControll.up;
            weaponControll.CheckChangeWeapon(level);
        }
    }

    public void AddCash(int Cash)
    {
        playerData.Cash += Cash;
        SetValueCash();
        AddData();
    }

    public void AddData()
    {
        PlayerPersistence.SaveData(this);
    }

    public bool isSelect;
    public void WeaponSelect()
    {
        if(!isSelect && !weaponControll.CheckStatusSkill())
        {
            isSelect = true;
        }
        else
        {
            isSelect = false;
        }
        viewWeapon.SetActive(isSelect);
    }
}