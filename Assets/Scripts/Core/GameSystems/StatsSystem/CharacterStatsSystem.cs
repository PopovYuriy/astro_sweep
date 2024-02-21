using System.Collections.Generic;
using System.Linq;
using Core.GameSystems.StatsSystem.Data;
using Core.GameSystems.StatsSystem.Enum;
using Core.GameSystems.StatsSystem.Model;
using Zenject;

namespace Core.GameSystems.StatsSystem
{
    public sealed class CharacterStatsSystem : ITickable
    {
        private List<IStatModel> _stats;

        [Inject]
        private void Construct(StatsConfig config)
        {
            _stats = new List<IStatModel>(config.Stats.Length);
            
            foreach (var statData in config.Stats)
                _stats.Add(new StatModel(statData.Type, statData.Value, statData.MaxValue));
        }

        public IStatModel GetStatModel(StatType type) => _stats.First(s => s.Type == type);

        public void Tick()
        {
            foreach (var statModel in _stats)
                statModel.Tick();
        }
    }
}