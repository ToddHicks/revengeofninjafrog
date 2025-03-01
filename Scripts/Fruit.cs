using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FruitType{
    apple,
    banana,
    cherry,
    melon,
    pineapple
}
public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject fruitCollection;
    [SerializeField] private int fruitCount = 1;
    public void Awake(){
        GetComponent<Animator>().SetInteger("fruitReference", (int)fruitReference);
    }
    //[SerializeField] private int fruitReference;
    public FruitType fruitReference;
    // Use collect if we end up having an animation that triggers.
    //private bool collect = true;
    private void OnTriggerEnter2D(Collider2D collision){
        if(collision.GetComponent<Player>() != null /*&& collect*/){
            if(fruitReference == FruitType.apple){
                for(int c = 0; c < fruitCount; ++c)
                    PlayerManager.instance.AddApple();
            }
            else if(fruitReference ==FruitType.banana){
                for(int c = 0; c < fruitCount; ++c)
                    PlayerManager.instance.AddBanana();
            }
            else if(fruitReference ==FruitType.cherry){
                for(int c = 0; c < fruitCount; ++c)
                    PlayerManager.instance.AddCherry();
            }
            else if(fruitReference ==FruitType.melon){
                for(int c = 0; c < fruitCount; ++c)
                    PlayerManager.instance.AddMelon();
            }
            else if(fruitReference ==FruitType.pineapple){
                for(int c = 0; c < fruitCount; ++c)
                    PlayerManager.instance.AddPineApple();
            }
            if(fruitCollection!=null){
                GameObject fruitFX = Instantiate(fruitCollection, transform.position, transform.rotation);
                Destroy(fruitFX, .3f);
            }
            //collect = false;
            Destroy(gameObject);
        }
    }
}
