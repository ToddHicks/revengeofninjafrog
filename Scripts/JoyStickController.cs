using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickController : MonoBehaviour
{
    public static JoyStickController instance;
    [SerializeField] private VariableJoystick joystick;
    [SerializeField] private Button jumpButton;
    public void Awake(){
        if(instance == null){
            instance = this;
        }
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
    }
    public void AssignPlayerControls(Player player){
        player.joystick = joystick;
        //jumpButton.onClick.RemoveAllListeners();
        //jumpButton.onClick.AddListener(player.JumpButton);
    }
}
