using System;
using System.Collections.Generic;
using Pizza;
using System.Linq;

namespace MyPizza
{
    class Program
    {
        static void Main(string[] args)
        {
            var pizzaName = Console.ReadLine().Split().Skip(1).First();
            var dough = Console.ReadLine().Split().Skip(1).ToArray();
            List<string> topping = new List<string>();
            string input = Console.ReadLine();
            do
            {
                topping.Add(input);
                input = Console.ReadLine();
            } while (input != "END");

            var pizza = new Pizza.Pizza(pizzaName, dough, topping.ToArray());

            Console.WriteLine(string.Format("{0} - {1} Calories.", pizza.Name, pizza.Calories));
        }
    }
}
