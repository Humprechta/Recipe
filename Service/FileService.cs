using System.Diagnostics;
using System.Text.Json;

namespace Recipe
{
    public class FileService
    {
        public string filePath { get; private set; }
        private readonly PrintService _print = new();

        public FileService()
        {
        }
        /// <summary>
        /// Let user set path for finding json file
        /// </summary>
        public void SetPath()
        {
            Process process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = "-Command \"Add-Type -AssemblyName System.Windows.Forms; $f=New-Object System.Windows.Forms.OpenFileDialog; $f.Filter='JSON Files (*.json)|*.json'; $f.ShowDialog() | Out-Null; Write-Output $f.FileName\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string result = process.StandardOutput.ReadToEnd().Trim();
            process.WaitForExit();
            filePath = string.IsNullOrEmpty(result) ? string.Empty : result;

        }

        /// <summary>
        /// Uloží seznam receptů do souboru jako JSON.
        /// </summary>
        public void SaveRecipes(List<Recipe> recipes)
        {

            try
            {
                string json = JsonSerializer.Serialize(recipes, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
                _print.Print("[Recipes saved successfully.]", true, ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                _print.Print("{Error saving recipes: " + ex.Message + "}");
            }
        }

        /// <summary>
        /// Načte seznam receptů ze souboru JSON.
        /// </summary>
        public List<Recipe> LoadRecipes()
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    _print.Print("[Recipe file not found. Returning an empty list.]");
                    return new List<Recipe>();
                }

                string json = File.ReadAllText(filePath);
                _print.Print("[Recipes succesfully loaded]", true, ConsoleColor.Green);
                return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
            }
            catch (Exception ex)
            {
                _print.Print("{Error loading recipes: " + ex.Message + "}");
                return new List<Recipe>();
            }
        }
    }
}