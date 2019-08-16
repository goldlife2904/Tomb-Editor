﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using TombLib.LevelData;
using TombLib.LevelData.IO;
using TombLib.Projects;

namespace TombIDE.Shared
{
	public class LevelHandling
	{
		public static List<string> GenerateSectionMessages(ProjectLevel level, int ambientSoundID, bool horizon)
		{
			return new List<string>
			{
				"\n[Level]",
				"Name= " + level.Name,
				"Level= DATA\\" + level.Name.ToUpper().Replace(' ', '_') + ", " + ambientSoundID,
				"LoadCamera= 0, 0, 0, 0, 0, 0, 0",
				"Horizon= " + (horizon? "ENABLED" : "DISABLED")
			};
		}

		public static void UpdatePrj2GameSettings(string prj2FilePath, ProjectLevel destLevel, Project destProject)
		{
			Level level = Prj2Loader.LoadFromPrj2(prj2FilePath, null);

			string dataFileName = destLevel.Name.Replace(' ', '_') + destProject.GetLevelFileExtension();

			level.Settings.GameDirectory = destProject.ProjectPath;
			level.Settings.GameExecutableFilePath = Path.Combine(destProject.ProjectPath, destProject.GetExeFileName());
			level.Settings.GameLevelFilePath = Path.Combine(destProject.ProjectPath, "data", dataFileName);
			level.Settings.GameVersion = destProject.GameVersion;

			Prj2Writer.SaveToPrj2(prj2FilePath, level);
		}

		public static string RemoveIllegalNameSymbols(string levelName)
		{
			char[] illegalNameChars = { ';', '[', ']', '=', ',', '.', '!' };
			return illegalNameChars.Aggregate(levelName, (current, c) => current.Replace(c.ToString(), string.Empty));
		}
	}
}
