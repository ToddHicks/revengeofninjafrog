using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpikedBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 pushDirection;
    [SerializeField] private GameObject spikedBall;
    // Start is called before the first frame update
    private bool readyPush;
    void Start()
    {
        
        rb.AddForce(pushDirection,ForceMode2D.Impulse);
    }
    void Update(){
        if(spikedBall.GetComponent<Rigidbody2D>().velocity.x < 0)
            readyPush = true;
        if(spikedBall.GetComponent<Rigidbody2D>().velocity.x > 0 && readyPush){
            rb.AddForce(pushDirection,ForceMode2D.Impulse);
            readyPush = false;
        }
    }
}
