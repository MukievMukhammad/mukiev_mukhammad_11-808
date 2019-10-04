using System;

namespace Pizza
{
    public class Dough
    {
        readonly string flour;
        readonly string bakingTeqchnique;
        readonly int weight;
        public Dough(string flour, string bakingTeqchnique, int weight)
        {
            this.flour = FlourIsValid(flour.ToLower()) ? flour : throw new Exception("Invalid type of dough.");
            this.bakingTeqchnique = bakingTeqchnique;
            this.weight = weight >= 0 && weight <= 200 ? 
                weight : throw new Exception("Dough weight should be in the range [1..200].");
        }

        public double Colories
        {
            get
            {
                var flourModification = flour.ToLower() == "white" ? 1.5 : 1;
                double bakingModification;
                switch (bakingTeqchnique.ToLower())
                {
                    case "crispy":
                        bakingModification = 0.9;
                        break;
                    case "chewy":
                        bakingModification = 1.1;
                        break;
                    case "homemade":
                        bakingModification = 1;
                        break;
                    default:
                        throw new Exception("Invalid baking technique");
                }
                return 2 * weight * flourModification * bakingModification;
            }
        }

        bool FlourIsValid(string flour)
        {
            return
                flour.ToLower() == "white" ||
                flour.ToLower() == "wholegrain";
        }
    }
}
