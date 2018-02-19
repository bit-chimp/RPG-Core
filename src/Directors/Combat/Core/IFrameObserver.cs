using Libraries.Patterns.Observer;
using UnityEngine;

namespace Mine.Combat
{
    public interface IFrameObserver
    {
        void OnAnimationEvent(AnimationEvent evt);
    }
}