using System;
using System.Linq;
using System.Collections.Generic;

namespace Pizza
{
    public class Pizza
    {
        public string Name { get; }
        private readonly List<Topping> toppings = new List<Topping>();
        private Dough dough;
        public Pizza(string name, string[] dough, string[] toppings)
        {
            Name = name.Length > 0 && name.Length <= 15 ? 
                name : throw new Exception("«Название пиццы должно быть от 1 до 15 символов».");
            AddToppings(toppings);
            DetermineDough(dough);
        }

        public double Calories
        {
            get
            {
                double toppingsCalories = 0;
                foreach (var topp in toppings)
                    toppingsCalories += topp.Calories;
                return dough.Colories + toppingsCalories;
            }
        }

        // Как скрыть этот метод
        public void AddToppings(string[] toppings)
        {
            if (toppings.Length > 10) throw new Exception("Number of toppings should be in range [0..10]");
            foreach(var topp in toppings)
            {
                var componets = topp.Split().Skip(1);
                this.toppings.Add(new Topping(componets.First(), int.Parse(componets.Last())));
            }
        }

        public void DetermineDough(string[] dough)
        {
            this.dough = new Dough(dough[0], dough[1], int.Parse(dough[2]));
        }
    }
}
