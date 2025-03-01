using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Fruit and level.
    public int appleCount = 0; // Heal
    public int bananaCount = 0; // Jump level
    public int cherryCount = 0; // Speed level
    public int melonCount = 0; // Attack level
    public int pineappleCount = 0; // Hitpoint level
    public int lives = 10; // Lives

    private int level2 = 100;
    private int level3 = 200;
    private int level4 = 400;
    private int level5 = 700;
    private int level6 = 1100;
    private int level7 = 1600;
    private int level8 = 2200;
    private int level9 = 3000;
    private int level10 = 4000;

    public int jumpLevel = 1;
    public int speedLevel = 1;
    public int attackLevel = 1;
    public int hitPointLevel = 1;

    [SerializeField] public int activeJumpLevel = 1;
    [SerializeField] public int activeSpeedLevel = 1;
    [SerializeField] public int activeAttackLevel = 1;
    [SerializeField] public int activeHitPointLevel = 1;

    public int highestLevelUnlocked = 0;


    public static PlayerManager instance;

    [SerializeField] private GameObject playerPrefab;
    public Transform respawnPoint;
    public GameObject currentPlayer;
    private Player player;
    public bool inGame = false;


    [SerializeField] public bool pcMode;

    private void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
        if(respawnPoint != null)
            PlayerRespawn();
    }
    public void Start(){
        if(StatsPresenter.instance != null){
            StatsPresenter.instance.SetApples(appleCount);
            StatsPresenter.instance.SetBananas(bananaCount);
            StatsPresenter.instance.SetCherries(cherryCount);
            StatsPresenter.instance.SetMelons(melonCount);
            StatsPresenter.instance.SetPineapples(pineappleCount);
            StatsPresenter.instance.SetLives(lives);
            StatsPresenter.instance.SetHitPoints(player.currentHitPoints);
        }
    }

    public void UnlockLevels(int maxLevel){
        if(maxLevel > highestLevelUnlocked){
            highestLevelUnlocked = maxLevel;
        }
    }
    public void PlayerRespawn()
    {
        if(currentPlayer == null){
            AudioManager.instance.PlayConsistentSFX(12);
            currentPlayer = Instantiate(playerPrefab, respawnPoint.position, transform.rotation);
            player = currentPlayer.GetComponent<Player>();
            player.jumpLevel = activeJumpLevel;
            player.attackLevel = activeAttackLevel;
            player.speedLevel = activeSpeedLevel;
            player.maxHitPoints = activeHitPointLevel + 2;
            player.currentHitPoints = player.maxHitPoints;
            player.pcMode = pcMode;
            if(StatsPresenter.instance != null){
                StatsPresenter.instance.SetLives(lives);
                StatsPresenter.instance.SetHitPoints(player.currentHitPoints);
            } 
            JoyStickController.instance.AssignPlayerControls(currentPlayer.GetComponent<Player>());
        }
    }
    public void KillPlayer(){

        AudioManager.instance.PlayConsistentSFX(1);
        Destroy(currentPlayer);
        lives --;
    }
    public void AddApple(){
        AudioManager.instance.PlaySFX(9);
        appleCount+=1;
        if(StatsPresenter.instance != null){
            StatsPresenter.instance.SetApples(appleCount);
        }
        if(appleCount % 50 == 0)
        {
            lives++;
            StatsPresenter.instance.SetLives(lives);
            StatsPresenter.instance.SetMessage("Extra Life!", 3f);
        }   
        Heal();
    }
    public void AddBanana(){
        AudioManager.instance.PlaySFX(9);
        int currentLevel = jumpLevel;
        bool levelChanged = false;
        bananaCount+=1;
        if(StatsPresenter.instance != null){
            StatsPresenter.instance.SetBananas(bananaCount);
        }
        if(bananaCount == level10){
            jumpLevel = 10;
            levelChanged = true;
        }
        else if(bananaCount == level9){
            jumpLevel = 9;
            levelChanged = true;
        }
        else if(bananaCount == level8){
            jumpLevel = 8;
            levelChanged = true;
        }
        else if(bananaCount == level7){
            jumpLevel = 7;
            levelChanged = true;
        }
        else if(bananaCount == level6){
            jumpLevel = 6;
            levelChanged = true;
        }
        else if(bananaCount == level5){
            jumpLevel = 5;
            levelChanged = true;
        }
        else if(bananaCount == level4){
            jumpLevel = 4;
            levelChanged = true;
        }
        else if(bananaCount == level3){
            jumpLevel = 3;
            levelChanged = true;
        }
        else if(bananaCount == level2){
            jumpLevel = 2;
            levelChanged = true;
        }
        if(levelChanged && currentLevel+1 == jumpLevel)
            activeJumpLevel = jumpLevel;
        if(levelChanged){
            player.jumpLevel = activeJumpLevel;
            LevelUp();
            StatsPresenter.instance.SetMessage("Jump Level Up!", 3f);
        }
        
    }
    public void AddCherry(){
        AudioManager.instance.PlaySFX(9);
        int currentLevel = speedLevel;
        bool levelChanged = false;
        cherryCount+=1;
        if(StatsPresenter.instance != null){
            StatsPresenter.instance.SetCherries(cherryCount);
        }
        if(cherryCount == level10){
            speedLevel = 10;
            levelChanged = true;
        }
        else if(cherryCount == level9){
            speedLevel = 9;
            levelChanged = true;
        }
        else if(cherryCount == level8){
            speedLevel = 8;
            levelChanged = true;
        }
        else if(cherryCount == level7){
            speedLevel = 7;
            levelChanged = true;
        }
        else if(cherryCount == level6){
            speedLevel = 6;
            levelChanged = true;
        }
        else if(cherryCount == level5){
            speedLevel = 5;
            levelChanged = true;
        }
        else if(cherryCount == level4){
            speedLevel = 4;
            levelChanged = true;
        }
        else if(cherryCount == level3){
            speedLevel = 3;
            levelChanged = true;
        }
        else if(cherryCount == level2){
            speedLevel = 2;
            levelChanged = true;
        }
        if(levelChanged && currentLevel+1 == speedLevel)
            activeSpeedLevel = speedLevel;
        if(levelChanged){
            player.speedLevel = activeSpeedLevel;
            LevelUp();
            StatsPresenter.instance.SetMessage("Speed Level Up!", 3f);
        }
    }
    public void AddMelon(){
        AudioManager.instance.PlaySFX(9);
        int currentLevel = attackLevel;
        bool levelChanged = false;
        melonCount+=1;
        if(StatsPresenter.instance != null){
            StatsPresenter.instance.SetMelons(melonCount);
        }
        if(melonCount == level10){
            attackLevel = 10;
            levelChanged = true;
        }
        else if(melonCount == level9){
            attackLevel = 9;
            levelChanged = true;
        }
        else if(melonCount == level8){
            attackLevel = 8;
            levelChanged = true;
        }
        else if(melonCount == level7){
            attackLevel = 7;
            levelChanged = true;
        }
        else if(melonCount == level6){
            attackLevel = 6;
            levelChanged = true;
        }
        else if(melonCount == level5){
            attackLevel = 5;
            levelChanged = true;
        }
        else if(melonCount == level4){
            attackLevel = 4;
            levelChanged = true;
        }
        else if(melonCount == level3){
            attackLevel = 3;
            levelChanged = true;
        }
        else if(melonCount == level2){
            attackLevel = 2;
            levelChanged = true;
        }
        if(levelChanged && currentLevel+1 == attackLevel)
            activeAttackLevel = attackLevel;
        if(levelChanged){
            player.attackLevel = activeAttackLevel;
            LevelUp();
            StatsPresenter.instance.SetMessage("Attack Level Up!", 3f);
        }
    }
    public void AddPineApple(){
        AudioManager.instance.PlaySFX(9);
        int currentLevel = hitPointLevel;
        bool levelChanged = false;
        pineappleCount+=1;
        if(StatsPresenter.instance != null){
            StatsPresenter.instance.SetPineapples(pineappleCount);
        }
        if(pineappleCount == level10){
            hitPointLevel = 10;
            levelChanged = true;
        }
        else if(pineappleCount == level9){
            hitPointLevel = 9;
            levelChanged = true;
        }
        else if(pineappleCount == level8){
            hitPointLevel = 8;
            levelChanged = true;
        }
        else if(pineappleCount == level7){
            hitPointLevel = 7;
            levelChanged = true;
        }
        else if(pineappleCount == level6){
            hitPointLevel = 6;
            levelChanged = true;
        }
        else if(pineappleCount == level5){
            hitPointLevel = 5;
            levelChanged = true;
        }
        else if(pineappleCount == level4){
            hitPointLevel = 4;
            levelChanged = true;
        }
        else if(pineappleCount == level3){
            hitPointLevel = 3;
            levelChanged = true;
        }
        else if(pineappleCount == level2){
            hitPointLevel = 2;
            levelChanged = true;
        }
        if(levelChanged && currentLevel+1 == hitPointLevel)
            activeHitPointLevel = hitPointLevel;
        if(levelChanged){
            player.hitPointLevel = activeHitPointLevel;
            player.maxHitPoints = activeHitPointLevel + 2;
            player.currentHitPoints = player.maxHitPoints;
            LevelUp();
            StatsPresenter.instance.SetMessage("Max Hitpoints Level Up!", 3f);
        }
    }
    public void Heal(){
        if(player.currentHitPoints < player.maxHitPoints){
            player.currentHitPoints+=1;
            StatsPresenter.instance.SetHitPoints(player.currentHitPoints);
        }
    }
    private void LevelUp(){
        AudioManager.instance.PlayConsistentSFX(10);
    }

    public void ToggleTouchControls(){
        pcMode = !pcMode;
        if(pcMode)
            PlayerPrefs.SetInt("pcMode", 1);
        else
            PlayerPrefs.SetInt("pcMode", 0);
    }

    void Update()
    {
        if(respawnPoint!=null)
            PlayerRespawn();
    }
}
