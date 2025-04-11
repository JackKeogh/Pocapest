using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ECS;
using Pocapest.src.Engine.Commands;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pocapest.src.Engine.Systems
{
	public class InputHandlingSystem
	{
		private Dictionary<Keys, IInputCommand> keyCommands = new Dictionary<Keys, IInputCommand>();
		private KeyboardState currentState;
		private KeyboardState previousState;
		private double keyPressedTime = 0.0;
		private const double debounceTime = 0.2;

		public InputHandlingSystem() 
		{
			this.keyCommands[Keys.W] = new MoveUpCommand();
			this.keyCommands[Keys.S] = new MoveDownCommand();
			this.keyCommands[Keys.D] = new MoveRightCommand();
			this.keyCommands[Keys.A] = new MoveLeftCommand();
		}

		public void HandleInput(Entity entity, GameTime gameTime)
		{
			this.currentState = Keyboard.GetState();

			foreach (var command in keyCommands)
			{
				if (this.IsKeyPressed(command.Key, gameTime))
				{
					command.Value.Execute(entity);
				}
			}
		}

		private bool IsKeyPressed(Keys key, GameTime gameTime)
		{
			if (this.currentState.IsKeyDown(key) && !this.previousState.IsKeyDown(key))
			{
				if (gameTime.TotalGameTime.TotalSeconds - keyPressedTime > debounceTime)
				{
					keyPressedTime = gameTime.TotalGameTime.TotalSeconds;
					return true;
				}
			}

			return false;
		}
	}
}
