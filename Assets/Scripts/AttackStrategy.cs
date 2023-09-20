using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d_sidescroller.Assets.Scripts.Interfaces;
using Godot;

namespace d_sidescroller.Assets.Scripts
{
    public class AttackStrategy : IArcherEnemyStrategy
    {
        private Player _player;
        private ArcherEnemy _self;

        public AttackStrategy(Player player, ArcherEnemy self)
        {
            _player = player;
            _self = self;
        }

        public void Strategy()
        {
            Vector2 velocity = _self.Velocity;
            velocity.X = 0;
            _self.Velocity = velocity;
        }
    }
}
