using d_sidescroller.Assets.Scripts.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d_sidescroller.Assets.Scripts
{
    public class ArcherGettingCloserStrategy : IEnemyStrategy
    {
        private Player _player;
        private ArcherEnemy _self;

        public ArcherGettingCloserStrategy(Player player, ArcherEnemy self)
        {
            _player = player;
            _self = self;
        }

        public void Strategy()
        {
            Vector2 velocity = _self.Velocity;
            velocity.X = (_player.Position.X - _self.Position.X) / Math.Abs(_player.Position.X - _self.Position.X) * _self.Speed;
            _self.Velocity = velocity;
        }
    }
}
