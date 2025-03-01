using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject levelButton;
    [SerializeField] private Transform levelButtonParent;

    [SerializeField] private bool[] levelOpen;

    public void Start(){
        // Destroy all children first, just in case.
        for(int i = levelButtonParent.transform.childCount - 1; i >= 0; i--)
            Destroy(levelButtonParent.transform.GetChild(i).gameObject);
        // Add Village
        GameObject villageButton = Instantiate(levelButton, levelButtonParent);
        villageButton.GetComponent<Button>().onClick.AddListener(() => LoadLevel("Village"));
        villageButton.GetComponentInChildren<TextMeshProUGUI>().text = "Village";
        for(int i = 0; i < SceneManager.sceneCountInBuildSettings && i <= PlayerManager.instance.highestLevelUnlocked; i++)
        {
            string sceneName = "Level " + i;
            GameObject newButton = Instantiate(levelButton, levelButtonParent);
            newButton.GetComponent<Button>().onClick.AddListener(() => LoadLevel(sceneName));
            newButton.GetComponentInChildren<TextMeshProUGUI>().text = sceneName;
        }
    }

    public void LoadLevel(string sceneName){
        SceneManager.LoadScene(sceneName);
        AudioManager.instance.PlaySFX(7);
    }
}
