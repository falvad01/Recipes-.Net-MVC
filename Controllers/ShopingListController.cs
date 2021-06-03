using System;
using System.Collections.Generic;
using System.Text;
using lab2.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab2.Controllers
{
    public class ShopingListController : Controller
    {
        // GET
        public IActionResult ShopingList()
        {
            ViewBag.shopingRecipes = Recipes.Instance.RecipesList;
            return View();
        }


        [HttpPost]
        public IActionResult ShopingList(int[] selectedRecipes)
        {
            List<Recipe> shoppinglist = new List<Recipe>();
            List<Recipe> recipes = Recipes.Instance.RecipesList;


            for (int i = 0; i < selectedRecipes.Length; i++)
            {
                for (int j = 0; j < selectedRecipes[i]; j++)
                {
                    shoppinglist.Add(recipes[i]);
                }
            }


            List<Ingredient> vectorIngredients = new List<Ingredient>();
            foreach (var forRecipe in shoppinglist)
            {
                List<Ingredient> auxvec = forRecipe.Ingredients;


                foreach (var forIng in auxvec)
                {
                    if (vectorIngredients.Count == 0)
                    {
                        vectorIngredients.Add(forIng);
                    }
                    else
                    {
                        int existIndex = -1;
                        for (int k = 0; k < vectorIngredients.Count; k++)
                        {
                            if (forIng.Equals(vectorIngredients[k]))
                            {
                                existIndex = k;
                            }
                        }

                        if (existIndex != -1)
                        {
                            float newValue = vectorIngredients[existIndex].Ammount + forIng.Ammount;
                            vectorIngredients.Add(new Ingredient(forIng.Name, newValue, forIng.Unit));
                            vectorIngredients.RemoveAt(existIndex);
                        }
                        else
                        {
                            vectorIngredients.Add(forIng);
                        }
                    }
                }
            }


            StringBuilder ingredientsAssemble = new StringBuilder();


            for (int i = 0; i < vectorIngredients.Count; i++)
            {
                ingredientsAssemble.Append(vectorIngredients[i].ToString());
                ingredientsAssemble.Append("\n");
            }


            return Content(ingredientsAssemble.ToString());
        }
    }
}