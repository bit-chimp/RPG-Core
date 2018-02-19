using System;
using System.Collections.Generic;
using ECS.Combat.Damage;
using UnityEngine;

namespace Mine.Combat
{
    public abstract class BaseAttack : IAttack
    {
        public abstract string GetId();
        public abstract string GetAttackType();

        public abstract void OnAnimationEvent(AnimationEvent evt);
        public abstract void OnAnimationComplete(AnimationEvent evt);
        public abstract void OnPrepare();
        public abstract void OnUse();
        public abstract void OnUpdate();
        public abstract void OnFinish();

        public event EventHandler OnCompleteEvent;
        public event EventHandler OnFailedEvent;

        protected bool m_requiresTarget = false;
        protected string m_animId;
        protected GameEntity m_entity;

        public BaseAttack(GameEntity e)
        {
            m_entity = e;
        }

        public virtual bool CanUse()
        {
            if (m_requiresTarget)
            {
                return m_entity.hasTarget;
            }

            return true;
        }

        protected void GenerateClip(string animId, List<AnimationEvent> events)
        {
            m_animId = animId;
            var director = m_entity.animationDirector.director;
            director.AddAnimation(animId, director.CreateAnimationClip(animId, events));
        }
        
//
//        private FrameListener m_frameListener;
//        protected void AddFrameListener(int frame, string id)
//        {
//            m_clip.AddFrameListener(frame, id, this);
//        }
//
//        protected void AddFrameListeners(IEnumerable<int> frames, string id)
//        {
//            foreach (var frame in frames)
//            {
//                AddFrameListener(frame, id);
//            }
//        }



        protected void PlayAnim(bool playNow, bool loop)
        {
            var animDirector = m_entity.animationDirector.director;
            animDirector.PlayAnim(m_animId, playNow, loop);
        }


        protected void OnComplete()
        {
            if (OnCompleteEvent != null)
            {
                OnCompleteEvent(this, null);
            }
        }

        protected void DealDamage(float amt)
        {
            var target = Contexts.sharedInstance.game.GetEntityWithId(m_entity.target.value);
            DamageHelpers.DealDamage(target, m_entity, amt);
        }

        protected void OnFailed()
        {
            if (OnFailedEvent != null)
            {
                OnFailedEvent(this, null);
            }
        }


    }
}