using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class MissionControll : MonoBehaviour
{
    [Serializable]
    public struct MissionGame
    {
        public Sprite icon;
        public string derscription;
        public int ruby;
    }

    [SerializeField] MissionGame[] missionGame = null;

    public GameObject content;

    HomeController homeController;
    AudioSource audioSource;

    public Transform increase;
	GameObject textIncrease;

    public AudioClip clipClaim;

    void Awake()
    {
        homeController = FindObjectOfType<HomeController>();
        audioSource = GetComponent<AudioSource>();
        MissionGamePlay();
        textIncrease = increase.GetChild(0).gameObject;
    }

    void OnEnable()
    {
        audioSource.volume = homeController.playerData.Fx;
    }

    int tienDo = 9;
    int mucTieu = 10;

    int CheckTienDo(int i)
    {
        if(i == 0)
            tienDo = homeController.playerData.MissCurrent0;
        else if(i == 1)
            tienDo = homeController.playerData.MissCurrent1;
        else if(i == 2)
            tienDo = homeController.playerData.MissCurrent2;
        else if(i == 3)
            tienDo = homeController.playerData.MissCurrent3;
        else if(i == 4)
            tienDo = homeController.playerData.MissCurrent4;
        else if(i == 5)
            tienDo = homeController.playerData.MissCurrent5;
        else if(i == 6)
            tienDo = homeController.playerData.MissCurrent6;
        else if(i == 7)
            tienDo = homeController.playerData.Level;
        return tienDo;
    }

    int CheckMucTieu(int i)
    {
        if(i == 0)
            mucTieu = homeController.playerData.Miss0;
        else if(i == 1)
            mucTieu = homeController.playerData.Miss1;
        else if(i == 2)
            mucTieu = homeController.playerData.Miss2;
        else if(i == 3)
            mucTieu = homeController.playerData.Miss3;
        else if(i == 4)
            mucTieu = homeController.playerData.Miss4;
        else if(i == 5)
            mucTieu = homeController.playerData.Miss5;
        else if(i == 6)
            mucTieu = homeController.playerData.Miss6;
        else if(i == 7)
            mucTieu = homeController.playerData.Miss7;
        return mucTieu;
    }

    void CheckMucTieuSave(int i)
    {
        if(i == 0)
        {
            homeController.playerData.Miss0 += Mathf.RoundToInt(homeController.playerData.Miss0  * 1.2f);
            mucTieu = homeController.playerData.Miss0;
        }
        else if(i == 1)
        {
            homeController.playerData.Miss1 += Mathf.RoundToInt(homeController.playerData.Miss1  * 1.2f);
            mucTieu = homeController.playerData.Miss1;
        }
        else if(i == 2)
        {
            homeController.playerData.Miss2 += Mathf.RoundToInt(homeController.playerData.Miss2  * 1.2f);
            mucTieu = homeController.playerData.Miss2;
        }
        else if(i == 3)
        {
            homeController.playerData.Miss3 += Mathf.RoundToInt(homeController.playerData.Miss3  * 1.2f);
            mucTieu = homeController.playerData.Miss3;
        }
        else if(i == 4)
        {
            homeController.playerData.Miss4 += Mathf.RoundToInt(homeController.playerData.Miss4  * 1.35f);
            mucTieu = homeController.playerData.Miss4;
        }
        else if(i == 5)
        {
            homeController.playerData.Miss5 += Mathf.RoundToInt(homeController.playerData.Miss5  * 1.2f);
            mucTieu = homeController.playerData.Miss5;
        }
        else if(i == 6)
        {
            homeController.playerData.Miss6 += Mathf.RoundToInt(homeController.playerData.Miss6  * 1.2f);
            mucTieu = homeController.playerData.Miss6;
        }
        else if(i == 7)
        {
            homeController.playerData.Miss7 += 10;
            mucTieu = homeController.playerData.Miss7;
        }
    }

    void MissionGamePlay()
    {
        GameObject item = content.transform.GetChild(0).gameObject;
        GameObject g;
        for(int i = 0; i < missionGame.Length; i++)
        {
            CheckTienDo(i);
            CheckMucTieu(i);

            g = Instantiate(item, content.transform);
            g.transform.GetChild(0).GetComponent<Image>().sprite = missionGame[i].icon;
            g.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = missionGame[i].derscription;
            g.transform.GetChild(2).GetComponent<Slider>().maxValue = mucTieu;
            g.transform.GetChild(2).GetComponent<Slider>().value = tienDo;
            g.transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text = tienDo + "/" + mucTieu;

            if(tienDo >= mucTieu)
            {
                g.transform.GetChild(3).gameObject.SetActive(true);
                g.transform.GetChild(4).gameObject.SetActive(false);

                Button btnClaim = g.transform.GetChild(3).GetComponent<Button>();
                btnClaim.interactable = true;
                btnClaim.AddEventListener(i, ItemClicked);
            }
            else
            {
                g.transform.GetChild(3).gameObject.SetActive(false);
                g.transform.GetChild(4).gameObject.SetActive(true);
            }

            g.transform.GetChild(3).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + missionGame[i].ruby.ToString();
            g.transform.GetChild(4).GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + missionGame[i].ruby.ToString();
        }
        Destroy(item);
    }

    void ItemClicked(int itemIndex)
    {
        audioSource.clip=clipClaim;
        audioSource.Play();
        CheckTienDo(itemIndex);
        CheckMucTieuSave(itemIndex);
        homeController.playerData.Ruby += missionGame[itemIndex].ruby;
        TextIncrease(missionGame[itemIndex].ruby);
        homeController.UpdateText();
        homeController.AddData();

        if(tienDo < mucTieu)
        {
            content.transform.GetChild(itemIndex).GetChild(3).gameObject.SetActive(false);
            content.transform.GetChild(itemIndex).GetChild(4).gameObject.SetActive(true);
        }
        content.transform.GetChild(itemIndex).GetChild(2).GetComponent<Slider>().maxValue = mucTieu;
        content.transform.GetChild(itemIndex).GetChild(2).GetComponent<Slider>().value = tienDo;
        content.transform.GetChild(itemIndex).GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text = tienDo + "/" + mucTieu;
    }

    void TextIncrease(int ruby)
	{
		GameObject g = Instantiate(textIncrease, increase.transform);
		g.SetActive(true);
		g.GetComponent<TextMeshProUGUI>().text = "+" + ruby.ToString();
		Destroy(g, 1f);
	}
}
