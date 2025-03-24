namespace Recipe.Service
{
    public class ProgramService
    {
        private PrintService _printService = new();
        private Recipes _recipes = new();
        private Dictionary<string, Action> _mainMenuOptions;
        private SearchService _searchService = new();
        private FileService _fileService = new();
        public ProgramService()
        {
            _mainMenuOptions = new Dictionary<string, Action>
            {
                { "Add recipe", AddRecipe },
                { "Search recipe", SearchRecipe },
                { "Show all recipes", ListAllRecipes },
                { "Exit", ExitProgram }
            };
        }

        public void start()
        {
            _printService.Print("Welcome in Recipe app by Václav Taitl as Assignment for Object-Oriented Programming class - XAMK v1.0");
            _printService.Print("Choose a file with name Recipes -> [Recipe.json], if file not exist, create new.");
            _fileService.SetPath();

            _recipes.AddRecipe(_fileService.LoadRecipes());
            while (true) //program loop
            {
                try
                {
                    HandleMenu(_mainMenuOptions);
                }
                catch (Exception e)
                {
                    _printService.Print("{Err occured: " + e.Message + "}");
                }

            }
        }
        public void AddRecipe()
        {
            Recipe recipe = CreatingRecipeService.createRecipe();
            _recipes.AddRecipe(recipe);
            _fileService.SaveRecipes(_recipes.GetRecipes());
            _printService.Print("[Recipe saved]", true, ConsoleColor.Green);
        }

        private void SearchRecipe()
        {
            _searchService.SearchRecipe(_recipes.GetRecipes());
        }

        public void ListAllRecipes()
        {
            _printService.ListRecipes(_recipes.GetRecipes());
        }
        public void ExitProgram()
        {
            _fileService.SaveRecipes(_recipes.GetRecipes());
            Environment.Exit(0);
        }
        public void HandleMenu(Dictionary<string, Action> menuOpt)
        {
            _printService.deli();
            _printService.Print($"│       [Menu]       │");
            _printService.deli();
            int index = 1;
            foreach (var option in menuOpt.Keys)
            {
                _printService.Print("{" + index + "}. ", false);
                _printService.Print($"{option}");
                index++;
            }

            _printService.Print("Choose action: ", false);
            string choice = Console.ReadLine();

            if (int.TryParse(choice, out int optionIndex) && optionIndex > 0 && optionIndex <= menuOpt.Count)
            {
                string selectedOption = new List<string>(menuOpt.Keys)[optionIndex - 1];
                _printService.Print($"You choose: [{selectedOption}]");

                menuOpt[selectedOption].Invoke(); // Zavolá odpovídající metodu
            }
            else
            {
                _printService.Print("{Invalid number, try again}");
            }
        }
    }
}
