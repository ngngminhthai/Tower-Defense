﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Units
{
    public class Harpy : Attacker
    {


        public Animator animator;

        public int level;
        public Harpy(int level) : base(level)
        {
        }
        
        public void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            unityEvents = new Dictionary<EventName, UnityEngine.Events.UnityEvent<int>>();
            unityEvents.Add(EventName.GoldChangeEvent, new GoldChangeEvent());
            EventManager.AddInvoker(EventName.GoldChangeEvent, this);
            AttackRange = ManageInfor.HappyRange;
            SelectedRange = ManageInfor.HarpySelectedRange;
            if ( level == null)
            {
                Level = 0;
            }
            else Level = level;


            CoolDown = 1;
            BaseDamage = ManageInfor.HarpyDamage;
            BaseHitPoints = ManageInfor.HappyBaseHitPoints;
            BaseSpeed = 15;

            Initialize(ManageInfor.HarpyDamagePer, ManageInfor.HarpyHitpointsPer, 0.1f);
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
        protected override void Die()
        {


            unityEvents[EventName.GoldChangeEvent].Invoke(1);
            base.Die();
            // Gold.PlusGold(value);



        }

    }
}
