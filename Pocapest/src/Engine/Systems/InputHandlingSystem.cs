using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ECS;
using Pocapest.src.Engine.Commands;
using System.Collections.Generic;

namespace Pocapest.src.Engine.Systems
{
	public class InputHandlingSystem
	{
		private Dictionary<Keys, IInputCommand> keyCommands = new Dictionary<Keys, IInputCommand>();

		public InputHandlingSystem() 
		{
			keyCommands[Keys.W] = new MoveUpCommand();
			keyCommands[Keys.S] = new MoveDownCommand();
			keyCommands[Keys.D] = new MoveRightCommand();
			keyCommands[Keys.A] = new MoveLeftCommand();
		}

		public void HandleInput(Entity entity)
		{
			foreach (var command in keyCommands)
			{
				if (Keyboard.GetState().IsKeyDown(command.Key))
				{
					command.Value.Execute(entity);
				}
			}
		}
	}
}
