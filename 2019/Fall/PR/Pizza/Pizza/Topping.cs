using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza
{
    public class Topping
    {
        readonly string topping;
        readonly int weight;
        public Topping(string topping, int weight)
        {
            this.topping = ToppingIsValid(topping.ToLower()) ? topping : 
                throw new Exception(string.Format("Cannot place {0} on top of your pizza.", topping));
            this.weight = weight >= 0 && weight <= 50 ? weight : 
                throw new Exception(string.Format("{0} weight should be in the range [1..50].", topping));
        }

        public double Calories
        {
            get
            {
                double toppingModif;
                switch (topping.ToLower())
                {
                    case "meat":
                        toppingModif = 1.2;
                        break;
                    case "veggies":
                        toppingModif = 0.8;
                        break;
                    case "cheese":
                        toppingModif = 1.1;
                        break;
                    case "sauce":
                        toppingModif = 0.9;
                        break;
                    default:
                        throw new Exception("Invalid topping");
                }
                return weight * toppingModif * 2;
            }
        }

        bool ToppingIsValid(string topping)
        {
            return
                topping == "meat" ||
                topping == "veggies" ||
                topping == "cheese" ||
                topping == "sauce";
        }
    }
}
