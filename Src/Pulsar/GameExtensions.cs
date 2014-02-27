using System;

namespace Pulsar.Module
{
	/// <summary>
	/// Game extensions.
	/// </summary>
	public static class GameExtensions
	{
		/// <summary>
		/// The global data.
		/// </summary>
		private static GameData _globalData;

		/// <summary>
		/// The temp data.
		/// </summary>
		private static GameData _tempData;

		/// <summary>
		/// Initialize the global data.
		/// </summary>
		internal static void InitGlobalData(this Game game)
		{
			_globalData = new GameData();
		}

		/// <summary>
		/// Initialize the temp data.
		/// </summary>
		/// <param name="game">Game.</param>
		internal static void InitTempData(this Game game)
		{
			_tempData = new GameData();
		}

		/// <summary>
		/// Get global data.
		/// </summary>
		/// <returns>The global data.</returns>
		internal static GameData GlobalData(this Game game)
		{
			return _globalData;
		}

		/// <summary>
		/// Get temp data.
		/// </summary>
		/// <returns>The temp data.</returns>
		internal static GameData TempData(this Game game)
		{
			return _tempData;
		}

		/// <summary>
		/// Get global data.
		/// </summary>
		/// <returns>The global data.</returns>
		public static GameData GlobalData(this IModule module)
		{
			return _globalData;
		}

		/// <summary>
		/// Get temp data.
		/// </summary>
		/// <returns>The temp data.</returns>
		public static GameData TempData(this IModule module)
		{
			return _tempData;
		}
	}
}

