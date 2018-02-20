using System.Collections.Generic;
using Entitas;


namespace Libraries.btcp.RPG_Core.src.Directors.Combat.ECS.Logic
{
    public class UpdateCombatDirectorFrameSystem : ReactiveSystem<GameEntity>
    {
        private Contexts m_contexts;

        public UpdateCombatDirectorFrameSystem (Contexts contexts) : base(contexts.game)
        {
            m_contexts = contexts;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AllOf(GameMatcher.CombatDirector, GameMatcher.AnimationEvent));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAnimationEvent;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach(var e in entities)
            {
                var evt = e.animationEvent.evt;
            
                e.combatDirector.director.OnAnimationEvent(evt);
            }
        }
    }
}
