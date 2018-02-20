using Entitas;


namespace Libraries.btcp.RPG_Core.src.Directors.Combat.ECS.Logic
{
    public class UpdateCombatDirectorAttackSystem : ICleanupSystem
    {
        private Contexts m_contexts;
        private IGroup<GameEntity>  m_completedAttacks;
        private IGroup<GameEntity> m_animListener;

        public UpdateCombatDirectorAttackSystem (Contexts contexts)
        {
            m_contexts = contexts;
            m_completedAttacks = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.CombatDirector,GameMatcher.AttackComplete));

        }

        public void Cleanup()
        {
            foreach (var e in m_completedAttacks.GetEntities())
            {
                e.isAttackComplete = false;
            }
        }
    }
}
