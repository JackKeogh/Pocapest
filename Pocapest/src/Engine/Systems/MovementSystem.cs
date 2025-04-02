using Microsoft.Xna.Framework;
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
	public class MovementSystem : EntityUpdateSystem
	{
		private ComponentMapper<PositionComponent> positionMapper;
		private ComponentMapper<VelocityComponent> velocityMapper;
		private ComponentMapper<MovementComponent> targetPositionMapper;

		public MovementSystem() :
			base(Aspect.All(typeof(PositionComponent), typeof(VelocityComponent), typeof(MovementComponent)))
		{
		}

		public override void Initialize(IComponentMapperService mapperService)
		{
			this.positionMapper = mapperService.GetMapper<PositionComponent>();
			this.velocityMapper = mapperService.GetMapper<VelocityComponent>();
			this.targetPositionMapper = mapperService.GetMapper<MovementComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var entity in this.ActiveEntities)
			{
				var position = this.positionMapper.Get(entity);
				var velocity = this.velocityMapper.Get(entity);
				var target = this.targetPositionMapper.Get(entity);

				if (!target.CanMove)
				{
					var deltaX = velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds * Constants.TileSize;
					var deltaY = velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds * Constants.TileSize;

					var newPosition = new Vector2(position.X + deltaX, position.Y + deltaY);

					position.X = newPosition.X;
					position.Y = newPosition.Y;

					if (Math.Abs(position.X - target.X) < 0.1f &&
						Math.Abs(position.Y - target.Y) < 0.1f)
					{
						position.X = target.X;
						position.Y = target.Y;
						velocity.X = 0;
						velocity.Y = 0;
						target.CanMove = true;
					}
				}
			}
		}
	}
}
