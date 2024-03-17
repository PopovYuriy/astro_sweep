using System;
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
                _stats.Add(new StatModel(statData.Id, statData.BaseValue));
        }

        public IStatModel GetStatModel(StatId id)
        {
            var model = _stats.FirstOrDefault(s => s.Id == id);
            if (model == null)
                throw new ArgumentException($"Model with id {id} has not found");
            
            return model;
        }

        public void Tick()
        {
            foreach (var statModel in _stats)
                statModel.Tick();
        }
    }
}