using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using NLua;
using Pocapest.src.Engine.GameMechanics;
using Pocapest.src.Models;
using System;
using System.Collections.Generic;

namespace Pocapest.src.Engine.Systems
{
	public class AreaLoadingSystem
	{
		private EntityFactory Factory;

		public AreaLoadingSystem(EntityFactory factory)
		{
			this.Factory = factory;
		}

		public List<Area> LoadAreas(World world)
		{
			List<Area> areas = new List<Area>();
			Lua root = new Lua();
			root.DoFile("src/Scripts/Locations/base.lua");

			LuaTable areaNames = (LuaTable)root["areas"];

			foreach (LuaTable item in areaNames.Values)
			{
				areas.Add(this.LoadArea(item["name"].ToString(), world));
			}

			return areas;
		}

		private Area LoadArea(string areaName, World world)
		{
			Area area = new Area();
			string path = $"src/Scripts/Locations/{areaName}.lua";
			Lua root = new Lua();
			root.DoFile(path);

			LuaTable lArea = (LuaTable)root[areaName];
			LuaTable bounds = (LuaTable)lArea["bounds"];
			LuaTable tiles = (LuaTable)lArea["tiles"];

			area.Bounds = new Rectangle(
				int.Parse(bounds["x"].ToString()),
				int.Parse(bounds["y"].ToString()),
				int.Parse(bounds["w"].ToString()),
				int.Parse(bounds["h"].ToString())
				);

			foreach (LuaTable tile in tiles.Values)
			{
				string texture = tile["TextureName"].ToString();
				int x = int.Parse(tile["X"].ToString());
				int y = int.Parse(tile["Y"].ToString());
				ColliderType type = Enum.Parse<ColliderType>(tile["ColliderType"].ToString());

				Entity entity = this.Factory.CreateTileEntity(world, texture, x, y, type);
				area.TrackEntity(entity);
			}

			return area;
		}
	}
}
