using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


// Do not forget to change the inheritance original one to MonoBehaviourPunCallbacks
public class MainMenu : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;
    public TMP_InputField nameInput;

    public void ChangeName()
    {
        PhotonNetwork.NickName = nameInput.text;
    }

    public void CreateRoom()
    {
        // making the room visible to others, public or private, and of course, setting a player
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;


        PhotonNetwork.CreateRoom(createInput.text, roomOptions);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }


    // use this override, we have to change the inheritance class MonoBehaviour to MonoBehaviourPunCallbacks
    public override void OnJoinedRoom()
    {
        // choose the scene you want to show next
        PhotonNetwork.LoadLevel("Game");
    }

}
