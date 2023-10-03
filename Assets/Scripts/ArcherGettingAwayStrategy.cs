using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d_sidescroller.Assets.Scripts.Interfaces;
using Godot;

namespace d_sidescroller.Assets.Scripts
{
    public class ArcherGettingAwayStrategy : IEnemyStrategy
    {
        private Player _player;
        private ArcherEnemy _self;

        public ArcherGettingAwayStrategy(Player player, ArcherEnemy self)
        {
            _player = player;
            _self = self;
        }

        public void Strategy()
        {
            Vector2 velocity = _self.Velocity;
            velocity.X = (_self.Position.X - _player.Position.X) / Math.Abs(_self.Position.X - _player.Position.X) * _self.Speed;
            _self.Velocity = velocity;
        }
    }
}
