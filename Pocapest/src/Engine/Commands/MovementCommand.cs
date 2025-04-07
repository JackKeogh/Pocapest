using MonoGame.Extended.ECS;
using Pocapest.src.Engine.Components;
using Pocapest.src.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Engine.Commands
{
	public class MoveUpCommand : IInputCommand
	{
		public void Execute(Entity entity) 
		{
			var position = entity.Get<PositionComponent>();
			var velocity = entity.Get<VelocityComponent>();
			var movement = entity.Get<MovementComponent>();

			// Is the entity already moving
			if (!movement.CanMove)
			{
				return;
			}

			// Set target position and disable ability to move
			movement.X = position.X;
			movement.Y = position.Y - Constants.TileSize;
			movement.CanMove = false;

			// Set velocity based on direction
			velocity.X = 0;
			velocity.Y = -1;
		}
	}

	public class MoveDownCommand : IInputCommand
	{
		public void Execute(Entity entity)
		{
			var position = entity.Get<PositionComponent>();
			var velocity = entity.Get<VelocityComponent>();
			var movement = entity.Get<MovementComponent>();

			// Is the entity already moving
			if (!movement.CanMove)
			{
				return;
			}

			// Set target position and disable ability to move
			movement.X = position.X;
			movement.Y = position.Y + Constants.TileSize;
			movement.CanMove = false;

			// Set velocity based on direction
			velocity.X = 0;
			velocity.Y = 1;
		}
	}

	public class MoveRightCommand : IInputCommand
	{
		public void Execute(Entity entity)
		{
			var position = entity.Get<PositionComponent>();
			var velocity = entity.Get<VelocityComponent>();
			var movement = entity.Get<MovementComponent>();

			// Is the entity already moving
			if (!movement.CanMove)
			{
				return;
			}

			// Set target position and disable ability to move
			movement.X = position.X + Constants.TileSize;
			movement.Y = position.Y;
			movement.CanMove = false;

			// Set velocity based on direction
			velocity.X = 1;
			velocity.Y = 0;
		}
	}

	public class MoveLeftCommand : IInputCommand
	{
		public void Execute(Entity entity)
		{
			var position = entity.Get<PositionComponent>();
			var velocity = entity.Get<VelocityComponent>();
			var movement = entity.Get<MovementComponent>();

			// Is the entity already moving
			if (!movement.CanMove)
			{
				return;
			}

			// Set target position and disable ability to move
			movement.X = position.X - Constants.TileSize;
			movement.Y = position.Y;
			movement.CanMove = false;

			// Set velocity based on direction
			velocity.X = -1;
			velocity.Y = 0;
		}
	}
}
