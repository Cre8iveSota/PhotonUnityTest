using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    PlayerController[] players;
    PlayerController nearestPlayer;
    Score score;
    public float speed;
    private void Start()
    {
        players = FindObjectsOfType<PlayerController>();
        score = FindObjectOfType<Score>();
    }

    private void Update()
    {
        float distanceOne = Vector2.Distance(transform.position, players[0].transform.position);
        float distanceTwo = Vector2.Distance(transform.position, players[1].transform.position);
        if (distanceOne < distanceTwo)
        {
            nearestPlayer = players[0];
        }
        else
        {
            nearestPlayer = players[1];
        }

        if (nearestPlayer != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, nearestPlayer.transform.position, speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        // To call only once, if the PhotonNetwork.IsMasterClient is used, the procedure are invoked by Master only. 
        if (PhotonNetwork.IsMasterClient)
        {
            if (other.tag == "GoldenRay")
            {
                score.AddScore();
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
