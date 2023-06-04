using Assets.Scripts.Gameplay.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : Attacker
{
    // Start is called before the first frame update


 
    public Animator animator;
    public int level;
    public Monitor(int level) : base(level)
    {
        animator.SetBool("isAttack", false);
    }

  
    public void Start()
    {
        unityEvents = new Dictionary<EventName, UnityEngine.Events.UnityEvent<int>>();
        unityEvents.Add(EventName.GoldChangeEvent, new GoldChangeEvent());
        EventManager.AddInvoker(EventName.GoldChangeEvent, this);
        Initialize();
       
    }

    private void Initialize()
    {

        AttackRange = ManageInfor.MonitorRange;
        SelectedRange = ManageInfor.MonitorSelectedRange;
        if (level == null)
        {
            Level = 0;
        }
        else
        {
            Level = level;
        }


        CoolDown = 2;
        BaseDamage = ManageInfor.MonitorDamage;
        BaseHitPoints = ManageInfor.MonitorBaseHitPoints;
        BaseSpeed = 15;

        Initialize(ManageInfor.MonitorDamagePer, ManageInfor.MonitorHitPointPer, 0.1f);
        OnLanding();
    }
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
    public void OnLanding()
    {
        animator.SetBool("isAttack", false);
    }

    protected override void Die()
    {

        int value = Convert.ToInt32(Math.Ceiling(Strength2()));
        unityEvents[EventName.GoldChangeEvent].Invoke(value);
    
        base.Die();
        // Gold.PlusGold(value);



    }
}
