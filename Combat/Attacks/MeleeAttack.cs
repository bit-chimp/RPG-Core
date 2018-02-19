using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Mine.Combat.Attacks
{
    public class MeleeAttack : BaseAttack
    {
        private int[] m_attackFrames;

        public MeleeAttack(GameEntity e, string animId, int[] attackFrames) : base(e)
        {
            var events = new List<AnimationEvent>();

            foreach (var atkFrame in attackFrames)
            {
                var evt = new AnimationEvent();
                evt.functionName = "OnAnimation";
                evt.stringParameter = "Attack";
                evt.floatParameter = 99692589f;
                evt.time = atkFrame;
                events.Add(evt);
            }

            /* TODO :
             
                 var evts = CreateEvents("Attack", attackFrames, OnAttack); Call function on hit
             */

            GenerateClip(animId, events);
        }


        public override bool CanUse()
        {
            var currentCooldown = m_entity.cooldown.value;
            return currentCooldown <= 0;
        }

        public override string GetId()
        {
            return "melee_attacks";
        }

        public override string GetAttackType()
        {
            return "offensive";
        }


        public override void OnPrepare()
        {
            PlayAnim(true, false);
        }


        public override void OnUse()
        {
            //Reset Cooldown
            var currentCooldown = m_entity.cooldown.value;
            var totalCooldown = m_entity.cooldown.total;
            currentCooldown = totalCooldown;
            m_entity.ReplaceCooldown(currentCooldown, totalCooldown);

        }

        public override void OnUpdate()
        {
            var currentCooldown = m_entity.cooldown.value;
            var totalCooldown = m_entity.cooldown.total;

            if (currentCooldown > 0)
            {
                currentCooldown -= Time.deltaTime;
            }

            m_entity.ReplaceCooldown(currentCooldown, totalCooldown);
        }


        public override void OnFinish()
        {
        }

        public override void OnAnimationEvent(AnimationEvent evt)
        {
            //TODO : Check if collided with target before applying damage
            if (evt.stringParameter == "Attack")
            {
                DealDamage(m_entity.damage.value);
            }
        }


        public override void OnAnimationComplete(AnimationEvent evt)
        {
            OnComplete();
        }
    }
}