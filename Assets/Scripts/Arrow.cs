using Godot;
using System;

public partial class Arrow : Weapon
{
	public override float Cooldown { get; set; } = 2000f;
	public override float Damage { get; set; } = 20f;


	public override void Attack()
	{
		
	}
}
