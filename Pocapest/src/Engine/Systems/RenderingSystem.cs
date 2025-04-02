using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Pocapest.src.Engine.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Engine.Systems
{
	public class RenderingSystem : EntityDrawSystem
	{
		private SpriteBatch spriteBatch;
		private OrthographicCamera camera;
		private ComponentMapper<SpriteComponent> spriteMapper;
		private ComponentMapper<PositionComponent> positionMapper;

		public RenderingSystem(GraphicsDevice graphicsDevice, OrthographicCamera camera) :
			base(Aspect.All(typeof(SpriteComponent), typeof(PositionComponent)))
		{
			this.spriteBatch = new SpriteBatch(graphicsDevice);
			this.camera = camera;
		}
		public override void Initialize(IComponentMapperService mapperService)
		{
			this.spriteMapper = mapperService.GetMapper<SpriteComponent>();
			this.positionMapper = mapperService.GetMapper<PositionComponent>();
		}

		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

			foreach (var entity in this.ActiveEntities)
			{
				var sprite = this.spriteMapper.Get(entity);
				var position = this.positionMapper.Get(entity);

				spriteBatch.Draw(sprite.Texture, new Vector2(position.X, position.Y), Color.White);
			}

			spriteBatch.End();
		}		
	}
}
