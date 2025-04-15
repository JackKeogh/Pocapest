using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using Pocapest.src.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Engine.GameMechanics
{
	public class Area
	{
		public Rectangle Bounds { get; set; }
		private HashSet<Entity> Entities = new HashSet<Entity>();

		public Area()
		{
			this.Bounds = new Rectangle(0,0,0,0);
		}

		public bool Contains(Rectangle position)
		{
			return this.Bounds.Contains(position);
		}

		public void TrackEntity(Entity entity)
		{
			this.Entities.Add(entity);
		}

		public void DeactivateEntities()
		{
			this.UpdateActiveComponent(false);
		}

		public void ActivateEntities()
		{
			this.UpdateActiveComponent(true);
		}

		private void UpdateActiveComponent(bool value)
		{
			foreach (var entity in this.Entities)
			{
				if (entity.Has<ActiveComponent>())
				{
					entity.Get<ActiveComponent>().IsActive = value;
				}
			}
		}
	}
}
