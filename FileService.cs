using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Recipe
{
        public class FileService
    {
        private readonly string _filePath;
        private readonly PrintService print = new();

        public FileService(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Uloží seznam receptů do souboru jako JSON.
        /// </summary>
        public void SaveRecipes(List<Recipe> recipes)
        {

            try
            {
                string json = JsonSerializer.Serialize(recipes, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
                print.Print("[Recipes saved successfully.]", true, ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                print.Print("{Error saving recipes: " + ex.Message + "}");
            }
        }

        /// <summary>
        /// Načte seznam receptů ze souboru JSON.
        /// </summary>
        public List<Recipe> LoadRecipes()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    print.Print("[Recipe file not found. Returning an empty list.]");
                    return new List<Recipe>();
                }

                string json = File.ReadAllText(_filePath);
                print.Print("[Recipes succesfully loaded]",true, ConsoleColor.Green);
                return JsonSerializer.Deserialize<List<Recipe>>(json) ?? new List<Recipe>();
            }
            catch (Exception ex)
            {
                print.Print("{Error loading recipes: " + ex.Message + "}");
                return new List<Recipe>();
            }
        }
    }
}