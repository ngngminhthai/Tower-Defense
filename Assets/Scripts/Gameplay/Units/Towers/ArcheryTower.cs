﻿using UnityEngine;

namespace Assets.Scripts.Gameplay.Units.Defenders
{
    public class ArcheryTower : Tower
    {
        public override void Create(Vector2 buildPosition, GameObject gameObject)
        {
            GameObject spaw = Instantiate<GameObject>(gameObject, buildPosition, Quaternion.identity);
        }

        private void Start()
        {
            Damage = ConfigurationUtils.ArcheryTowerDamage;
            Cooldown = ConfigurationUtils.ArcheryTowerCoolDown;
        }
    }
}
