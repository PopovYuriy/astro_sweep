using System.Collections.Generic;
using System.Linq;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Model;
using Zenject;

namespace Core.GameSystems.StatsSystem
{
    public sealed class CharacterStatsSystem : ITickable, IInitializable
    {
        private List<IStatModel> _stats;

        void IInitializable.Initialize()
        {
            _stats = new List<IStatModel> {new StatModel(StatType.Charge, 100)};
        }

        public IStatModel GetStatModel(StatType type) => _stats.First(s => s.Type == type);

        public void Tick()
        {
            foreach (var statModel in _stats)
                statModel.Tick();
        }
    }
}