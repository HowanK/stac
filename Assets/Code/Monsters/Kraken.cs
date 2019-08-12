﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kraken : ShowMonster
{
    private int legNum;
    private int addPower, loseDefens;
    private int maxHp;
    private int randomAction;

    protected override void Start()
    {
        base.Start();
        legNum = 6;
        addPower = 2;
        loseDefens = 5;
        maxHp = hp;
        randomAction = Random.Range(0, 10);
        if(randomAction <= 5)
        {
            isAttack = false;
        }
        else
        {
            isAttack = true;
        }
        attackUI.SetActive(isAttack);
        defensUI.SetActive(!isAttack);
    }

    public override void LoseHp(int damage)
    {
        if(ondefensPower > 0)
        {
            int defens = ondefensPower - damage;
            ondefensPower = defens;
            if(ondefensPower < 0)
            {
                shakePower = Mathf.Abs(ondefensPower);
                hp += ondefensPower;
                StartCoroutine(Shaking());
            }
        }
        else
        {
            shakePower = damage;
            hp -= damage;
            StartCoroutine(Shaking());
        }
        SoundManager.Instance.PlaySFX(SoundManager.SFXList.MONSTER_DAMAGE);
        
        if(hp <= 0)
        {
            legNum--;
            if(legNum <= 0)
            {
                ui.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
            else
            {
                hp = (int)(maxHp * 0.1f);
                attackPower += addPower;
                defensPower -= loseDefens;
            }
        }
    }

    protected override void ChangeState()
    {
        if(isAttack)
        {
            action = ACTION.ATTACK;
        }
        else
        {
            action = ACTION.DEFENS;
        }
        
        randomAction = Random.Range(0, 10);
        if(randomAction <= 5)
        {
            isAttack = false;
        }
        else
        {
            isAttack = true;
        }
    }

    protected override void Attack()
    {
        Knight.instance.LoseHp(attackPower * legNum);
    }
    
}