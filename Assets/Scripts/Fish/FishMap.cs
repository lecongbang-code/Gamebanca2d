using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMap : MonoBehaviour
{
    public Transform parentMap;
    public Transform targetMap;
    public GameObject[] LvMap;

    GameObject[] pointMap;

    FishControll fishControll;

    public bool isMap = false;
    public float move = 0.0015f;
    float moveCurrent;

    WeaponControll weaponControll;

    void Start()
    {
        moveCurrent = move;
        fishControll = FindObjectOfType<FishControll>();
        weaponControll = FindObjectOfType<WeaponControll>();
    }

    GameObject _Map;

    void Update()
    {
        if(weaponControll.isFreeze)
        {
            move = 0f;
        }
        else
        {
            move = moveCurrent;    
        }

        if(_Map)
        {
            _Map.transform.position = Vector3.Lerp(_Map.transform.position, targetMap.position, move);

            if(Vector2.Distance(_Map.transform.position, targetMap.position) < 10)
            {
                DestroyMap();
                isMap = false;
            }
        }
    }

    public void InstantiateFish()
    {
        isMap = true;
        int a = Random.Range(0,4);

        for (int i = 0; i < LvMap.Length; i++)
        {
            if(a == i)
            {
                _Map = Instantiate(LvMap[i], parentMap.position, parentMap.rotation, parentMap);
                _Map.SetActive(true);
                pointMap = GameObject.FindGameObjectsWithTag("Pointmaplv1");
                CallIns(i);
            }
        }
    }

    public void DestroyMap()
    {
        Destroy(_Map);
        pointMap = null;
    }

    void CallIns(int i)
    {
        if(i == 0) CreateFishMapLv1();
        else if (i == 1) CreateFishMapLv2();
        else if (i == 2) CreateFishMapLv3();
        else if (i == 3) CreateFishMapLv4();
    }

    void CreateFishMapLv1()
    {
        for (int i = 0; i < pointMap.Length; i++)
        {
            GameObject Fish = Instantiate(fishControll.fishs[Random.Range(0,fishControll.fishs.Length)].fishPrefabs, pointMap[i].transform.position, pointMap[i].transform.rotation, _Map.transform);
            Fish.GetComponent<Fish>().map = true;
        }
    }

    void CreateFishMapLv2()
    {
        int a = Random.Range(7, fishControll.fishs.Length);
        for (int i = 0; i < pointMap.Length; i++)
        {
            GameObject Fish = Instantiate(fishControll.fishs[a].fishPrefabs, pointMap[i].transform.position, pointMap[i].transform.rotation, _Map.transform);
            Fish.GetComponent<Fish>().map = true;
        }
    }

    void CreateFishMapLv3()
    {
        int a = Random.Range(7, fishControll.fishs.Length);
        int b = Random.Range(7, fishControll.fishs.Length);
        int c = Random.Range(7, fishControll.fishs.Length);

        for (int i = 0; i < pointMap.Length/4; i++)
        {
            GameObject Fish = Instantiate(fishControll.fishs[a].fishPrefabs, pointMap[i].transform.position, pointMap[i].transform.rotation, _Map.transform);
            Fish.GetComponent<Fish>().map = true;
        }

        for (int i = pointMap.Length/4; i < pointMap.Length/2; i++)
        {
            GameObject Fish = Instantiate(fishControll.fishs[b].fishPrefabs, pointMap[i].transform.position, pointMap[i].transform.rotation, _Map.transform);
            Fish.GetComponent<Fish>().map = true;
        }

        for (int i = pointMap.Length/2; i < pointMap.Length; i++)
        {
            GameObject Fish = Instantiate(fishControll.fishs[c].fishPrefabs, pointMap[i].transform.position, pointMap[i].transform.rotation, _Map.transform);
            Fish.GetComponent<Fish>().map = true;
        }
    }

    void CreateFishMapLv4()
    {
        int a = Random.Range(7, fishControll.fishs.Length);
        for (int i = 0; i < 3; i++)
        {
            GameObject Fish = Instantiate(fishControll.fishs[Random.Range(0,7)].fishPrefabs, pointMap[i].transform.position, pointMap[i].transform.rotation, _Map.transform);
            Fish.GetComponent<Fish>().map = true;
        }

        for (int i = 3; i < pointMap.Length; i++)
        {
            GameObject Fish = Instantiate(fishControll.fishs[a].fishPrefabs, pointMap[i].transform.position, pointMap[i].transform.rotation, _Map.transform);
            Fish.GetComponent<Fish>().map = true;
        }
    }
}