using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace unity.Helpers.Animations
{
    public class AnimationDirector
    {
        //TODO : Ensure that when this entity is removed that this director is also destroyed
        private readonly GameEntity m_entity;
        private Dictionary<string, AnimationClip> m_animationStore;

        public AnimationDirector(GameEntity entity)
        {
            m_entity = entity;
            Init();
        }

        private void Init()
        {
            m_animationStore = new Dictionary<string, AnimationClip>();
        }

        //TODO : Create store of enums for all possible actions?
        public enum Action
        {
            Mining,
            Death,
            Attack
        }


        public void AddAnimation(string name, AnimationClip clip)
        {
            if (clip == null) return;
            m_animationStore.Add(name, clip);
        }


        public void PlayAnim(string animId, bool playNow, bool loopAnimation = false)
        {
            if (!ValidateEntity()) return;
            if (HasClip(animId) == false) return;

            var go = m_entity.view.gameObject;

            var clip = GetAnimationClip(animId);
            m_entity.isNextAnimationPatient = !playNow;
            m_entity.isAnimationLoop = loopAnimation;
            m_entity.ReplaceAnimation(clip);
        }
//
//        public void PlayAnimForAction(Action action, bool playNow, bool loopAnimation)
//        {
//            var animClip = GetAnimationForAction(action);
//            PlayAnim(animClip, playNow, loopAnimation);
//        }

//        public AnimationClip GetAnimationForAction(Action action)
//        {
//            var store = SafeGetStore(action);
//
//            Assert.IsTrue(store.Count  > 0,
//                FormatDebug("No animations found for this action [" + Enum.GetName(typeof(Action), action)+"]"));
//
//            /*TODO : Figure out approach for animation selection
//             
//                 If I have a pickaxe, play mine_pickaxe_anim
//                 If I have a blunt weapon, play mine_bluntweapon_anim
//             */
//
//            return store[0];
//        }


        private bool ValidateEntity()
        {

            if (m_entity.isDead)
            {
                Debug.Log("Entity is dead");
                return false;
            }
            
            if (m_entity.hasView == false)
            {
                Debug.LogAssertion(FormatDebug("Entity does not have a View Component"));
                return false;
            }

            return true;
        }

        private static string FormatDebug(string msg)
        {
            return "[AnimationDirector] " + msg;
        }

        public AnimationClip CreateAnimationClip(string animId, List<AnimationEvent> events)
        {
            var localClip = Resources.Load<AnimationClip>("Animations/Clips/" + animId);

            AnimationClip clip;

            if (localClip != null)
            {
                clip = GameObject.Instantiate(localClip);
            }
            else
            {
                return null;
            }

            //Create Animation Complete Event
            AnimationEvent onComplete = new AnimationEvent();
            onComplete.functionName = "OnAnimComplete";
            onComplete.time = clip.length * clip.frameRate;
            events.Add(onComplete);

            foreach (var evt in events)
            {
                evt.time /= clip.frameRate;
                clip.AddEvent(evt);
            }

            return clip;
        }

        private AnimationClip GetAnimationClip(string clipName)
        {
            Assert.IsTrue(m_animationStore.ContainsKey(clipName), "Animation Clip not found " + clipName);
            return m_animationStore[clipName];
        }

        public bool HasClip(string clipName)
        {
            return m_animationStore.ContainsKey(clipName);
        }
    }
}