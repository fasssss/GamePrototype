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
        protected bool IsAgressive { get; set; }
        public float Hp { get; set; }
        public float Speed { get; set; }
        public float BaseActionSpeed { get; set; }
    }
}
