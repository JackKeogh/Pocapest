using Microsoft.Xna.Framework;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Pocapest.src.Engine.Components;
using Pocapest.src.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Engine.Systems
{
	public class CollisionSystem : EntityUpdateSystem
	{
		private ComponentMapper<ColliderComponent> collisionMapper;
		private ComponentMapper<MovementComponent> movementMapper;
		private ComponentMapper<PositionComponent> positionMapper;

		public CollisionSystem() :
			base (Aspect.All(typeof(ColliderComponent)).One(typeof(MovementComponent), typeof(TileComponent)))
		{

		}

		public override void Initialize(IComponentMapperService mapperService)
		{
			this.collisionMapper = mapperService.GetMapper<ColliderComponent>();
			this.movementMapper = mapperService.GetMapper<MovementComponent>();
			this.positionMapper = mapperService.GetMapper<PositionComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var entity in this.ActiveEntities)
			{
				// Can the entity move
				if (movementMapper.Has(entity))
				{
					var movement = movementMapper.Get(entity);
					var position = positionMapper.Get(entity);
					var collider = collisionMapper.Get(entity);

					if (movement.CanMove == false)
					{
						movement.CanMove = this.Collision(entity);
						collider.Collider = new Rectangle((int)position.X, (int)position.Y, Constants.TileSize, Constants.TileSize);
					}
				}
			}
		}

		public bool Collision(int current)
		{
			var currentCollider = collisionMapper.Get(current);

			foreach(var entity in this.ActiveEntities)
			{
				if (current != entity)
				{
					var entityCollider = collisionMapper.Get(entity);

					if (currentCollider.Collider.Intersects(entityCollider.Collider)
						&& entityCollider.ColliderType == Models.ColliderType.NotWalkable)
					{
						return true;
					}
				}
			}

			return false;
		}
	}
}
