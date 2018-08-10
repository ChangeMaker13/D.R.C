﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : Weapon
{
    //this makes spark effect when bullet is fired.
    public MeshRenderer m_MuzzleFlash;
    public MeshRenderer m_MuzzleFlash2;

    public Transform m_ShootPos;

    private Animator m_Ani;

    public override void Shoot()
    {
        if (m_AmmoBulletNum <= 0)
            return;

        if (m_WeaponType == Weapon_Type.RIFLE)
        {
            StopCoroutine("Shoot_Rifle");
            StartCoroutine("Shoot_Rifle");
        }
        else if (m_WeaponType == Weapon_Type.MINIGUN)
        {
            StopCoroutine("Shoot_Minigun");
            StartCoroutine("Shoot_Minigun");
        }
        else
            ShootBullet();
    }

    void Awake()
    {
        m_Ani = transform.root.GetComponent<Animator>();

        Initialize();
    }

    void OnEnable()
    {
        m_MuzzleFlash.enabled = false;
        m_MuzzleFlash2.enabled = false;
    }

    void Start()
    {
        ObjListAdd();
        m_AmmoBulletNum = 0;

        m_AmmoBulletNum = m_MaxBulletNum;
    }

    public IEnumerator Shoot_Rifle()
    {
        for(int i = 0; i < 3; i++)      //Shoot 3 bullet per 0.15 sec at once 
        {
            ShootBullet();
            yield return new WaitForSeconds(0.15f);
        }
    }

    public IEnumerator Shoot_Minigun()
    {
        if (!m_Ani.GetBool("Minigun_Attack_Bool"))
        {
            m_Ani.SetBool("Minigun_Attack_Bool", true);
            yield return new WaitForSeconds(1.5f);
        }
        int k = 30;
        while (m_AmmoBulletNum > 0 && k > 0)
        {
            if (!m_Ani.GetBool("Minigun_Attack_Bool")) break;
            ShootBullet();
            k--;
            yield return new WaitForSeconds(0.05f);
        }
        m_Ani.SetBool("Minigun_Attack_Bool", false);
    }
    
    public void ShootBullet()
    {
        Vector3 Dir = m_ShootTarget.position - m_ShootPos.position;
        Dir = Dir / Dir.magnitude;
        GameObject bullet = ObjectPoolMgr.instance.CreatePooledObject(m_BulletSort, m_ShootPos.position, Quaternion.LookRotation(Dir));
        bullet.SendMessage("SetBodyDamage", m_BodyDamage);
        bullet.SendMessage("SetHeadDamage", m_HeadDamage);

        m_AmmoBulletNum -= 1;
        Makeflash();
    }

    public void Makeflash()
    {
        m_MuzzleFlash.transform.eulerAngles += new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));
        m_MuzzleFlash2.transform.eulerAngles += new Vector3(Random.Range(0, 90), Random.Range(0, 90), Random.Range(0, 90));

        m_MuzzleFlash.enabled = true;
        m_MuzzleFlash2.enabled = true;

        Invoke("Cancelflash", 0.05f); 
    }

    public void Cancelflash()
    {
        m_MuzzleFlash.enabled = false;
        m_MuzzleFlash2.enabled = false;
    }
}
