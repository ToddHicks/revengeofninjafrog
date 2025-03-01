using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision){
        // if(collision.GetComponent<Player>()!=null)
        if(collision.tag == "Player"){
            //Debug.Log("Knocked!");
            Player player = collision.GetComponent<Player>();
            player.Knockback(transform);
        }
    }
}
