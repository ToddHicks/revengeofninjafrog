using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDetailsManager : MonoBehaviour
{
    [SerializeField] private int bgmIndex;
    void Start(){
        AudioManager.instance.PlayBGM(bgmIndex);
    }
}
