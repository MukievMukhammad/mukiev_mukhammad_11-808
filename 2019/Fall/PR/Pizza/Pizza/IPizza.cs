using System;
using System.Collections.Generic;
using System.Text;

namespace Pizza
{
    interface IPizza
    {
        void AddToppings(List<string> topppings);
        void DetermineDough(string flour);
    }
}
