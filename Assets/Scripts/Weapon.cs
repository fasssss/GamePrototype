using Godot;
using System;

public abstract partial class Weapon : Node
{
	public abstract float Cooldown { get; set; }
	public abstract float Damage { get; set; }

	public abstract void Attack();
}
