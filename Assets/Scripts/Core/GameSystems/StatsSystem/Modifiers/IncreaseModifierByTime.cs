using UnityEngine;

namespace Core.GameSystems.StatsSystem.Modifiers
{
    public sealed class IncreaseModifierByTime : BaseModifierAbstract
    {
        public IncreaseModifierByTime(float value) : base(value) { }

        public override float Apply(float value)
        {
            return value + Value * Time.deltaTime;
        }
    }
}