using Entitas;


namespace Libraries.btcp.RPG_Core.src.Directors.Combat.ECS.Logic
{
    public class UpdateCombatDirectorSystem : IExecuteSystem
    {
        private Contexts m_contexts;
        private IGroup<GameEntity> m_group;

        public UpdateCombatDirectorSystem (Contexts contexts)
        {
            m_contexts = contexts;
            m_group = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.CombatDirector, GameMatcher.AnimationDirector).NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (var e in m_group.GetEntities())
            {
                e.combatDirector.director.Update();
            }
        }
    }
}
