﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhaseBigFireballs : BossPhase
{
    public GameObject BigFireBallPrefab;

    [SerializeField] float TimeBetweenFireballs = 1f;
    [SerializeField] Transform shootingPivot;
    bool canShoot = false;

    public override void ActivatePhase()
    {
        Debug.Log("Big Fireball Phase Started");
        base.ActivatePhase();
        head.tag = "Enemy";
        torso.tag = "Enemy";
        restbody.tag = "Enemy";

        canShoot = true;

        StartCoroutine(HandleShooting());
        //animator.Play("InitPhase");
    }

    private void OnEnable()
    {
        ActivatePhase();
    }

    public override void DeactivatePhase()
    {
        base.DeactivatePhase();

        canShoot = false;
    }

    void OnDisable()
    {
        DeactivatePhase();
        canShoot = false;
    }

    public override void Update()
    {
        base.Update();
    }

    IEnumerator HandleShooting()
    {
        while (canShoot)
        {
            yield return new WaitForSeconds(TimeBetweenFireballs);
            Shoot();
        }
    }

    void Shoot()
    {
        SoundManager.instance.PlayFireballThrowSound();
        Vector2 direction = (playerObject.transform.position - shootingPivot.position).normalized;
        GameObject fireball = (GameObject)Instantiate(BigFireBallPrefab, shootingPivot.position, Quaternion.identity);
        fireball.GetComponent<BigFireball>().Initialize(5f, direction);

        if (PhaseController.instance.currentPhaseIndex - 1 == 1)
        {
            spriteAnimator.Play("EnemyFireballNaked");
            Debug.Log(spriteAnimator.gameObject.name);
        }
        else if(PhaseController.instance.currentPhaseIndex - 1 == 3)
        {
            spriteAnimator.Play("EnemyFireballArm");
        }
        else if (PhaseController.instance.currentPhaseIndex - 1 == 5)
        {
            spriteAnimator.Play("EnemyFireballHead");
        }
    }
}
