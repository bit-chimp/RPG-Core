
using Libraries.btcp.RPG_Core.src.Directors.Combat.ECS.Logic;

namespace Libraries.btcp.RPG_Core.src.Directors.Combat.ECS
{
    public sealed class CombatDirectorSystems : Feature
    {
        public CombatDirectorSystems(Contexts contexts) : base("Combat Director Systems")
        {
            Add(new UpdateCombatDirectorSystem(contexts));
            Add(new UpdateCombatDirectorFrameSystem(contexts));
            Add(new UpdateCombatDirectorAttackSystem(contexts));

        }
    }
}