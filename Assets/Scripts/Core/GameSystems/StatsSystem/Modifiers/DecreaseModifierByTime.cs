using UnityEngine;

namespace Core.GameSystems.StatsSystem.Modifiers
{
    public sealed class DecreaseModifierByTime : BaseModifierAbstract
    {
        public DecreaseModifierByTime(float value) : base(value) { }

        public override float Apply(float value)
        {
            return value - Value * Time.deltaTime;
        }
    }
}