using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d_sidescroller.Assets.Scripts
{
    public class WeaponInitializer
    {
        private Dictionary<Type, PackedScene> _WeaponRes { get; set; } = new Dictionary<Type, PackedScene> {
            { typeof(Arrow), ResourceLoader.Load<PackedScene>("res://Assets/Objects/arrow.res") }
        };

        protected float Attack<T>(Player player, Enemy owner) where T : Weapon
        {
            PackedScene weaponRes = _WeaponRes[typeof(T)];
            if (weaponRes == null)
            {
                throw new Exception("Weapon of given type wasn't found in that enemy");
            }

            Weapon weapon = weaponRes.Instantiate<T>();

            if (weapon is Arrow)
            {
                weapon.Position = owner.Position;
                owner.GetParent().CallDeferred("add_child", weapon);
                var playerRelativePosition = new Vector2(player.Position.X - owner.Position.X, player.Position.Y - owner.Position.Y);
                weapon.ApplyImpulse(new Vector2((playerRelativePosition.X), (playerRelativePosition.Y - 300) * 2f), new Vector2(0, -32));
            }

            return 0;
        }
    }
}
