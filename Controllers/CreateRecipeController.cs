using System;
using System.Collections.Generic;
using System.Text;
using lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers
{
    
    
    public class CreateRecipeController : Controller
    {
        private Recipe recipeToEdit;
        // GET
        public IActionResult CreateRecipe()
        {
            return View();
        }
        
        public IActionResult EditRecipe(int recipeToEditIndex)
        {
            
            
             recipeToEdit = Recipes.Instance.RecipesList[recipeToEditIndex];
             Recipes.Instance.RecipeToEdit = recipeToEdit;

            StringBuilder ingredients = new StringBuilder();

            for (int i = 0; i < recipeToEdit.Ingredients.Count; i++)
            {
                if (i == recipeToEdit.Ingredients.Count - 1)
                {
                    ingredients.Append(recipeToEdit.Ingredients[i]);
                }
                else
                {
                    ingredients.Append(recipeToEdit.Ingredients[i] + ",");

                }
            }
            
            StringBuilder steps = new StringBuilder();

            for (int i = 0; i < recipeToEdit.Steps.Count; i++)
            {
                if (i == recipeToEdit.Steps.Count - 1)
                {
                    steps.Append(recipeToEdit.Steps[i]);
                }
                else
                {
                    steps.Append(recipeToEdit.Steps[i] + ",");

                }
            }
            ViewBag.recipeToEdit = recipeToEdit;
            ViewBag.ingredients = ingredients;
            ViewBag.steps = steps;
            
            return View();
        }
        
        public RedirectResult CreateNewRecipe(string recipeName, string areaIngredients, string areaSteps)
        {
            Console.WriteLine(recipeName);
            Console.WriteLine(areaIngredients);
            Console.WriteLine(areaSteps);

            string[] stepsArr = areaSteps.Split(',');
            List<string> steps = new List<string>(stepsArr);
            string[] ingredientsArr = areaIngredients.Split(',');

            List<Ingredient> ingredients = new List<Ingredient>();
            foreach (var i in ingredientsArr)
            {
              
                string[] ingString = i.Split(' ');

              

                Ingredient ing = new Ingredient(ingString[0], float.Parse(ingString[1]), ingString[2]);
                ingredients.Add(ing);

            }


            Recipe recipeNew = new Recipe(recipeName, ingredients, steps);
            RecipesDAO recipeDao = new RecipesDAO();
            recipeDao.addRecipe(recipeNew);
            return Redirect("/Home");
        }
        public RedirectResult EditExistedRecipe(string recipeName, string areaIngredients, string areaSteps)
        {

            RecipesDAO recipeDAO = new RecipesDAO();

            List<String> toDelete = new List<string>();
            toDelete.Add(Recipes.Instance.RecipeToEdit.Name);
            recipeDAO.DeleteFromJson(toDelete);
            
    
            return CreateNewRecipe(recipeName,areaIngredients,areaSteps);

        }
    }
    
    

    
    
}