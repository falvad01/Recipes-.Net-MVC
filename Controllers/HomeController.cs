
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using lab2.Models;

namespace lab2.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
           // Recipes recipes = Recipes.Instance;
            RecipesDAO recipeDao = new RecipesDAO();
            Recipes.Instance.RecipesList = recipeDao.ReadJson();


            ViewBag.recipes = Recipes.Instance.RecipesList;
            
            return View();
        }

        
        
        public ActionResult MyAction(string submitButton,string[] selectedRecipes) {
            switch(submitButton) {
                case "Edit":
                    return(Edit(selectedRecipes));
                case "Delete":
                    return(Delete(selectedRecipes));
                default:
                    return(View("Index"));
            }
        }

        private ActionResult Delete(string[] selectedRecipes)
        {

            List<String> toDelete = new List<string>(selectedRecipes);
            
            RecipesDAO recipeDao = new RecipesDAO();
            recipeDao.DeleteFromJson(toDelete);

            for (int i = 0; i < selectedRecipes.Length; i++)
            {
                Console.Write(i);
                for (var j = 0; j < Recipes.Instance.RecipesList.Count; j++)
                {
                    var r = Recipes.Instance.RecipesList[j];
                    if (selectedRecipes[i] == r.Name)
                    {
                        Console.Write(r.Name);
                        Recipes.Instance.RecipesList.RemoveAt(j);
                    }
                }
            }
            
            ViewBag.recipes = Recipes.Instance.RecipesList;
            return(View("Index"));
        }

        private ActionResult Edit(string[] selectedRecipes)
        {
            
            ViewBag.recipes = Recipes.Instance.RecipesList;

            if (selectedRecipes.Length != 1)
            {

                return View("Index");
            }
            else
            {
                int index = -1;
                for (var i = 0; i < Recipes.Instance.RecipesList.Count; i++)
                {
                    
                    if (Recipes.Instance.RecipesList[i].Name == selectedRecipes[0])
                    {
                        index = i;
                    }
                }
                
                return RedirectToAction("EditRecipe", "CreateRecipe", new{recipeToEditIndex = index});
            }
        
    }

  
        public IActionResult Privacy() => View();
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        
        public IActionResult CreateRecipe() => RedirectToAction("CreateRecipe", "CreateRecipe");
        
        public IActionResult ShopingList() =>  RedirectToAction("ShopingList", "ShopingList");
        


       

    }
}

    

