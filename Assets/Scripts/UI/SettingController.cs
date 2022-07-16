using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingController : MonoBehaviour
{
    public Slider sliderVolume;
    public Slider sliderFx;
    public Toggle toggleFps;

    HomeController homeController;

    void Start()
    {   
        homeController = FindObjectOfType<HomeController>();

        sliderVolume.value = homeController.playerData.Volume;
        sliderFx.value = homeController.playerData.Fx;
        toggleFps.isOn = homeController.playerData.Fps == 1? true:false;
    }

    public void ValueChangeVolume()
	{
        float Volume = sliderVolume.value;
        homeController.playerData.Volume = Volume;
        homeController.AddData();
	}

    public void ValueChangeFx()
	{   
        float Fx = sliderFx.value;
		homeController.playerData.Fx = Fx;
        homeController.audioSource.volume = Fx;
        homeController.AddData();
	}

    public void ValueChangeFps()
	{
        int fps = toggleFps.isOn?1:0;
		homeController.playerData.Fps = fps;
        homeController.AddData();
        homeController.ChangeFps();
	}
}
