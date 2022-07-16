using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TreasureDetail : MonoBehaviour
{
    public TextMeshProUGUI[] item;
    public TextMeshProUGUI boom;
    public TextMeshProUGUI freeze;

    public GameObject msg;
    HomeController homeController;
    Spin spin;

    void Start()
    {
        homeController = FindObjectOfType<HomeController>();
        spin = FindObjectOfType<Spin>();
        UpdateText();
    }

    void Update()
    {
       
    }

    GameObject Msg;
    public void MsgStatus(string text)
    {
        Destroy(Msg);
        Msg = Instantiate(msg, transform);
        if(text != "")
            Msg.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        Destroy(Msg, 1f);
    }

    public void UpdateText()
    {
        item[0].text = homeController.playerData.X1.ToString();
        item[1].text = homeController.playerData.X2.ToString();
        item[2].text = homeController.playerData.X3.ToString();
        boom.text = homeController.playerData.Boom.ToString();
        freeze.text = homeController.playerData.Freeze.ToString();
    }

    public void CloseGameObject()
    {
        if(spin.isCoroutine)
        {
            gameObject.SetActive(false);
        }
        else
        MsgStatus("");
    }
}
