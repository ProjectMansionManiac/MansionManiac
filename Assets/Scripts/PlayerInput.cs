﻿using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        Vector2 directionalInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (directionalInput.y < 0)
        {
            directionalInput.y = 1f;
        }

        if (!Input.GetButton("Jump") && !Input.GetButton("Fire1"))
        {
            
            if (directionalInput == Vector2.zero)
            {
                playerMovement.animator.Play("Idle");
            }
            else if (-.2f > directionalInput.x && directionalInput.x > .2f )
            {
                playerMovement.animator.Play("Walk");
            }
        }


        if (directionalInput.x < 0)
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        } else if (directionalInput == Vector2.zero)
        {

        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }

        playerMovement.SetDirectionalInput(directionalInput);

        if (Input.GetButtonDown("Jump") && !Input.GetButton("Fire1"))
        {
            playerMovement.OnJumpInputDown();
            playerMovement.animator.Play("Jump");
        }

        //if (Input.GetButtonUp("Jump"))
        //{
        //    playerMovement.OnJumpInputUp();
        //    playerMovement.animator.Play("Idle");
        //}
    }
}
