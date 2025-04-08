using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Pocapest.src.Engine.Components;
using System.Diagnostics;

namespace Pocapest.src.Engine.Systems
{
	public class AnimationSystem : EntityUpdateSystem
	{
		private ComponentMapper<AnimatedComponent> animationMapper;

		public AnimationSystem() :
			base(Aspect.All(typeof(AnimatedComponent)))
		{

		}

		public override void Initialize(IComponentMapperService mapperService)
		{
			this.animationMapper = mapperService.GetMapper<AnimatedComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var entity in this.ActiveEntities)
			{
				var animation = this.animationMapper.Get(entity);
				animation.AnimatedSprite.Update(gameTime);

				animation.AnimatedSprite.Controller.Play();
			}
		}
	}
}
