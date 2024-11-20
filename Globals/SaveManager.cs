using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nova.Scripts
{
    internal class SaveManager
    {
        private static SaveManager instance;
        private static Godot.Collections.Dictionary<string, Variant> SaveProperties { get; set; } = new Godot.Collections.Dictionary<string, Variant>();
        private static bool IsWindows = OS.GetName().ToLower() == "windows";
        private static string HomeDirectoryPath = IsWindows ? OS.GetEnvironment("USERPROFILE") : OS.GetEnvironment("HOME");
        private static string GameSaveDirectoryPath = IsWindows ? "Saved Games" : ".local/share";
        private static string SaveFileDir = System.IO.Path.Join(HomeDirectoryPath, GameSaveDirectoryPath, (string)ProjectSettings.GetSetting("application/config/name"));
        private static string SaveFileName = "data.save";
        private static string SaveFilePath = System.IO.Path.Join(SaveFileDir, SaveFileName);


        private SaveManager()
        {
            if (!System.IO.Directory.Exists(SaveFileDir))
            {
                System.IO.Directory.CreateDirectory(SaveFileDir);
            }

            if (!System.IO.File.Exists(SaveFilePath))
            {
                System.IO.File.Create(SaveFilePath);
            }

            var file = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Read);
            if (file == null)
            {
                GD.Print($"Save file could not be opened: {FileAccess.GetOpenError()}");
                throw new System.IO.IOException("Could not open save file");
            }

            var json = new Json();
            var jsonString = file.GetLine();
            var parseResult = json.Parse(jsonString);
            if (parseResult != Error.Ok)
            {
                GD.Print($"JSON parse error {json.GetErrorMessage()} with json string {jsonString} at line {json.GetErrorLine}");
                return;
            }

            SaveProperties = new Godot.Collections.Dictionary<string, Variant>((Godot.Collections.Dictionary)json.Data);
            file.Close();
        }

        static public SaveManager GetSaveManager()
        {
            if (instance == null)
            {
                instance = new SaveManager();
            }

            return instance;
        }

        public void UpsertSaveAttribute(Godot.Collections.Dictionary<string, Variant> items)
        {
            foreach (var item in items)
            {
                SaveProperties[item.Key] = item.Value;
            }
        }

        public void Save()
        {
            var file = FileAccess.Open(SaveFilePath, FileAccess.ModeFlags.Write);
            var json = new Json();
            file.StoreLine(Json.Stringify(SaveProperties));
            file.Flush();
            file.Close();
        }
    }
}
