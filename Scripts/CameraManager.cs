using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject myCamera;
    [SerializeField] private PolygonCollider2D cd;
    //[SerializeField] private Color gizmosColor;
    
    void Start()
    {
        myCamera.GetComponent<CinemachineVirtualCamera>().Follow = PlayerManager.instance.currentPlayer.transform;
    }
    void Update(){
        if (myCamera.GetComponent<CinemachineVirtualCamera>().Follow == null)
            myCamera.GetComponent<CinemachineVirtualCamera>().Follow = PlayerManager.instance.currentPlayer.transform;
    }
}
