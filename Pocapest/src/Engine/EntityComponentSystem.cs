using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using Pocapest.src.Engine.Systems;

namespace Pocapest.src.Engine
{
	public class EntityComponentSystem
	{
		private World world;
		private EntityFactory entityFactory;
		private InputHandlingSystem inputHandlingSystem;
		private Entity player;

		public EntityComponentSystem(GraphicsDevice graphicsDevice, OrthographicCamera camera)
		{
			this.entityFactory = new EntityFactory(graphicsDevice);
			this.inputHandlingSystem = new InputHandlingSystem();

			this.world = new WorldBuilder()
				.AddSystem(new CameraSystem(camera))
				.AddSystem(new CollisionSystem())
				.AddSystem(new MovementSystem())
				.AddSystem(new AnimationSystem())
				.AddSystem(new RenderingSystem(graphicsDevice, camera))
				.Build();

			this.player = this.entityFactory.CreatePlayerEntity(this.world);
			this.entityFactory.CreateTileEntity(this.world, 32, 32);
			this.entityFactory.CreateTileEntity(this.world, 64, 32, Models.ColliderType.Walkable);
		}

		public void Update(GameTime gameTime)
		{
			this.inputHandlingSystem.HandleInput(player);

			this.world.Update(gameTime);
		}

		public void Draw(GameTime gameTime)
		{
			this.world.Draw(gameTime);
		}
	}
}
