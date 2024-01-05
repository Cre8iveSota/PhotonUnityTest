using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    PhotonView photonView;
    Animator animator;
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;




            if (moveInput == Vector2.zero)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isRunning", true);
            }
        }
    }
}
