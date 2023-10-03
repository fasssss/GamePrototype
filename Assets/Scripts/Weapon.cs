using Godot;
using System;

public abstract partial class Weapon : RigidBody2D
{
    public abstract float Damage { get; set; }
}
