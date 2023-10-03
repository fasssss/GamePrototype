using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d_sidescroller.Assets.Scripts.Interfaces;
using Godot;
using Godot.DependencyInjection.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace d_sidescroller.Assets.Scripts
{
    public class ArcherAttackStrategy : WeaponInitializer, IEnemyStrategy 
    {
        private Player _player;
        private ArcherEnemy _self;
        private Timer _timer;

        public ArcherAttackStrategy(Player player, ArcherEnemy self)
        {
            _player = player;
            _self = self;
            _timer = (Timer)_self.FindChild("ActionCooldown");
            _timer.OneShot = true;
        }

        public void Strategy()
        {
            Vector2 velocity = _self.Velocity;
            velocity.X = 0;
            _self.Velocity = velocity;
            if(_timer.TimeLeft <= 0)
            {
                Attack<Arrow>(_player, _self);
                _timer.Start(_self.BaseActionSpeed * 2);
            }
        }
    }
}
