using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    int score = 0;
    PhotonView photonView;
    public TMP_Text scoreDisplay;


    private void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void AddScore()
    {
        photonView.RPC("AddScoreRPC", RpcTarget.All);
    }

    [PunRPC]
    void AddScoreRPC()
    {
        score++;
        scoreDisplay.text = score.ToString();
    }
}
