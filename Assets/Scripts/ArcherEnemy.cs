using d_sidescroller.Assets.Scripts;
using Godot;
using System;

public partial class ArcherEnemy : Enemy
{
	private float _Hp { get; set; }

	public override void _Ready()
	{
		_Hp = 100f;
	}

	public override void _Process(double delta)
	{
	}
	
	public override float Attack() 
	{
		throw new NotImplementedException();
	}
	
	private void Move() 
	{
		
	}
}
