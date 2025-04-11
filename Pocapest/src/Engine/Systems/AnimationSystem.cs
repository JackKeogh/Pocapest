using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Pocapest.src.Engine.Components;
using Pocapest.src.Helper;
using System.Diagnostics;

namespace Pocapest.src.Engine.Systems
{
	public class AnimationSystem : EntityUpdateSystem
	{
		private ComponentMapper<AnimatedComponent> animationMapper;
		private ComponentMapper<MovementComponent> movementMapper;
		private ComponentMapper<VelocityComponent> velocityMapper;

		public AnimationSystem() :
			base(Aspect.All(typeof(AnimatedComponent)))
		{

		}

		public override void Initialize(IComponentMapperService mapperService)
		{
			this.animationMapper = mapperService.GetMapper<AnimatedComponent>();
			this.movementMapper = mapperService.GetMapper<MovementComponent>();
			this.velocityMapper = mapperService.GetMapper<VelocityComponent>();
		}

		public override void Update(GameTime gameTime)
		{
			foreach (var entity in this.ActiveEntities)
			{
				var animation = this.animationMapper.Get(entity);
				var movement = this.movementMapper.Get(entity);

				this.UseIdle(movement, animation, gameTime);

				animation.AnimatedSprite.Update(gameTime);
			}
		}

		public void UseIdle(MovementComponent movement, AnimatedComponent animator, GameTime gameTime)
		{
			if (movement.CanMove)
			{
				var currentFrame = animator.AnimatedSprite.Controller.CurrentFrame;
				var currentAnim = animator.AnimatedSprite.CurrentAnimation;

				if (currentAnim.Contains("idle"))
				{
					return;
				}

				if (currentAnim == Constants.Animation.WalkDown && (currentFrame == 0 || currentFrame == 2))
				{
					animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.IdleDown]);
				}
				else if (currentAnim == Constants.Animation.WalkUp && (currentFrame == 3 || currentFrame == 5))
				{
					animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.WalkUp]);
				}
				else if (currentAnim == Constants.Animation.WalkLeft && (currentFrame == 6 || currentFrame == 8))
				{
					animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.IdleLeft]);
				}
				else if (currentAnim == Constants.Animation.WalkRight && (currentFrame == 9 || currentFrame == 11))
				{
					animator.AnimatedSprite.SetAnimation(animator.Animations[Constants.Animation.IdleRight]);
				}
			}
		}
	}
}
