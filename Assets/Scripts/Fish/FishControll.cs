using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Fishs
{
    public string name;
    public GameObject fishPrefabs;
    public int totalFish;
    public int currentFish = 0;
}

public class FishControll : MonoBehaviour
{
    public bool isLvMap;
    public Fishs[] fishs;
    Player player;
    GameObject[] targetL;
    GameObject[] targetR;
    float timeCheckCurrentFish = 5;
    FishMap fishMap;

    float timePlay;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        fishMap = FindObjectOfType<FishMap>();
        targetL = GameObject.FindGameObjectsWithTag("TargetL");
        targetR = GameObject.FindGameObjectsWithTag("TargetR");
    }

    void Update()
    {
        if(!fishMap.isMap)
        {
            timePlay += Time.deltaTime;
            if(timePlay >= 300)
            {
                fishMap.InstantiateFish();
                timePlay = 0;
            }
        
            if(!player.weaponControll.isFreeze && timePlay < 290)
            {
                timeCheckCurrentFish += Time.deltaTime;
                if(timeCheckCurrentFish >= 5f)
                {
                    CheckCurrentFish();
                    timeCheckCurrentFish = 0f;
                }
            }
        }
    }

    void CheckCurrentFish()
    {
        int a = Random.Range(0,4);
        if(fishs[a].currentFish < fishs[a].totalFish)
        {
            CreateFishBig(fishs[a].fishPrefabs);
            fishs[a].currentFish++;
        }
        
        for (int i = 4; i < fishs.Length; i++)
        {
            if(fishs[i].currentFish < fishs[i].totalFish)
            {
                CreateFishBig(fishs[i].fishPrefabs);
                fishs[i].currentFish++;
            }
        }
    }

    public void CurrentFishDie(string nameFish)
    {
        if(!isLvMap)
        {
            foreach(Fishs ca in fishs)
            {
                if(nameFish == ca.name)
                {
                    ca.currentFish--;
                }
            }
        }   
    }
    
    public void CreateFishBig(GameObject fishBig)
    {
        int randomPoint = Random.Range(1,3);
        if(randomPoint == 1)
        {
            int randomTarget = Random.Range(0,targetL.Length);
            GameObject Fish = Instantiate(fishBig, targetL[randomTarget].transform.position, targetL[randomTarget].transform.rotation, transform);
            Fish.gameObject.GetComponent<Fish>().targetGoto = targetR[Random.Range(0,11)];
            Fish.gameObject.GetComponent<Fish>().randomPoint = randomPoint;
        }
        else if(randomPoint == 2)
        {
            int randomTarget = Random.Range(0,targetR.Length);
            GameObject Fish = Instantiate(fishBig, targetR[randomTarget].transform.position, targetR[randomTarget].transform.rotation, transform);
            Fish.gameObject.GetComponent<Fish>().targetGoto = targetL[Random.Range(0,11)];
            Fish.gameObject.GetComponent<Fish>().randomPoint = randomPoint;
            Quaternion target = Quaternion.Euler(-180, 180, 0);
            Fish.gameObject.transform.GetChild(0).transform.rotation = Quaternion.Slerp(transform.rotation, target, 100);
        }
    }
}
