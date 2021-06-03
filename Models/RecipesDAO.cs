using System;

using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


//using JsonSerializer = System.Text.Json.JsonSerializer;


namespace lab2.Controllers
{
    public class RecipesDAO
    {

        
        public RecipesDAO()
        {
        }


        public  List<Recipe> ReadJson()
        {
            List<Recipe> recipes = new List<Recipe>();
            JObject o1 = JObject.Parse(File.ReadAllText(@"recipes.json"));

            // read JSON directly from a file
            using (StreamReader file = File.OpenText(@"recipes.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject) JToken.ReadFrom(reader);

                IList<string> keys = o2.Properties().Select(p => p.Name).ToList();
                
                Console.WriteLine();

                foreach (var t in keys)
                {
                    JObject recipeJ = (JObject) o2.GetValue(t);
                    List<Ingredient> ingredients = GetIngredients(recipeJ);
                    List<string> steps = GetSteps(recipeJ);
                    Recipe recipe = new Recipe(t,ingredients,steps);
                    recipes.Add(recipe);
                }
            }

            return recipes;
        }

        public void addRecipe(Recipe recipeNew)
        {
            
            using (StreamReader file = File.OpenText(@"recipes.json"))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                
                JObject o2 = (JObject) JToken.ReadFrom(reader);
                
                JObject itemToAdd = new JObject();
                
                foreach (Ingredient i in recipeNew.Ingredients)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(i.Ammount.ToString(CultureInfo.InvariantCulture) + " ");
                    sb.Append(i.Unit);
                    
                    itemToAdd[i.Name] = sb.ToString();

                }
                itemToAdd["recipe"] = JToken.FromObject(recipeNew.Steps);
               
                o2[recipeNew.Name] = itemToAdd;
                
                System.IO.File.WriteAllText(@"recipes.json", o2.ToString());
                
            }
        }

        public void DeleteFromJson(List<String> toDelete)//TODO add parameters
        {

            
                using (StreamReader file = File.OpenText(@"recipes.json"))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o2 = (JObject) JToken.ReadFrom(reader);
                    foreach (string i in toDelete)
                    {
                        o2.Remove(i);
                    }

                    System.IO.File.WriteAllText(@"recipes.json", o2.ToString());

                }


        }

        private List<string> GetSteps(JObject recipe)
        {

            JArray steps = (JArray)recipe.GetValue("recipe");
            List<string> stepsArray = new List<string>();
            if (steps != null)
            {
                stepsArray = steps.ToObject<List<string>>();
            }
            
            return stepsArray;
        }

        private List<Ingredient> GetIngredients(JObject recipe)
        {

            IList<string> keys = recipe.Properties().Select(p => p.Name).ToList();
            Ingredient ing;
            List<Ingredient> ingredientArrayList = new List<Ingredient>();
            foreach (var t in keys)
            {
                if (t != "recipe")
                {
                    JValue ingredientJ = (JValue)recipe.GetValue(t);
                    String ammountAndUnit = ingredientJ.ToString();

                    String[] split = ammountAndUnit.Split(" ");
                    
                    ing = new Ingredient(t,float.Parse(split[0]),split[1]);

                    ingredientArrayList.Add(ing);
                }
            }
            
            return ingredientArrayList;
        }
    }
}
