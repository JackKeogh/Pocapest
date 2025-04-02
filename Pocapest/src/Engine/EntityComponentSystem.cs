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

		public EntityComponentSystem(GraphicsDevice graphicsDevice, OrthographicCamera camera)
		{
			world = new WorldBuilder()
				.AddSystem(new RenderingSystem(graphicsDevice, camera))
				.Build();
		}

		public void Update(GameTime gameTime)
		{
			world.Draw(gameTime);
		}

		public void Draw(GameTime gameTime)
		{
			world.Draw(gameTime);
		}
	}
}
