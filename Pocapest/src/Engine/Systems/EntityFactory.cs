using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using Pocapest.src.Engine.Components;
using Pocapest.src.Models;
using Pocapest.src.Helper;
using MonoGame.Extended.Graphics;
using NLua;
using System;
using System.Collections.Generic;

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

			// Attach animated sprite
			var animatedComponent = new AnimatedComponent()
			{
				AnimatedSprite = new AnimatedSprite(this.CreateSpriteSheet("src/Scripts/Animations/PlayerAnimation",
				TextureHandlingSystem.Instance().GetTexture("player"))),
				Animations = new Dictionary<string, string>() { { "wDown", "walkDown" }, { "wUp", "walkUp" }, { "wLeft", "walkLeft" }, { "wRight", "walkRight" },
																{ "iDown", "idleDown" }, { "iUp", "idleUp" }, { "iLeft", "idleLeft" }, { "iRight", "idleRight" } }
			};
			animatedComponent.AnimatedSprite.SetAnimation("walkLeft");
			playerEntity.Attach(animatedComponent);

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

		public Entity CreateTileEntity(World world)
		{
			var entity = world.CreateEntity();

			// Attach position component
			var positionComponent = new PositionComponent() { X = 64, Y = 64 };
			entity.Attach(positionComponent);

			// Attach sprite component
			var spriteComponent = new SpriteComponent()
			{
				Texture = TextureHandlingSystem.Instance().GetTexture("test"),
				Source = new Rectangle(0, 0, Constants.TileSize, Constants.TileSize)
			};
			entity.Attach(spriteComponent);

			// Attach collider component
			var colliderComponent = new ColliderComponent() { ColliderType = ColliderType.NotWalkable, Collider = new Rectangle(64, 64, 32, 32) };
			entity.Attach(colliderComponent);

			// Attach t5ile component
			var tileComponent = new TileComponent();
			entity.Attach(tileComponent);

			return entity;
		}

		private SpriteSheet CreateSpriteSheet(string obj, Texture2D texture)
		{
			Lua lua = new Lua();
			lua.DoFile($"{obj}.lua");

			var atlas = Texture2DAtlas.Create($"Atlas/{obj}", texture, 16, 32, 12, 1, 1);

			var spritesheet = new SpriteSheet(obj, atlas);

			LuaTable animations = (LuaTable)lua["animations"];

			foreach (var animationame in animations.Keys)
			{
				LuaTable animation = (LuaTable)animations[animationame];

				spritesheet.DefineAnimation(animationame.ToString(), builder =>
				{
					builder.IsLooping(bool.Parse(animation["isLooping"].ToString()));
					builder.IsPingPong(bool.Parse(animation["isPingPong"].ToString()));
					LuaTable frames = (LuaTable)animation["frames"];
					foreach (LuaTable frame in frames.Values)
					{
						var frameindex = int.Parse(frame["frame"].ToString());
						var duration = TimeSpan.FromSeconds((double)frame["duration"]);
						builder.AddFrame(frameindex, duration);
					}
				});
			}

			return spritesheet;
		}
	}
}
