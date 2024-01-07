using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed;
    float resetSpeed;
    public float dashSpeed;
    public float dashTime;
    PhotonView photonView;
    Animator animator;
    Health health;
    LineRenderer rend;
    public TMP_Text nameDisplay;

    public float minX, maxX, minY, maxY;
    private void Start()
    {
        resetSpeed = speed;
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
        health = FindObjectOfType<Health>();
        rend = FindObjectOfType<LineRenderer>();

        if (photonView.IsMine)
        {
            nameDisplay.text = PhotonNetwork.NickName;
        }
        else
        {
            nameDisplay.text = photonView.Owner.NickName;
        }
    }
    private void Update()
    {
        if (photonView.IsMine)
        {
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;

            Wrap();

            if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
            {
                StartCoroutine(Dash());
            }

            if (moveInput == Vector2.zero)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isRunning", true);
            }

            rend.SetPosition(0, transform.position);
        }
        else
        {
            rend.SetPosition(1, transform.position);
        }
    }

    private void Wrap()
    {
        if (transform.position.x < minX)
        {
            transform.position = new Vector2(maxX, transform.position.y);
        }
        if (transform.position.x > maxX)
        {
            transform.position = new Vector2(minX, transform.position.y);
        }
        if (transform.position.y < minY)
        {
            transform.position = new Vector2(transform.position.x, maxY);
        }
        if (transform.position.y > maxY)
        {
            transform.position = new Vector2(transform.position.x, minY);
        }
    }

    IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (photonView.IsMine)
        {
            if (other.tag == "Enemy")
            {
                health.TakeDamage();
            }
        }
    }
}
