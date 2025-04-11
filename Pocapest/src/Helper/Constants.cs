using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Helper
{
	public static class Constants
	{
		public static readonly int TileSize = 32;
		public static readonly int Scale = 2;
		
		public class Animation
		{
			public static readonly string WalkDown = "walkDown";
			public static readonly string WalkUp = "walkUp";
			public static readonly string WalkLeft = "walkLeft";
			public static readonly string WalkRight = "walkRight";
			public static readonly string IdleDown = "idleDown";
			public static readonly string IdleUp = "idleUp";
			public static readonly string IdleLeft = "idleLeft";
			public static readonly string IdleRight = "idleRight";
		}
	}
}
