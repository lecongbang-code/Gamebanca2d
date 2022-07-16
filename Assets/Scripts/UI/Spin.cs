using System.Collections;
using UnityEngine;
using TMPro;

public class Spin : MonoBehaviour 
{
	int randVal;
	float timeInterval;
	int finalAngle;
	int section;
	float totalAngle;
    [HideInInspector]
    public bool isCoroutine;

    public PickerWheel pickerWheel;

	HomeController homeController;
	TreasureDetail treasureDetail;

	public Transform reduce;
	GameObject textReduce;

	public Transform increase;
	GameObject textIncrease;

	public Transform parentCash;
	public Transform parentItem;
	public GameObject[] itemUI;

	AudioSource audioSource;
    public AudioClip clipSpin;
    public AudioClip clipThuong;

	void Awake () 
	{
		pickerWheel = FindObjectOfType<PickerWheel>();
		homeController = FindObjectOfType<HomeController>();
		treasureDetail = FindObjectOfType<TreasureDetail>();
		audioSource = GetComponent<AudioSource>();
		isCoroutine = true;
		section = pickerWheel.wheelPieces.Length;
		totalAngle = 360 / section;

		textReduce = reduce.GetChild(0).gameObject;
		textIncrease = increase.GetChild(0).gameObject;
	}

	void OnEnable()
    {
		audioSource.volume = homeController.playerData.Fx;
	}

	void TextReduce(int ruby)
	{
		GameObject Reduce = Instantiate(textReduce, reduce.transform);
		Reduce.SetActive(true);
		Reduce.GetComponent<TextMeshProUGUI>().text = "-" + ruby.ToString();
		Destroy(Reduce, 1f);
	}

	void TextIncrease(int cash)
	{
		GameObject Increase = Instantiate(textIncrease, increase.transform);
		Increase.SetActive(true);
		Increase.GetComponent<TextMeshProUGUI>().text = "+" + cash.ToString("N0");
		Destroy(Increase, 1f);
	}

	public void PlayAudio(string nameClip)
    {
        if(nameClip=="Spin")
        {
            audioSource.clip = clipSpin;
        }
        else if(nameClip=="Thuong")
        {
            audioSource.clip = clipThuong;
        }
        
        audioSource.Play();
    }

	public void ClickSpin(int x)
	{
		if (isCoroutine && homeController.playerData.Ruby >= x) 
		{
			PlayAudio("Spin");
			homeController.playerData.Ruby -= x;
			TextReduce(x);
			homeController.UpdateText();
			homeController.AddData();
			StartCoroutine(StartSpin(80, 100, x));
		}
		else if(!isCoroutine)
		{
			treasureDetail.MsgStatus("");
		}
		else
		{
			treasureDetail.MsgStatus("Thiếu Ngọc rồi!");
		}
	}

	IEnumerator StartSpin(int min, int max, int x)
	{
		isCoroutine = false;
		
		randVal = Random.Range (min, max);

		timeInterval = 0.1f * Time.deltaTime;

		for (int i = 0; i < randVal; i++) 
		{
			pickerWheel.transform.Rotate (0, 0, (totalAngle/2));

			if (i > Mathf.RoundToInt (randVal * 0.1f))
			{
				timeInterval = 0.2f * Time.deltaTime;
			}
            if (i > Mathf.RoundToInt (randVal * 0.2f))
			{
				timeInterval = 0.4f * Time.deltaTime;
			}
            if (i > Mathf.RoundToInt (randVal * 0.3f))
			{
				timeInterval = 0.6f * Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.4f))
			{
				timeInterval = 0.8f * Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.5f))
			{
				timeInterval = 1f * Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.6f))
			{
				timeInterval = 1.2f * Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.7f))
			{
				timeInterval = 1.4f * Time.deltaTime;
			}
            if (i > Mathf.RoundToInt (randVal * 0.8f))
			{
				timeInterval = 1.6f * Time.deltaTime;
			}
            if (i > Mathf.RoundToInt (randVal * 0.9f))
			{
				timeInterval = 1.8f * Time.deltaTime;
			}

			yield return new WaitForSeconds (timeInterval);
		}

		if (Mathf.RoundToInt (pickerWheel.transform.eulerAngles.z) % totalAngle != 0)
		{
			pickerWheel.transform.Rotate (0, 0, totalAngle/2);
		}
		finalAngle = Mathf.RoundToInt (pickerWheel.transform.eulerAngles.z);

		for (int i = 0; i < section; i++) 
		{
			if (finalAngle == i * totalAngle)
			{
                if(i==0)
                {
					if(Random.Range(0, 6) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(SpinMore(x));
					}
                }
				else if(i == 8)
				{
					if(Random.Range(0, 4) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(SpinMore(x));
					}
				}
				else if(i == 13)
				{
					if(Random.Range(0, 3) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(SpinMore(x));
					}
				}
				else if(i == 5)
				{
					if(Random.Range(0, 2) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(SpinMore(x));
					}
				}
				else if(i == 10)
				{
					if(Random.Range(0, 2) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(SpinMore(x));
					}
				}
                else
                {
                    AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    isCoroutine = true;
                }
			}
		}
	}

    IEnumerator SpinMore(int x)
	{
		randVal = 1;

		timeInterval = 0.1f * Time.deltaTime;

		for (int i = 0; i < randVal; i++) 
		{
			pickerWheel.transform.Rotate (0, 0, (totalAngle/2));

			if (i > Mathf.RoundToInt (randVal * 0.1f))
			{
				timeInterval = 0.2f * Time.deltaTime;
			}
            if (i > Mathf.RoundToInt (randVal * 0.2f))
			{
				timeInterval = 0.4f * Time.deltaTime;
			}
            if (i > Mathf.RoundToInt (randVal * 0.3f))
			{
				timeInterval = 0.6f * Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.4f))
			{
				timeInterval = 0.8f * Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.5f))
			{
				timeInterval = 1f * Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.6f))
			{
				timeInterval = 1.2f * Time.deltaTime;
			}
			if (i > Mathf.RoundToInt (randVal * 0.7f))
			{
				timeInterval = 1.4f * Time.deltaTime;
			}
            if (i > Mathf.RoundToInt (randVal * 0.8f))
			{
				timeInterval = 1.6f * Time.deltaTime;
			}
            if (i > Mathf.RoundToInt (randVal * 0.9f))
			{
				timeInterval = 1.8f * Time.deltaTime;
			}

			yield return new WaitForSeconds (timeInterval);
		}

		if (Mathf.RoundToInt (pickerWheel.transform.eulerAngles.z) % totalAngle != 0)
		{
			pickerWheel.transform.Rotate (0, 0, totalAngle/2);
		}
		finalAngle = Mathf.RoundToInt (pickerWheel.transform.eulerAngles.z);

		for (int i = 0; i < section; i++) 
		{
			if (finalAngle == i * totalAngle)
			{
                if(i==0)
                {
					if(Random.Range(0, 6) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(StartSpin(1, 1, x));
					}
                }
				else if(i == 8)
				{
					if(Random.Range(0, 4) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(StartSpin(1, 1, x));
					}
				}
				else if(i == 13)
				{
					if(Random.Range(0, 3) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(StartSpin(1, 1, x));
					}
				}
				else if(i == 5)
				{
					if(Random.Range(0, 2) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(StartSpin(1, 1, x));
					}
				}
				else if(i == 10)
				{
					if(Random.Range(0, 2) == 0)
					{
						AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    	isCoroutine = true;
					}
					else
					{
                    	StartCoroutine(StartSpin(1, 1, x));
					}
				}
                else
                {
					AddCash(pickerWheel.wheelPieces[i].Label, pickerWheel.wheelPieces[i].Amount, x);
                    isCoroutine = true;
                }
			}
		}
	}

	void AddCash(string label, int amount, int x)
	{
		PlayAudio("Thuong");
		if(x != 1)
			amount *= x;

		if(label == "Xu")
		{	
			if(amount == 10 || amount == 50 || amount == 100)
			{
				amount *= 1000;
			}
			homeController.playerData.Cash += amount;
			TextIncrease(amount);
			homeController.UpdateText();
			homeController.AddData();

			GameObject item = Instantiate(itemUI[5], transform.position, transform.rotation, parentCash);
			item.GetComponent<ItemMove>().Anim("Cash");
		}
		else if(label == "50%")
		{
			homeController.playerData.X1 += amount;
			treasureDetail.UpdateText();
			homeController.AddData();

			GameObject item = Instantiate(itemUI[0], transform.position, transform.rotation, parentItem);
			item.transform.GetChild(0).gameObject.SetActive(false);
			item.transform.GetChild(1).gameObject.SetActive(false);
			item.GetComponent<ItemMove>().Anim("X1");
		}
		else if(label == "100%")
		{
			homeController.playerData.X2 += amount;
			treasureDetail.UpdateText();
			homeController.AddData();

			GameObject item = Instantiate(itemUI[1], transform.position, transform.rotation, parentItem);
			item.transform.GetChild(0).gameObject.SetActive(false);
			item.transform.GetChild(1).gameObject.SetActive(false);
			item.GetComponent<ItemMove>().Anim("X2");
		}
		else if(label == "200%")
		{
			homeController.playerData.X3 += amount;
			treasureDetail.UpdateText();
			homeController.AddData();

			GameObject item = Instantiate(itemUI[2], transform.position, transform.rotation, parentItem);
			item.transform.GetChild(0).gameObject.SetActive(false);
			item.transform.GetChild(1).gameObject.SetActive(false);
			item.GetComponent<ItemMove>().Anim("X3");
		}
		else if(label == "Boom")
		{
			homeController.playerData.Boom += amount;
			treasureDetail.UpdateText();
			homeController.AddData();

			GameObject item = Instantiate(itemUI[3], transform.position, transform.rotation, parentItem);
			item.transform.GetChild(0).gameObject.SetActive(false);
			item.transform.GetChild(1).gameObject.SetActive(false);
			item.GetComponent<ItemMove>().Anim("Boom");
		}
		else if(label == "Freeze")
		{
			homeController.playerData.Freeze += amount;
			treasureDetail.UpdateText();
			homeController.AddData();

			GameObject item = Instantiate(itemUI[4], transform.position, transform.rotation, parentItem);
			item.transform.GetChild(0).gameObject.SetActive(false);
			item.transform.GetChild(1).gameObject.SetActive(false);
			item.GetComponent<ItemMove>().Anim("Freeze");
		}
		else
		{
			Debug.Log("Error");
		}
	}
}