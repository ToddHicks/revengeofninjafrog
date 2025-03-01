using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillLevelSliderControlUI : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private string maxSliderValueReference;
    [SerializeField] private TextMeshProUGUI text;
    private void Awake(){
        slider.wholeNumbers = true;
        SetValues();
    }
    private void Start(){
        slider.onValueChanged.AddListener(SliderValue);
    }
    public void SetValues(){
        if (maxSliderValueReference == "jumpLevel"){
            slider.minValue = 1;
            slider.maxValue = PlayerManager.instance.jumpLevel;
            slider.value = PlayerManager.instance.activeJumpLevel;
        }
        else if(maxSliderValueReference == "speedLevel"){
            slider.minValue = 1;
            slider.maxValue = PlayerManager.instance.speedLevel;
            slider.value = PlayerManager.instance.activeSpeedLevel;
        }
        else if(maxSliderValueReference == "attackLevel") {
            slider.minValue = 1;
            slider.maxValue = PlayerManager.instance.attackLevel;
            slider.value = PlayerManager.instance.activeAttackLevel;
        }
        else if(maxSliderValueReference == "hitPointLevel") {
            slider.minValue = 1;
            slider.maxValue = PlayerManager.instance.hitPointLevel;
            slider.value = PlayerManager.instance.activeHitPointLevel;
        }
        text.text = string.Format("{0}", slider.value);
    }
    
    private void SliderValue(float value){
        Player player = PlayerManager.instance.currentPlayer.GetComponent<Player>();
        if (maxSliderValueReference == "jumpLevel"){
            PlayerManager.instance.activeJumpLevel = (int)value;
            player.jumpLevel = PlayerManager.instance.activeJumpLevel;
            PlayerPrefs.SetInt("activeJumpLevel", PlayerManager.instance.activeJumpLevel);
            
        }
        else if(maxSliderValueReference == "speedLevel"){
            PlayerManager.instance.activeSpeedLevel = (int)value;
            player.speedLevel = PlayerManager.instance.activeSpeedLevel;
            PlayerPrefs.SetInt("activeSpeedLevel", PlayerManager.instance.activeSpeedLevel);
        }
        else if(maxSliderValueReference == "attackLevel") {
            PlayerManager.instance.activeAttackLevel = (int)value;
            player.attackLevel = PlayerManager.instance.activeAttackLevel;
            PlayerPrefs.SetInt("activeAttackLevel", PlayerManager.instance.activeAttackLevel);
        }
        else if(maxSliderValueReference == "hitPointLevel") {
            PlayerManager.instance.activeHitPointLevel = (int)value;
            player.hitPointLevel = PlayerManager.instance.activeHitPointLevel;
            player.maxHitPoints = PlayerManager.instance.activeHitPointLevel + 2;
            if(player.currentHitPoints > player.maxHitPoints)
                player.currentHitPoints = player.maxHitPoints;
            PlayerPrefs.SetInt("activeHitPointLevel", PlayerManager.instance.activeHitPointLevel);
        }
        text.text = string.Format("{0}", slider.value);
    }
}
