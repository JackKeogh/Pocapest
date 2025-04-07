using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using Pocapest.src.Engine.Components;
using Pocapest.src.Models;

namespace Pocapest.src.Engine.Systems
{
	public class EntityFactory
	{
		private GraphicsDevice graphicsDevice;

		public EntityFactory (GraphicsDevice graphicsDevice)
		{
			this.graphicsDevice = graphicsDevice;
		}

		public Entity CreatePlayerEntity(World world)
		{
			var playerEntity = world.CreateEntity();

			// Attach player component
			var playerComponent = new PlayerComponent();
			playerEntity.Attach(playerComponent);

			// Set initial position
			var positionComponent = new PositionComponent() { X = 0, Y = 0 };
			playerEntity.Attach(positionComponent);

			// Attach player sprite
			var spriteComponent = new SpriteComponent() { Texture = TextureHandlingSystem.Instance().GetTexture("test") };
			playerEntity.Attach(spriteComponent);

			// Initialize velocity component
			var velocityComponent = new VelocityComponent() { X = 0, Y= 0 };
			playerEntity.Attach(velocityComponent);

			// Initialize collider component
			var colliderComponent = new ColliderComponent() { Collider = new Rectangle(0, 0, 32, 32), ColliderType = ColliderType.NotWalkable };
			playerEntity.Attach(colliderComponent);

			// Initialize movement component
			var movementComponent = new MovementComponent() { CanMove = true, X = 0, Y = 0 };
			playerEntity.Attach(movementComponent);

			return playerEntity;
		}
	}
}
