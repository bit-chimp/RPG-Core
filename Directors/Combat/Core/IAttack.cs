using System;
using UnityEngine;

namespace Mine.Combat
{
    public interface IAttack
    {
        string GetId();
        string GetAttackType();
        
        bool CanUse();
            
        //TODO: Add OnInterrupted
        
        void OnAnimationEvent(AnimationEvent evt);
        void OnAnimationComplete(AnimationEvent evt);

        void OnPrepare();
        void OnUse();
        void OnUpdate();
        void OnFinish();
        
        event EventHandler OnCompleteEvent;
        event EventHandler OnFailedEvent;
    }
    
}