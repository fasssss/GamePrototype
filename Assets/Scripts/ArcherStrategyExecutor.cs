using d_sidescroller.Assets.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d_sidescroller.Assets.Scripts
{
    public class ArcherStrategyExecutor
    {
        private IEnemyStrategy _strategy;

        public void SetStrategy(IEnemyStrategy strategy)
        {
            _strategy = strategy;
        }

        public void Execute() 
        {
            _strategy.Strategy();
        }
    }
}
