using Assets.Scripts.Gameplay.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banshee : Attacker
{
    public Banshee(int level) : base(level)
    {
    }


    public Animator animator;
    public int level;
    // Start is called before the first frame update
    public void Start()
    {
        AttackRange = ManageInfor.BansheeRange;
        SelectedRange = ManageInfor.BansheeSelectedRange;
       // Debug.Log("Banshee: " + AttackRange);
        if ( level == null)
        {
            Level = 0;
        }
        else
        {
            Level = level;
        }
      
        CoolDown = 1;
        BaseDamage = ManageInfor.BansheeDamage;
        BaseHitPoints = ManageInfor.BansheeBaseHitPoints;
        BaseSpeed = 15;
        //Damage = BaseDamage * damagePer * Level;


        Initialize(ManageInfor.BansheeDamagePer, ManageInfor.BansheeHitPointPer, 0.1f);
    }

    // Update is called once per frame
    public void Update()
    {
        if (isAttack)
        {
            animator.SetBool("isAttack", true);
         
        }
        else
        {
            animator.SetBool("isAttack", false);
          

        }
    }
}
