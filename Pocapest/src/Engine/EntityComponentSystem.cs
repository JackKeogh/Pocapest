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

		public EntityComponentSystem(GraphicsDevice graphicsDevice, OrthographicCamera camera)
		{
			entityFactory = new EntityFactory(graphicsDevice);

			world = new WorldBuilder()
				.AddSystem(new CameraSystem(camera))
				.AddSystem(new MovementSystem())
				.AddSystem(new RenderingSystem(graphicsDevice, camera))
				.Build();

			entityFactory.CreatePlayerEntity(world);
		}

		public void Update(GameTime gameTime)
		{
			world.Update(gameTime);
		}

		public void Draw(GameTime gameTime)
		{
			world.Draw(gameTime);
		}
	}
}
