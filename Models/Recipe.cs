using System;
using System.Collections;
using System.Collections.Generic;

namespace lab2
{
    public class Recipe
    {
        private string name;
        private List<Ingredient> ingredients;
        private List<String> steps;


        public Recipe(string name, List<Ingredient> ingredients, List<String> steps)
        {
            this.name = name;
            this.ingredients = ingredients;
            this.steps = steps;
        }

        public string Name
        {
            get => name;
            set => name = value;
        }
        
        public List<Ingredient> Ingredients
        {
            get => ingredients;
            set => ingredients = value;
        }

        public List<string> Steps
        {
            get => steps;
            set => steps = value;
        }
    }
}