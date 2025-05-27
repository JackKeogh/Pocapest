using Microsoft.Xna.Framework;
using MonoGame.Extended.ECS;
using NLua;
using Pocapest.src.Engine.GameMechanics;
using Pocapest.src.Helper;
using Pocapest.src.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

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

			this.LoadAreaBase(area, lArea, world);

			foreach (LuaTable tile in tiles.Values)
			{
				string texture = tile["TextureName"].ToString();
				int x = int.Parse(tile["X"].ToString());
				int y = int.Parse(tile["Y"].ToString());
				ColliderType type = Enum.Parse<ColliderType>(tile["ColliderType"].ToString());

				Entity entity = null;
				if (texture.Equals("grass"))
				{
					int srcX = Constants.TileSize * RandomNumberGenerator.GetRandomInt(1, 4);
					entity = this.Factory.CreateTileEntity(world, texture, x, y, type, srcX, srcW: 64);
				}
				else
				{
					entity = this.Factory.CreateTileEntity(world, texture, x, y, type);
				}
				area.TrackEntity(entity);
			}

			return area;
		}

		private void LoadAreaBase(Area area, LuaTable lArea, World world)
		{
			string texture = lArea["base"].ToString();
			int seed = int.Parse(lArea["seed"].ToString());
			int cellsW = area.Bounds.Width / Constants.TileSize;
			int cellsH = area.Bounds.Height / Constants.TileSize;

			RandomNumberGenerator.UpdateSeed(seed);

			for (int y = 0; y < cellsH; y++)
			{
				for (int x = 0; x < cellsW; x++)
				{
					int xPos = x * Constants.TileSize;
					int yPos = y * Constants.TileSize;

					int ranX = RandomNumberGenerator.GetRandomInt(0, 4) * 16;

					Debug.WriteLine(ranX);

					Entity e = this.Factory.CreateTileEntity(world, texture, xPos, yPos, ColliderType.Walkable, srcX: ranX, srcW: 64, srcH:16);
					area.TrackEntity(e);
				}
			}

			RandomNumberGenerator.RandomiseSeed();
		}
	}
}
