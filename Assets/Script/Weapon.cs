﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct WeaponDB
{
    public string Name;
    public int MaxBullet;
    public float Recoil;
    public int Weight;
    public float Shotrate;
    public string Bulletsort;
    public bool Autoshot;
    public int BodyDamage;
    public int HeadDamage;
    public float ReloadTime;
}

public enum Weapon_Type
{
    KATANA,
    HANDGUN,
    RIFLE,
    RPG,
    SNIPER,
    BOW,
    MINIGUN
}

//Weapon_Code in animator parameter
// 0 : katana // 1 : handgun // 2 : rifle // 3 : Minigun // 4 : RPG // 5 : Sniper

public abstract class Weapon : MonoBehaviour{

    public ObjType m_Type { get; set; }
    public string m_ObjName { get; set; }

    public int m_MaxBulletNum { get; set; }        //한번에장전 할수있는 최대탄수
    public int m_AmmoBulletNum { get; set; }        //탄창에 남아있는 총알수
    public string m_BulletSort { get; set; }        //총알의 종류
    public float m_Recoil { get; set; }             //반동
    public float m_Weight { get; set; }             //중량
    public float m_ShotRate { get; set; }           //연사속도
    public bool m_Autoshot { get; set; }            //자동발사여부
    public int m_BodyDamage { get; set; }
    public int m_HeadDamage { get; set; }
    public float m_ReloadTime { get; set; }         //재장전 시간
    public Weapon_Type m_WeaponType;

    public Transform m_ShootTarget { get; set; }
    public float m_StackedRecoil { get; set; }

    public Transform m_GrabPosRight;
    public Transform m_GrabPosLeft;

    //state(for animation)
    public bool m_IsShooting;

    public void Initialize()
    {
        WeaponDB DBData = ObjectManager.m_Inst.m_DBMgr.m_WeaponDB[gameObject.name];
        m_ObjName = DBData.Name;
        m_MaxBulletNum = DBData.MaxBullet;
        m_Recoil = DBData.Recoil;
        m_Weight = DBData.Weight;
        m_ShotRate = DBData.Shotrate;
        m_BulletSort = DBData.Bulletsort;
        m_Autoshot = DBData.Autoshot;
        m_BodyDamage = DBData.BodyDamage;
        m_HeadDamage = DBData.HeadDamage;
        m_ReloadTime = DBData.ReloadTime;

        m_Type = ObjType.OBJ_WEAPON;
    }

    public void ObjListAdd()
    {
        ObjectManager.m_Inst.Objects.m_Weaponlist.Add(this);
    }

    public void ChargeBullet()
    {
        m_AmmoBulletNum = m_MaxBulletNum;
    }

    public abstract void Shoot();

    public IEnumerator NarrowDownAim()
    {
        while(true)
        {
            m_StackedRecoil = Mathf.Max(0, m_StackedRecoil - (7 * Time.deltaTime));
            m_StackedRecoil = Mathf.Min(5, m_StackedRecoil);        //Max Recoil : 5.0f

            yield return null;
        }
    }

    public void HandleRecoil()  //수정할것
    {
        //m_StackedRecoil += m_Recoil;
        //GetComponentInParent<PlayerInput>().m_Mouse_Y = Mathf.Lerp(GetComponentInParent<PlayerInput>().m_Mouse_Y, GetComponentInParent<PlayerInput>().m_Mouse_Y - m_Recoil * 0.5f
            //, Time.deltaTime * 50);
    }
}