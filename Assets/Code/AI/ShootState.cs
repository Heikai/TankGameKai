using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TankGame.AI
{
    public class ShootState : AIStateBase
    {

        public float SqrShootingDistance
        {
            get { return Owner.ShootingDistance * Owner.ShootingDistance; }
        }

        public ShootState (EnemyUnit owner)
            : base(owner, AIStateType.Shoot)
        {
            AddTransition(AIStateType.FollowTarget);
            AddTransition(AIStateType.Patrol);
        }

        public override void Update()
        {
            Owner.Mover.Turn(Owner.Target.transform.position);
            Owner.Weapon.Shoot();

            Vector3 toPlayerVector = Owner.transform.position - Owner.Target.transform.position;
            float sqrDistanceToPlayer = toPlayerVector.sqrMagnitude;

            if (sqrDistanceToPlayer > SqrShootingDistance)
            {
                Owner.PerformTransition(AIStateType.FollowTarget);
            }

            if (Owner.Target.Health.CurrentHealth <= 0)
            {
                Owner.PerformTransition(AIStateType.Patrol);
            }
        }
    }
}