using UnityEngine;

namespace Libraries.btcp.RPG_Core.src.Directors.Combat.Core
{
    public interface IFrameObserver
    {
        void OnAnimationEvent(AnimationEvent evt);
    }
}