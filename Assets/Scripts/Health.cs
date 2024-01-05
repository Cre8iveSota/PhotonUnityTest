using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health = 10;
    public TMP_Text healthDisplay;
    PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }
    public void TakeDamage()
    {
        photonView.RPC("TakeDamageRPC", RpcTarget.All); // RpcTarget.All allow to all players' Syncronize
    }

    // This attribute just lets unity know that we want to syncronize all the code that in inside of this function
    [PunRPC]
    void TakeDamageRPC()
    {
        health--;
        healthDisplay.text = health.ToString();
    }
}
