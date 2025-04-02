using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Pocapest.src.Engine.Components;
using System;

namespace Pocapest.src.Engine.Systems
{
	public class CameraSystem : EntityUpdateSystem
	{
		private OrthographicCamera camera;
		private ComponentMapper<PositionComponent> positionMapper;
		private ComponentMapper<PlayerComponent> playerMapper;

		public CameraSystem(OrthographicCamera camera) :
			base (Aspect.All(typeof(PositionComponent), typeof(PlayerComponent)))
		{
			this.camera = camera;
		}

		public override void Initialize(IComponentMapperService mapperService)
		{
			this.positionMapper = mapperService.GetMapper<PositionComponent>();
			this.playerMapper = mapperService.GetMapper<PlayerComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var entity in this.ActiveEntities)
			{
				if (playerMapper.Has(entity))
				{
					var position = positionMapper.Get(entity);

					camera.Position = new Vector2(position.X, position.Y);
					break;
				}
			}
		}
	}
}
