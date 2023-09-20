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
        private IArcherEnemyStrategy _strategy;

        public void SetStrategy(IArcherEnemyStrategy strategy)
        {
            _strategy = strategy;
        }

        public void Execute() 
        {
            _strategy.Strategy();
        }
    }
}
