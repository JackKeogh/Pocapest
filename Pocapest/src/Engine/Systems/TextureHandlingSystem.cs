using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocapest.src.Engine.Systems
{
	public class TextureHandlingSystem
	{
		private static TextureHandlingSystem instance;
		private ContentManager contentManager;
		private Dictionary<string, Texture2D> textures;

		private TextureHandlingSystem(ContentManager contentManager)
		{
			this.contentManager = contentManager;
			this.textures = new Dictionary<string, Texture2D>();
			LoadTexturesFromLua();
		}

		public static TextureHandlingSystem Instance(ContentManager contentManager = null)
		{
			if (instance == null)
			{
				if (contentManager == null)
				{
					throw new ArgumentNullException(nameof(contentManager));
				}

				instance = new TextureHandlingSystem(contentManager);
			}

			return instance;
		}

		private void LoadTexturesFromLua()
		{
			Lua lua = new Lua();
			lua.DoFile("src/Scripts/textures.lua");

			LuaTable textureList = lua.GetTable("textures");

            foreach (LuaTable texture in textureList.Values)
			{
				var textureName = texture["name"].ToString();
				var texturePath = texture["path"].ToString();

				LoadTexture(textureName, texturePath);
			}
        }

		private void LoadTexture(string name, string path)
		{
			if (!this.textures.ContainsKey(name))
			{
				var texture = this.contentManager.Load<Texture2D>(path);
				this.textures.Add(name, texture);
			}
		}

		public void UnloadTexture(string name)
		{
			if (!this.textures.ContainsKey(name))
			{
				textures.Remove(name);
			}
		}

		public Texture2D GetTexture(string name)
		{
			if (textures.ContainsKey(name))
			{
				return this.CopyTexture(name);
			}

			return null;
		}

		private Texture2D CopyTexture(string name)
		{
			var originalTexture = this.textures[name];
			var textureCopy = new Texture2D(originalTexture.GraphicsDevice, originalTexture.Width, originalTexture.Height);
			Color[] data = new Color[originalTexture.Width * originalTexture.Height];
			originalTexture.GetData(data);
			textureCopy.SetData(data);
			return textureCopy;
		}
	}
}
