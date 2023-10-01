using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using d_sidescroller.Assets.Scripts.Interfaces;
using Godot;

namespace d_sidescroller.Assets.Scripts
{
    public class AttackStrategy : IEnemyStrategy
    {
        private Player _player;
        private ArcherEnemy _self;
        private Timer _timer;
        private Dictionary<Type, PackedScene> _WeaponRes { get; set; }

        public AttackStrategy(Player player, ArcherEnemy self, Dictionary<Type, PackedScene> weaponRes)
        {
            _player = player;
            _self = self;
            _WeaponRes = weaponRes;
            _timer = (Timer)_self.FindChild("ActionCooldown");
            _timer.OneShot = true;
        }

        public void Strategy()
        {
            Vector2 velocity = _self.Velocity;
            velocity.X = 0;
            _self.Velocity = velocity;
            _self.FindChild("ActionCooldown");
            if(_timer.TimeLeft <= 0)
            {
                Attack<Arrow>();
            }
        }

        private float Attack<T>() where T : Weapon
        {
            PackedScene weaponRes = _WeaponRes[typeof(T)];
            if (weaponRes == null)
            {
                throw new Exception("Weapon of given type wasn't found in that enemy");
            }

            Weapon weapon = weaponRes.Instantiate<T>();

            if (weapon is Arrow)
            {
                weapon.Position = _self.Position;
                _self.GetParent().CallDeferred("add_child", weapon);
                var playerRelativePosition = new Vector2(_player.Position.X - _self.Position.X, _player.Position.Y - _self.Position.Y);
                weapon.ApplyImpulse(new Vector2((playerRelativePosition.X), (playerRelativePosition.Y - 300) * 2f), new Vector2(0, -32));
                _timer.Start(weapon.Cooldown);
            }

            return 0;
        }
    }
}
