using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class HomeController : MonoBehaviour
{   
    public TextMeshProUGUI textLimitAds;

    public TextMeshProUGUI textLevel;
    public NumberCounter textCash;
    public NumberCounter textRuby;
    int fps;
    public PlayerData playerData{get; private set;}

    public Transform increaseCash;
	GameObject textIncreaseCash;

	public Transform increaseRuby;
	GameObject textIncreaseRuby;

    public GameObject adPanel;

    public Transform parentPanel;
    public GameObject homeMsg;
    GameObject _HomeMsg;

    [HideInInspector]
    public AudioSource audioSource;
    public AudioClip clickBtn;
    public AudioClip clipCash;

    public GameObject tapStartGame;

    void Awake()
    {
        playerData = PlayerPersistence.LoadData();
        audioSource = GetComponent<AudioSource>();
        textIncreaseCash = increaseCash.GetChild(0).gameObject;
		textIncreaseRuby = increaseRuby.GetChild(0).gameObject;
        audioSource.volume = playerData.Fx;
    }

    void Start()
    {   
        Time.timeScale = 1;
        ChangeFps();
        CheckForRewards();
        UpdateText();

        if(playerData.ClickStart == 1)
        {
            tapStartGame.SetActive(false);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
        }
    }

    public void UpdateText()
    {
        textLevel.text = playerData.Level.ToString();
        textCash.Value = playerData.Cash;
        textRuby.Value = playerData.Ruby;
        textLimitAds.text = "X" + playerData.LimitAds;
    }

    public void BtnClick()
    {
        audioSource.clip = clickBtn;
        audioSource.Play();
    }

    public void PlayAudio(string nameClip)
    {
        if(nameClip=="AddCash")
        {
            audioSource.clip = clipCash;
        }
        
        audioSource.Play();
    }

    public void HomeMsg(string msg)
    {
        _HomeMsg = Instantiate(homeMsg, parentPanel);
        _HomeMsg.SetActive(true);
        _HomeMsg.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = msg;
    }

    void CheckForRewards()
    {
        DateTime currentDatetime = DateTime.Now;
        DateTime rewardDatetime = DateTime.Parse(playerData.TimeOneDay);
        double elapsedSeconds = (currentDatetime - rewardDatetime).TotalSeconds;
        if(elapsedSeconds >= 65000f)
        {
            playerData.LimitAds = 5;
            playerData.TimeOneDay = DateTime.Now.ToString();
            AddData();
        }
    }

    public void DestroyMsg()
    {
        Destroy(_HomeMsg);
    }

    void TextIncreaseCash(int cash)
	{
		GameObject g = Instantiate(textIncreaseCash, increaseCash.transform);
		g.SetActive(true);
		g.GetComponent<TextMeshProUGUI>().text = "+" + cash.ToString();
		Destroy(g, 1f);
	}

    void TextIncreaseRuby(int ruby)
	{
		GameObject g = Instantiate(textIncreaseRuby, increaseRuby.transform);
		g.SetActive(true);
		g.GetComponent<TextMeshProUGUI>().text = "+" + ruby.ToString();
		Destroy(g, 1f);
	}

    public void ChangeFps()
    {
        fps = playerData.Fps == 1 ? 60:30;
        Application.targetFrameRate = fps;
    }

    public void AddData()
    {
        PlayerPersistence.SaveDataHome(this);
    }

    public void ClickCash()
    {
        isAdsCash = true;
    }

    public void ClickRuby()
    {
        isAdsCash = false;
    }

    bool isAdsCash;
    public void AdsAddCash()
    {
        PlayAudio("AddCash");
        adPanel.SetActive(false);
        if(isAdsCash)
        {
            playerData.Cash += 5000;
            TextIncreaseCash(5000);
        }
        else
        {
            playerData.Ruby += 3;
            TextIncreaseRuby(3);
        }
        playerData.LimitAds--;
        UpdateText();
        AddData();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void ResetData()
    {   
        playerData.Id = 0;
        playerData.Name = "Bang";
        playerData.Level = 1;
        playerData.Exp = 0;
        playerData.Cash = 10000;
        playerData.Ruby = 1;

        playerData.X1 = 1;
        playerData.X2 = 0;
        playerData.X3 = 0;
        playerData.Boom = 1;
        playerData.Freeze = 0;

        playerData.Power = 100;
        playerData.CashPower = 0;

        playerData.Volume = 0;
        playerData.Fx = 1;
        playerData.Fps = 0;

        playerData.MissCurrent0 = 0;
        playerData.MissCurrent1 = 0;
        playerData.MissCurrent2 = 0;
        playerData.MissCurrent3 = 0;
        playerData.MissCurrent4 = 0;
        playerData.MissCurrent5 = 0;
        playerData.MissCurrent6 = 0;
        playerData.MissCurrent7 = 0;

        playerData.Miss0 = 3;
        playerData.Miss1 = 3;
        playerData.Miss2 = 3;
        playerData.Miss3 = 3;
        playerData.Miss4 = 10;
        playerData.Miss5 = 3;
        playerData.Miss6 = 3;
        playerData.Miss7 = 10;

        playerData.LimitAds = 5;
        playerData.TimeOneDay = DateTime.Now.ToString();

        playerData.ClickStart = 1;

        AddData();
        LoadLevel(0);
    }

    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progrssText;

    public void LoadLevel (int sceneIndex)
    {
        FindObjectOfType<AdsRewarded>().OnDestro();
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
}
