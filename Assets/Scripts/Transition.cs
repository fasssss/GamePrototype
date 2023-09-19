using Godot;
using System;

public partial class Transition : Control
{
	private ColorRect _colorRect;
	private AnimationPlayer _animationPlayer;
	
	public override void _Ready()
	{
		_colorRect = (ColorRect)FindChild("ColorRect");
		_colorRect.Visible = false;
		_animationPlayer = (AnimationPlayer)FindChild("AnimationPlayer");

	}

	public override void _Process(double delta)
	{
	}

	public void FadeIn() 
	{
		_animationPlayer.Queue("fadeIn");
	}

	public void FadeOut()
	{
		_animationPlayer.Queue("fadeOut");
	}
}
