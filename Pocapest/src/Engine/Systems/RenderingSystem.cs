using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Graphics;
using Pocapest.src.Engine.Components;
using Pocapest.src.Helper;
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
		private ComponentMapper<AnimatedComponent> animationMapper;
		private ComponentMapper<PositionComponent> positionMapper;
		private ComponentMapper<ActiveComponent> activeMapper;

		public RenderingSystem(GraphicsDevice graphicsDevice, OrthographicCamera camera) :
			base(Aspect.All(typeof(PositionComponent))
				.One(typeof(SpriteComponent), typeof(AnimatedComponent)))
		{
			this.spriteBatch = new SpriteBatch(graphicsDevice);
			this.camera = camera;
		}
		public override void Initialize(IComponentMapperService mapperService)
		{
			this.spriteMapper = mapperService.GetMapper<SpriteComponent>();
			this.animationMapper = mapperService.GetMapper<AnimatedComponent>();
			this.positionMapper = mapperService.GetMapper<PositionComponent>();
			this.activeMapper = mapperService.GetMapper<ActiveComponent>();
		}

		public override void Draw(GameTime gameTime)
		{
			spriteBatch.Begin(transformMatrix: camera.GetViewMatrix());

			foreach (var entity in this.ActiveEntities)
			{
				var position = this.positionMapper.Get(entity);
				var active = this.activeMapper.Get(entity);

				if (active.IsActive)
				{
					if (spriteMapper.Has(entity))
					{
						var sprite = this.spriteMapper.Get(entity);
						spriteBatch.Draw(sprite.Texture, new Rectangle((int)position.X, (int)position.Y, Constants.TileSize, Constants.TileSize), sprite.Source, Color.White);
					}
					else if (animationMapper.Has(entity))
					{
						var animation = this.animationMapper.Get(entity);
						animation.AnimatedSprite.Draw(spriteBatch, new Vector2(position.X, position.Y - 32), 0, new Vector2(Constants.Scale));
					}
				}
			}

			spriteBatch.End();
		}		
	}
}
