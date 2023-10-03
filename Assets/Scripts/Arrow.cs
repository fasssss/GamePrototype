using Godot;
using System;

public partial class Arrow : Weapon
{
	public override float Damage { get; set; } = 20f;

	public Arrow()
	{
		this.BodyEntered += DealDamage;
	}

	private void DealDamage(Node body)
	{
		if (body.GetType() == typeof(Player))
		{
			var player = (Player)body;
			player.Hp -= Damage;
			GD.PrintRich("HP Remains " + player.Hp.ToString());
		}

		this.CallDeferred("free");
	}
}
