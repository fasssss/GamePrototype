using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace d_sidescroller.Assets.Scripts
{
    public abstract partial class Enemy : CharacterBody2D
    {
        public abstract float Attack();
    }
}
