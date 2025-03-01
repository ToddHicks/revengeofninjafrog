using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Start(){
        if(instance == null){
            instance = this;
            LoadPlayerStats();
        }
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    public void LoadPlayerStats(){
        // Save Apples
        PlayerManager.instance.appleCount = PlayerPrefs.GetInt("appleCount");
        // Save Bananas
        PlayerManager.instance.bananaCount = PlayerPrefs.GetInt("bananaCount");
        // Save Cherries
        PlayerManager.instance.cherryCount = PlayerPrefs.GetInt("cherryCount");
        // Save Melons
        PlayerManager.instance.melonCount = PlayerPrefs.GetInt("melonCount");
        // Save PineApples
        PlayerManager.instance.pineappleCount = PlayerPrefs.GetInt("pineappleCount");
        // Save Lives
        PlayerManager.instance.lives = PlayerPrefs.GetInt("lives");
        // Save Jump Level
        PlayerManager.instance.jumpLevel = PlayerPrefs.GetInt("jumpLevel");
        // Save Speed Level
        PlayerManager.instance.speedLevel = PlayerPrefs.GetInt("speedLevel");
        // Save Hitpoint Level
        PlayerManager.instance.hitPointLevel = PlayerPrefs.GetInt("hitPointLevel");
        // Save Attack Level
        PlayerManager.instance.attackLevel = PlayerPrefs.GetInt("attackLevel");
        // Save Selected Jump Level
        PlayerManager.instance.activeJumpLevel = PlayerPrefs.GetInt("activeJumpLevel");
        // Save Selected Speed Level
        PlayerManager.instance.activeSpeedLevel = PlayerPrefs.GetInt("activeSpeedLevel");
        // Save Selected Attack Level
        PlayerManager.instance.activeAttackLevel = PlayerPrefs.GetInt("activeAttackLevel");
        // Save Selected Hitpoint Level
        PlayerManager.instance.activeHitPointLevel = PlayerPrefs.GetInt("activeHitPointLevel");
        // Save Max Level Unlocked
        PlayerManager.instance.highestLevelUnlocked = PlayerPrefs.GetInt("highestLevelUnlocked");
        PlayerManager.instance.pcMode = PlayerPrefs.GetInt("pcMode")==1;

        // Bonus Levels.

    }
    public void NewGame(){
        PlayerManager.instance.appleCount = 0;
        PlayerManager.instance.bananaCount = 0;
        PlayerManager.instance.cherryCount = 0;
        PlayerManager.instance.melonCount = 0;
        PlayerManager.instance.pineappleCount = 0;
        PlayerManager.instance.lives = 10;
        PlayerManager.instance.hitPointLevel = 1;
        PlayerManager.instance.speedLevel = 1;
        PlayerManager.instance.jumpLevel = 1;
        PlayerManager.instance.attackLevel = 1;
        PlayerManager.instance.activeAttackLevel = 1;
        PlayerManager.instance.activeHitPointLevel = 1;
        PlayerManager.instance.activeJumpLevel = 1;
        PlayerManager.instance.activeSpeedLevel = 1;
        PlayerManager.instance.highestLevelUnlocked = -1;
        SavePlayerStats();
        // Reset friends
        PlayerPrefs.SetInt("Ted",0);
        PlayerPrefs.SetInt("Stan",0);
    }
    public void SavePlayerStats(){
        // Save Apples
        PlayerPrefs.SetInt("appleCount", PlayerManager.instance.appleCount);
        // Save Bananas
        PlayerPrefs.SetInt("bananaCount", PlayerManager.instance.bananaCount);
        // Save Cherries
        PlayerPrefs.SetInt("cherryCount", PlayerManager.instance.cherryCount);
        // Save Melons
        PlayerPrefs.SetInt("melonCount", PlayerManager.instance.melonCount);
        // Save PineApples
        PlayerPrefs.SetInt("pineappleCount", PlayerManager.instance.pineappleCount);
        // Save Lives
        PlayerPrefs.SetInt("lives", PlayerManager.instance.lives);
        // Save Jump Level
        PlayerPrefs.SetInt("jumpLevel", PlayerManager.instance.jumpLevel);
        // Save Speed Level
        PlayerPrefs.SetInt("speedLevel", PlayerManager.instance.speedLevel);
        // Save Hitpoint Level
        PlayerPrefs.SetInt("hitPointLevel", PlayerManager.instance.hitPointLevel);
        // Save Attack Level
        PlayerPrefs.SetInt("attackLevel", PlayerManager.instance.attackLevel);
        // Save Selected Jump Level
        PlayerPrefs.SetInt("activeJumpLevel", PlayerManager.instance.activeJumpLevel);
        // Save Selected Speed Level
        PlayerPrefs.SetInt("activeSpeedLevel", PlayerManager.instance.activeSpeedLevel);
        // Save Selected Attack Level
        PlayerPrefs.SetInt("activeAttackLevel", PlayerManager.instance.activeAttackLevel);
        // Save Selected Hitpoint Level
        PlayerPrefs.SetInt("activeHitPointLevel", PlayerManager.instance.activeHitPointLevel);
        // Save Max Level Unlocked
        PlayerPrefs.SetInt("highestLevelUnlocked", PlayerManager.instance.highestLevelUnlocked);
    }
}
