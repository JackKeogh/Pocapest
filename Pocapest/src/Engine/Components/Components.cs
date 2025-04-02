using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ECS;
using Pocapest.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Engine.Components
{
	public class PositionComponent
	{
		public float X { get; set; }
		public float Y { get; set; }
	}

	public class MovementComponent
	{
		public float X { get; set; }
		public float Y { get; set; }
		public bool CanMove { get; set; } = true;
	}

	public class SpriteComponent
	{
		public Texture2D Texture { get; set; }
	}

	public class VelocityComponent
	{
		public float X { get; set; }
		public float Y { get; set; }
	}

	public class ColliderComponent
	{
		public Rectangle Collider { get; set; }
		public ColliderType ColliderType { get; set; }
	}

	public class InventoryComponent
	{
		public List<Item> Items { get; set; } = new List<Item>();
	}

	public class PocapestsComponent
	{
		public List<Entity> Pocapests { get; set; } = new List<Entity>();
	}

	public class StatComponent
	{
		public int Health { get; set; }
		public int Attack { get; set; }
		public int Defense { get; set; }
		public int SpecialAttack { get; set; }
		public int SpecialDefense { get; set; }
		public int Speed { get; set; }
	}

	public class AiComponent
	{
		public string Script { get; set; }
	}

	public class DialogueComponent
	{
		public string Dialogue { get; set; }
	}

	public class DoorComponent
	{
		public Vector2 Position { get; set; }
		public bool IsOpen { get; set; }
	}
}
