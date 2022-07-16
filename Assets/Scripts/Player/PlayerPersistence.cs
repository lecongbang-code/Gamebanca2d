using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPersistence : MonoBehaviour
{
    public static void SaveData(Player player)
    {
        PlayerPrefs.SetInt("Cash", player.playerData.Cash);
        PlayerPrefs.SetInt("Ruby", player.playerData.Ruby);
        PlayerPrefs.SetInt("Level", player.playerData.Level);
        PlayerPrefs.SetInt("Exp", player.playerData.Exp);

        PlayerPrefs.SetInt("X1", player.playerData.X1);
        PlayerPrefs.SetInt("X2", player.playerData.X2);
        PlayerPrefs.SetInt("X3", player.playerData.X3);
        PlayerPrefs.SetInt("Boom", player.playerData.Boom);
        PlayerPrefs.SetInt("Freeze", player.playerData.Freeze);

        PlayerPrefs.SetInt("Power", player.playerData.Power);
        PlayerPrefs.SetInt("CashPower", player.playerData.CashPower);

        PlayerPrefs.SetInt("MissCurrent0", player.playerData.MissCurrent0);
        PlayerPrefs.SetInt("MissCurrent1", player.playerData.MissCurrent1);
        PlayerPrefs.SetInt("MissCurrent2", player.playerData.MissCurrent2);
        PlayerPrefs.SetInt("MissCurrent3", player.playerData.MissCurrent3);
        PlayerPrefs.SetInt("MissCurrent4", player.playerData.MissCurrent4);
        PlayerPrefs.SetInt("MissCurrent5", player.playerData.MissCurrent5);
        PlayerPrefs.SetInt("MissCurrent6", player.playerData.MissCurrent6);
        PlayerPrefs.SetInt("MissCurrent7", player.playerData.MissCurrent7);
    }

    public static void SaveDataHome(HomeController player)
    {
        PlayerPrefs.SetInt("Id", 1);
        PlayerPrefs.SetString("Name", "Bang");
        PlayerPrefs.SetInt("Cash", player.playerData.Cash);
        PlayerPrefs.SetInt("Ruby", player.playerData.Ruby);
        PlayerPrefs.SetInt("Level", player.playerData.Level);
        PlayerPrefs.SetInt("Exp", player.playerData.Exp);

        PlayerPrefs.SetInt("X1", player.playerData.X1);
        PlayerPrefs.SetInt("X2", player.playerData.X2);
        PlayerPrefs.SetInt("X3", player.playerData.X3);
        PlayerPrefs.SetInt("Boom", player.playerData.Boom);
        PlayerPrefs.SetInt("Freeze", player.playerData.Freeze);

        PlayerPrefs.SetInt("Power", player.playerData.Power);
        PlayerPrefs.SetInt("CashPower", player.playerData.CashPower);

        PlayerPrefs.SetFloat("Volume", player.playerData.Volume);
        PlayerPrefs.SetFloat("Fx", player.playerData.Fx);
        PlayerPrefs.SetInt("Fps", player.playerData.Fps);

        PlayerPrefs.SetInt("MissCurrent0", player.playerData.MissCurrent0);
        PlayerPrefs.SetInt("MissCurrent1", player.playerData.MissCurrent1);
        PlayerPrefs.SetInt("MissCurrent2", player.playerData.MissCurrent2);
        PlayerPrefs.SetInt("MissCurrent3", player.playerData.MissCurrent3);
        PlayerPrefs.SetInt("MissCurrent4", player.playerData.MissCurrent4);
        PlayerPrefs.SetInt("MissCurrent5", player.playerData.MissCurrent5);
        PlayerPrefs.SetInt("MissCurrent6", player.playerData.MissCurrent6);
        PlayerPrefs.SetInt("MissCurrent7", player.playerData.MissCurrent7);

        PlayerPrefs.SetInt("Miss0", player.playerData.Miss0);
        PlayerPrefs.SetInt("Miss1", player.playerData.Miss1);
        PlayerPrefs.SetInt("Miss2", player.playerData.Miss2);
        PlayerPrefs.SetInt("Miss3", player.playerData.Miss3);
        PlayerPrefs.SetInt("Miss4", player.playerData.Miss4);
        PlayerPrefs.SetInt("Miss5", player.playerData.Miss5);
        PlayerPrefs.SetInt("Miss6", player.playerData.Miss6);
        PlayerPrefs.SetInt("Miss7", player.playerData.Miss7);

        PlayerPrefs.SetInt("LimitAds", player.playerData.LimitAds);
        PlayerPrefs.SetString("TimeOneDay", player.playerData.TimeOneDay.ToString());

        PlayerPrefs.SetInt("ClickStart", player.playerData.ClickStart);
    }

    public static PlayerData LoadData()
    {
        int id = PlayerPrefs.GetInt("Id");
        string name = PlayerPrefs.GetString("Name");
        int cash = PlayerPrefs.GetInt("Cash");
        int ruby = PlayerPrefs.GetInt("Ruby");
        int level = PlayerPrefs.GetInt("Level");
        int exp = PlayerPrefs.GetInt("Exp");

        int x1 = PlayerPrefs.GetInt("X1");
        int x2 = PlayerPrefs.GetInt("X2");
        int x3 = PlayerPrefs.GetInt("X3");
        int boom = PlayerPrefs.GetInt("Boom");
        int freeze = PlayerPrefs.GetInt("Freeze");

        int power = PlayerPrefs.GetInt("Power");
        int cashPower = PlayerPrefs.GetInt("CashPower");

        float volume = PlayerPrefs.GetFloat("Volume");
        float fx = PlayerPrefs.GetFloat("Fx");
        int fps = PlayerPrefs.GetInt("Fps");

        int missCurrent0 = PlayerPrefs.GetInt("MissCurrent0");
        int missCurrent1 = PlayerPrefs.GetInt("MissCurrent1");
        int missCurrent2 = PlayerPrefs.GetInt("MissCurrent2");
        int missCurrent3 = PlayerPrefs.GetInt("MissCurrent3");
        int missCurrent4 = PlayerPrefs.GetInt("MissCurrent4");
        int missCurrent5 = PlayerPrefs.GetInt("MissCurrent5");
        int missCurrent6 = PlayerPrefs.GetInt("MissCurrent6");
        int missCurrent7 = PlayerPrefs.GetInt("MissCurrent7");

        int miss0 = PlayerPrefs.GetInt("Miss0");
        int miss1 = PlayerPrefs.GetInt("Miss1");
        int miss2 = PlayerPrefs.GetInt("Miss2");
        int miss3 = PlayerPrefs.GetInt("Miss3");
        int miss4 = PlayerPrefs.GetInt("Miss4");
        int miss5 = PlayerPrefs.GetInt("Miss5");
        int miss6 = PlayerPrefs.GetInt("Miss6");
        int miss7 = PlayerPrefs.GetInt("Miss7");

        int limitAds = PlayerPrefs.GetInt("LimitAds");
        string timeOneDay = PlayerPrefs.GetString("TimeOneDay");

        int clickStart = PlayerPrefs.GetInt("ClickStart");

        PlayerData playerData = new PlayerData();
        {
            playerData.Id = id;
            playerData.Name = name;
            playerData.Cash = cash;
            playerData.Ruby = ruby;
            playerData.Level = level;
            playerData.Exp = exp;

            playerData.X1 = x1;
            playerData.X2 = x2;
            playerData.X3 = x3;
            playerData.Boom = boom;
            playerData.Freeze = freeze;

            playerData.Power = power;
            playerData.CashPower = cashPower;

            playerData.Volume = volume;
            playerData.Fx = fx;
            playerData.Fps = fps;

            playerData.MissCurrent0 = missCurrent0;
            playerData.MissCurrent1 = missCurrent1;
            playerData.MissCurrent2 = missCurrent2;
            playerData.MissCurrent3 = missCurrent3;
            playerData.MissCurrent4 = missCurrent4;
            playerData.MissCurrent5 = missCurrent5;
            playerData.MissCurrent6 = missCurrent6;
            playerData.MissCurrent7 = missCurrent7;

            playerData.Miss0 = miss0;
            playerData.Miss1 = miss1;
            playerData.Miss2 = miss2;
            playerData.Miss3 = miss3;
            playerData.Miss4 = miss4;
            playerData.Miss5 = miss5;
            playerData.Miss6 = miss6;
            playerData.Miss7 = miss7;

            playerData.LimitAds = limitAds;
            playerData.TimeOneDay = timeOneDay;

            playerData.ClickStart = clickStart;
        };
        return playerData;
    }
}
