﻿using MonoGame.Extended.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Engine.Commands
{
	public interface IInputCommand
	{
		void Execute(Entity entity);
	}
}
