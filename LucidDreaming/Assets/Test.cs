using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Sirenix.OdinInspector;
using UnityEngine;
using YuoTools;
using YuoTools.Extend.Helper;
using YuoTools.Main.Ecs;
using YuoTools.UI;

public class Test : MonoBehaviour
{
    public float StartHP = 6000;
    public float NowHp = 6000;

    [Button]
    public void Test1(float time)
    {
        timer = 0;
        NowHp = StartHP;
        timer_不灭之握 = 0;
        timer_心之钢 = 0;
        count_不灭之握 = 0;
        count_心之钢 = 0;
        while (timer < time)
        {
            GameUpdate();
        }

        Debug.Log($"总共时长{timer}秒,叠了不灭之握{count_不灭之握}层,心之钢{count_心之钢}层,最终血量{NowHp}");
    }

    float timer = 0;
    private float cd_不灭之握 = 4;
    private float cd_心之钢 = 20;
    float timer_不灭之握 = 0;
    float timer_心之钢 = 0;
    private float timeDelta = 0.2f;
    int count_不灭之握 = 0;
    int count_心之钢 = 0;

    private void GameUpdate()
    {
        timer += timeDelta;
        if (timer_不灭之握 > cd_不灭之握)
        {
            NowHp += 10;
            count_不灭之握++;
            timer_不灭之握 = 0;
        }
        else
        {
            timer_不灭之握 += timeDelta;
        }

        if (timer_心之钢 > cd_心之钢)
        {
            Duang();
            Duang();
            Duang();
            Duang();
            Duang();
            count_心之钢 += 5;
            timer_心之钢 = 0;
        }
        else
        {
            timer_心之钢 += timeDelta;
        }
    }

    void Duang()
    {
        var Damage = 125 + NowHp * 0.06f;
        NowHp += Damage * 0.1f;
    }
}