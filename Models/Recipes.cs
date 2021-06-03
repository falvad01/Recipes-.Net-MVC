using System.Collections.Generic;

namespace lab2.Models
{
    public class Recipes
    {

        private List<Recipe> recipesList;
        private Recipe recipeToEdit;
        private static Recipes instance=null;


        public Recipes()
        {
            recipesList = new List<Recipe>();
        }

        public Recipe RecipeToEdit
        {
            get => recipeToEdit;
            set => recipeToEdit = value;
        }


        public List<Recipe> RecipesList
        {
            get => recipesList;
            set => recipesList = value;
        }

        public void addRecipe(Recipe recipe)
        {
            recipesList.Add(recipe);
        }
        
        public static Recipes Instance
        {
            get
            {
                if (instance==null)
                {
                    instance = new Recipes();
                }
                return instance;
            }
        }
    }
}
