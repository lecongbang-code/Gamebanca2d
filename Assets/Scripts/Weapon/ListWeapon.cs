using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ListWeapon : MonoBehaviour
{
    public GameObject preItemWeapon;
    GameObject[] itemWeapon;

    WeaponControll weaponControll;
    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        weaponControll = FindObjectOfType<WeaponControll>();
        itemWeapon = new GameObject[5];
    }

    int nextUp;
    int currentUp;
    void OnEnable()
    {
        int nextList = 0;
        weaponControll.SetWeapon();
        nextUp = weaponControll.nextUp;
        if(currentUp == 0)
        {
            currentUp=nextUp;
        }
        for(int i = nextUp; i < nextUp+5; i++)
        {   
            if(i == currentUp)
            {
                continue;
            }
            else 
            {
                CreateWeapon(nextList, i);
                nextList++;
            }
        }
    }

    Button btnSelect;
    void CreateWeapon(int NextList, int i)
    {   
        itemWeapon[NextList] = Instantiate(preItemWeapon, transform);
        btnSelect = itemWeapon[NextList].transform.GetComponent<Button>();
        btnSelect.AddEventListener (i, ItemBtnClicked);
        itemWeapon[NextList].transform.GetChild(0).GetComponent<Image>().sprite = weaponControll.weapons[i].image;
        itemWeapon[NextList].transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = weaponControll.weapons[i].level.ToString();
    }

    public void CleanList()
    {
        for (int i = 0; i < itemWeapon.Length; i++)
        {
            if(itemWeapon[i])
            {
                Destroy(itemWeapon[i]);
            }
        }
    }

    void OnDisable()
    {
        CleanList();
    }

    void ItemBtnClicked (int itemIndex)
	{   
        currentUp = itemIndex;
        weaponControll.up = currentUp;
        weaponControll.WeaponSelect(weaponControll.Level[itemIndex]);
        gameObject.SetActive(false);
	}
}