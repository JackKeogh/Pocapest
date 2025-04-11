using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using Pocapest.src.Engine.Components;
using Pocapest.src.Helper;

namespace Pocapest.src.Engine.Commands
{
	public class MoveUpCommand : IInputCommand
	{
		public void Execute(Entity entity) 
		{
			var position = entity.Get<PositionComponent>();
			var velocity = entity.Get<VelocityComponent>();
			var movement = entity.Get<MovementComponent>();
			var collider = entity.Get<ColliderComponent>();
			var animator = entity.Get<AnimatedComponent>();

			// Is the entity already moving
			if (!movement.CanMove)
			{
				return;
			}

			// Get Current and Movement directions
			var currentDir = movement.Direction;
			var movementDir = Direction.Up;

			if (currentDir == movementDir)
			{
				// Set target position and disable ability to move
				movement.X = position.X;
				movement.Y = position.Y - Constants.TileSize;
				movement.CanMove = false;

				// Set collider position
				collider.Collider = new Rectangle((int)movement.X, (int)movement.Y, Constants.TileSize, Constants.TileSize);

				// Set velocity based on direction
				velocity.X = 0;
				velocity.Y = -1;

				// Set animation
				animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.WalkUp]);
			}
            else
            {
				// Update the current direction
				movement.Direction = Direction.Up;
				animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.IdleUp]);
			}
        }
	}

	public class MoveDownCommand : IInputCommand
	{
		public void Execute(Entity entity)
		{
			var position = entity.Get<PositionComponent>();
			var velocity = entity.Get<VelocityComponent>();
			var movement = entity.Get<MovementComponent>();
			var collider = entity.Get<ColliderComponent>();
			var animator = entity.Get<AnimatedComponent>();

			// Is the entity already moving
			if (!movement.CanMove)
			{
				return;
			}

			// Get Current and Movement directions
			var currentDir = movement.Direction;
			var movementDir = Direction.Down;

			if (currentDir == movementDir)
			{
				// Set target position and disable ability to move
				movement.X = position.X;
				movement.Y = position.Y + Constants.TileSize;
				movement.CanMove = false;

				// Set collider position
				collider.Collider = new Rectangle((int)movement.X, (int)movement.Y, Constants.TileSize, Constants.TileSize);

				// Set velocity based on direction
				velocity.X = 0;
				velocity.Y = 1;

				// Set Animation
				animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.WalkDown]);
			}
			else
			{
				movement.Direction = Direction.Down;
				animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.IdleDown]);
			}
		}
	}

	public class MoveRightCommand : IInputCommand
	{
		public void Execute(Entity entity)
		{
			var position = entity.Get<PositionComponent>();
			var velocity = entity.Get<VelocityComponent>();
			var movement = entity.Get<MovementComponent>();
			var collider = entity.Get<ColliderComponent>();
			var animator = entity.Get<AnimatedComponent>();

			// Is the entity already moving
			if (!movement.CanMove)
			{
				return;
			}

			// Get Current and Movement directions
			var currentDir = movement.Direction;
			var movementDir = Direction.Right;

			if (currentDir == movementDir)
			{
				// Set target position and disable ability to move
				movement.X = position.X + Constants.TileSize;
				movement.Y = position.Y;
				movement.CanMove = false;

				// Set collider position
				collider.Collider = new Rectangle((int)movement.X, (int)movement.Y, Constants.TileSize, Constants.TileSize);

				// Set velocity based on direction
				velocity.X = 1;
				velocity.Y = 0;

				// Set animation
				animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.WalkRight]);
			}
			else
			{
				movement.Direction = Direction.Right;
				animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.IdleRight]);
			}
		}
	}

	public class MoveLeftCommand : IInputCommand
	{
		public void Execute(Entity entity)
		{
			var position = entity.Get<PositionComponent>();
			var velocity = entity.Get<VelocityComponent>();
			var movement = entity.Get<MovementComponent>();
			var collider = entity.Get<ColliderComponent>();
			var animator = entity.Get<AnimatedComponent>();

			// Is the entity already moving
			if (!movement.CanMove)
			{
				return;
			}

			// Get Current and Movement directions
			var currentDir = movement.Direction;
			var movementDir = Direction.Left;

			if (currentDir == movementDir)
			{
				// Set target position and disable ability to move
				movement.X = position.X - Constants.TileSize;
				movement.Y = position.Y;
				movement.CanMove = false;

				// Set collider position
				collider.Collider = new Rectangle((int)movement.X, (int)movement.Y, Constants.TileSize, Constants.TileSize);

				// Set velocity based on direction
				velocity.X = -1;
				velocity.Y = 0;

				// Set animation
				animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.WalkLeft]);
			}
			else
			{
				movement.Direction = Direction.Left;
				animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.IdleLeft]);
			}
		}
	}
}
