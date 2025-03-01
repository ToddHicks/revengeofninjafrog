using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private int bgmIndex;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;
    private void Awake(){
        if(instance == null){
            instance = this;
        }
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    /*private void Start(){
        bgmIndex = 0;
    }*/
    private void Update(){
        if(!bgm[bgmIndex].isPlaying){
            bgm[bgmIndex].Play();
        }
    }
    public void PlaySFX(int sfxToPlay){
        if(sfxToPlay < sfx.Length){
            sfx[sfxToPlay].pitch = Random.Range(0.85f, 1.15f);
            sfx[sfxToPlay].Play();
        }
    }
    public void PlayConsistentSFX(int sfxToPlay){
        if(sfxToPlay < sfx.Length){
            sfx[sfxToPlay].Play();
        }
    }
    public void PlayBGM(int bgmToPlay){
        for(int i =0; i < bgm.Length; i++){
            bgm[i].Stop();
        }
        if(bgmToPlay < bgm.Length){
            bgmIndex = bgmToPlay;
        }
        bgm[bgmIndex].Play();
    }
}
