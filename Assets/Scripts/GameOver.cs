using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public TMP_Text scoreDisplay;
    public GameObject restartButton;
    public GameObject waitingText;
    PhotonView photonView;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        scoreDisplay.text = FindObjectOfType<Score>().score.ToString();

        if (PhotonNetwork.IsMasterClient == false)
        {
            restartButton.SetActive(false);
            waitingText.SetActive(true);
        }
    }

    public void OnClickRestart()
    {
        photonView.RPC("Restart", RpcTarget.All);
    }

    [PunRPC]
    void Restart()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
