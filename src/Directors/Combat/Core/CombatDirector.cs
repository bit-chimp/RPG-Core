using System;
using System.Collections.Generic;

using UnityEngine;

namespace Libraries.btcp.RPG_Core.src.Directors.Combat.Core
{
    public class CombatDirector
    {
        //TODO : Ensure that when this entity is removed that this director is also destroyed
        private readonly GameEntity m_entity;


        private List<IAttack> m_attacks;
        private IAttack m_currentAttack;

        public CombatDirector(GameEntity entity)
        {
            m_entity = entity;
            Init();
        }

        private void Init()
        {
            m_attacks = new List<IAttack>();
        }


        public void AddAttack(IAttack attack)
        {
            m_attacks.Add(attack);
            attack.OnCompleteEvent += OnCompleteAttack;
            attack.OnFailedEvent += OnFailedAttack;
        }

        private void OnCompleteAttack(object sender, EventArgs e)
        {
            FinishAttack();
        }

        private void OnFailedAttack(object sender, EventArgs e)
        {
            FinishAttack();
        }

        public void RemoveAttack(IAttack attack)
        {
            m_attacks.Remove(attack);
        }

        public bool DoAttack()
        {
            var atk = GetSuitableAttack();

            if (atk == null)
            {
                return false;
            }

            m_currentAttack = atk;
            m_currentAttack.OnPrepare();
            m_entity.isAttacking = true;
            return true;
        }

        public IAttack GetSuitableAttack()
        {
            List<IAttack> suitableAttacks = new List<IAttack>();

            foreach (var atk in m_attacks)
            {
                if (atk.CanUse())
                {
                    suitableAttacks.Add(atk);
                }
            }

            if (suitableAttacks.Count == 0)
            {
                Debug.Log("No suitable attacks found");
                return null;
            }

            //TODO : Check if matches request parameter, Mayb I want a casting attack or healing attack
            //TODO : Add Priority to attacks?
            IAttack suitableAtk = suitableAttacks[0];
            return suitableAtk;
        }

        public void Update()
        {
            UpdateAttacks();
            UpdateCurrentAttack();
        }

        private void UpdateAttacks()
        {
            foreach (var atk in m_attacks)
            {
                atk.OnUpdate();
            }
        }

        public void OnAnimationEvent(AnimationEvent evt)
        {
            if (m_currentAttack != null)
            {
                if (m_entity.isAnimationComplete)
                {
                    m_currentAttack.OnAnimationComplete(evt);
                }
                else
                {
                    m_currentAttack.OnAnimationEvent(evt);
                }
            }
        }


        private void UpdateCurrentAttack()
        {
            if (m_currentAttack != null)
            {
                m_currentAttack.OnUse();
            }
        }

        private void FinishAttack()
        {
            if (m_currentAttack != null)
            {
                m_currentAttack.OnFinish();
                m_currentAttack = null;
            }

            m_entity.isAttacking = false;
            m_entity.isAttackComplete = true;
        }
    }
}