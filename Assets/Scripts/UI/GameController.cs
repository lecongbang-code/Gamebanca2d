using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int level;
    public int maxLevel = 100;

    public int currentExp;
    public int maxExp;

    public bool[] itemSkills;
    public float timeItemSkills;

    public int[] nextLevelExp;

    int isk;

    public Transform ParentLevelUp;
    public GameObject PrefabsLevelUp;

    Player player;

    AudioSource audioSource;
    public AudioClip clickBtn;
    public AudioClip Ongun;
    public AudioClip clipShoot;
    public AudioClip clipBoom;

    void Awake()
    {
        player = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        Time.timeScale = 1;
        level = player.playerData.Level;
        currentExp = player.playerData.Exp;
        player.SetValueLevelPlayer(level);

        nextLevelExp = new int[maxLevel + 1];
        nextLevelExp[1] = 100;
        for (int i = 2; i < maxLevel; i++)
        {
            nextLevelExp[i] = Mathf.RoundToInt(nextLevelExp[i-1] * 1.1f);
        }

        audioSource.volume = player.playerData.Fx;
    }

    void Update()
    {
        CheckExp();
        OnItemSkill(isk);
    }

    void OnItemSkill(int Isk)
    {
        if(itemSkills[Isk])
        {
            player.blookItem[Isk].SetActive(true);
            player.textItem[Isk].text = timeItemSkills.ToString().Substring(0,timeItemSkills < 10? 1:2 ) + "s";
            timeItemSkills -= Time.deltaTime;
            if(timeItemSkills <= 0)
            {
                itemSkills[Isk] = false;
            }
        }
        else
        {
            player.blookItem[Isk].SetActive(false);
        }
    }

    public void ItemSkillSelect(int _isk)
    {
        if(!CheckStatusItemSkill() && !player.weaponControll.CheckStatusSkill())
        {
            if(_isk == 0)
            {
                if(player.playerData.X1 > 0)
                {
                    player.playerData.X1 -=1;
                    Item(_isk);
                }
            }
            else if(_isk == 1)
            {
                if(player.playerData.X2 > 0)
                {
                    player.playerData.X2 -=1;
                    Item(_isk);
                }
            }
            else if(_isk == 2)
            {
                if(player.playerData.X3 > 0)
                {
                    player.playerData.X3 -=1;
                    Item(_isk);
                }
            } 
        }
    }

    public void BtnClick()
    {
        audioSource.clip = clickBtn;
        audioSource.Play();
    }

    public void AudioPlay(string nameClip)
    {
        if(nameClip == "Ongun")
        {
            audioSource.clip = Ongun;
        }
        else if(nameClip == "Shoot")
        {
            audioSource.clip = clipShoot;
        }
        else if(nameClip == "Boom")
        {
            audioSource.clip = clipBoom;
        }
        audioSource.Play();
    }

    void Item(int _isk)
    {
        player.AddData();
        player.SetValueTotalItem();

        for(int i=0; i<itemSkills.Length; i++)
        {
            timeItemSkills = 46f;
            itemSkills[i] = false;
        }
        isk = _isk;
        itemSkills[isk] = true;
    }

    public bool CheckStatusItemSkill()
    {
        bool isCheck = false;
        for(int i=0; i<itemSkills.Length; i++)
        {
            if(itemSkills[i])
            {
                isCheck = true;
            }
        }
        return isCheck;
    }

    public void AddExp(int _exp)
    {
        currentExp += _exp;
        player.playerData.Exp = currentExp;
        player.AddData();
    }

    void CheckExp()
    {
        maxExp = nextLevelExp[level];
        if(currentExp >= maxExp && level < maxLevel)
        {
            LevelUp();
        }
        else if(level >= maxLevel)
        {
            currentExp = 0;
            player.playerData.Exp = currentExp;
            player.AddData();
        }
    }

    void LevelUp()
    {
        currentExp -= nextLevelExp[level];
        player.playerData.Exp = currentExp;
        level++;
        player.playerData.Level = level;
        player.SetValueLevelPlayer(level);
        player.AddData();
        InsTextLevelUp(level);
    }

    GameObject TextLevel;
    void InsTextLevelUp(int _Level)
    {
        Destroy(TextLevel);
        TextLevel = Instantiate(PrefabsLevelUp, ParentLevelUp);
        TextLevel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Lên Cấp: " + _Level;
        Destroy(TextLevel, 2f);
    }

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progrssText;
    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        int sub;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        loadingScreen.SetActive(true);
        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            float a = progress * 100f;
            if(a >= 100)sub=3;
            else if(a>10 && a<100)sub=2;
            else sub=1;
            progrssText.text = a.ToString().Substring(0, sub) + "%";
            yield return null;
        }
    }

    public void ClickCash()
    {
        isAdsCash = true;
    }

    public void ClickRuby()
    {
        isAdsCash = false;
    }

    public GameObject adPanel;
    bool isAdsCash;
    public void AdsAddCash()
    {
        adPanel.SetActive(false);
        if(isAdsCash)
        {
            player.playerData.Cash += 5000;
        }
        else
        {
            player.playerData.Ruby += 3;
            player.InsTextRubyUp(3);
        }
        player.playerData.LimitAds--;
        player.SetValueCash();
        player.AddData();
    }

    public void PauseGame()
    {
        // tắt âm thanh game
        // Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        // bật âm thanh game
        // Time.timeScale = 1;
    }

    public Transform parentPanel;
    public GameObject homeMsg;
    GameObject _HomeMsg;

    public void HomeMsg(string msg)
    {
        _HomeMsg = Instantiate(homeMsg, parentPanel);
        _HomeMsg.SetActive(true);
        _HomeMsg.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
    }

    public void DestroyMsg()
    {
        Destroy(_HomeMsg);
    }
}
