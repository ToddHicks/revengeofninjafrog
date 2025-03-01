using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCheckbox : MonoBehaviour
{
    [SerializeField] private GameObject controllerCheckbox;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerManager.instance.pcMode == false)
            controllerCheckbox.GetComponent<Toggle>().isOn = true;
        else
            controllerCheckbox.GetComponent<Toggle>().isOn = false;
    }
}
