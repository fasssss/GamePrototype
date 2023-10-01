using Godot;
using System;

public abstract partial class Weapon : RigidBody2D
{
	public abstract float Cooldown { get; set; }
	public abstract float Damage { get; set; }

	public abstract void DealDamage();
}
