using NUnit.Framework;
using Pizza;
using MyPizza;
using System;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DoughWhiteChewy100()
        {
            // arrange
            var flour = "White";
            var tq = "Chewy";
            var weight = 100;

            // act
            var dough = new Dough(flour, tq, weight);

            // assert
            Assert.AreEqual(330, dough.Colories);
        }

        [Test]
        public void DoughTip500Chewy100()
        {
            // arrange
            var flour = "Tip500";
            var tq = "Chewy";
            var weight = 100;

            // act

            // assert
            Assert.Catch<Exception>(() => { var a = new Dough(flour, tq, weight); });
        }

        [Test]
        public void DoughWhiteChewy240()
        {
            // arrange
            var flour = "White";
            var tq = "Chewy";
            var weight = 240;

            // act

            // assert
            Assert.Catch<Exception>(() => { var a = new Dough(flour, tq, weight); });
        }

        [Test]
        public void ToppingMeat30()
        {
            // arrange
            var toppingType = "meat";
            var weight = 30;

            // act
            var topping = new Topping(toppingType, weight);

            // assert
            Assert.AreEqual(72, topping.Calories);
        }

        [Test]
        public void ToppingKrenvirshi500()
        {
            // arrange
            var topping = "White";
            var weight = 500;

            // act

            // assert
            Assert.Catch<Exception>(() => { var a = new Topping(topping, weight); });
        }

        [Test]
        public void ToppingMeat500()
        {
            // arrange
            var topping = "Meat";
            var weight = 500;

            // act

            // assert
            Assert.Catch<Exception>(() => { var a = new Topping(topping, weight); });
        }

        [Test]
        public void Meatless370Calories()
        {
            // arrange
            var name = "Meatless";
            var dough = "Wholegrain Crispy 100".Split();
            var toppings = new[] { "Topping Veggies 50", "Topping Cheese 50" };

            var pizza = new Pizza.Pizza(name, dough, toppings);

            Assert.AreEqual("Meatless - 370 Calories.",
                string.Format("{0} - {1} Calories.", pizza.Name, pizza.Calories));
        }

        [Test]
        public void Exceptions()
        {
            // arrange
            var name = "Bulgarian";
            var dough = "White Chewy 100".Split();
            var toppings = new[] {
                "Topping Sauce 20",
                "Topping Cheese 50",
                "Topping Cheese 40",
                "Topping Meat 10",
                "Topping Sauce 10",
                "Topping Cheese 30",
                "Topping Cheese 40",
                "Topping Meat 20",
                "Topping Sauce 30",
                "Topping Cheese 25",
                "Topping Cheese 40",
                "Topping Meat 40"
            };

            Assert.Catch<Exception>(() => { var pizza = new Pizza.Pizza(name, dough, toppings); });
        }

        [Test]
        public void Exceptions1()
        {
            // arrange
            var name = "Bulgarian";
            var dough = "White Chewy 100".Split();
            var toppings = new[] {
                "Topping Sirene 50",
                "Topping Cheese 50",
                "Topping Krenvirsh 20",
                "Topping Meat 10"
            };

            Assert.Catch<Exception>(() => { var pizza = new Pizza.Pizza(name, dough, toppings); });
        }

        [Test]
        public void Exceptions3()
        {
            // arrange
            var name = "Bulgarian";
            var dough = "White Homemade 200".Split();
            var toppings = new[] {
                "Topping Meat 123"
            };

            Assert.Catch<Exception>(() => { var pizza = new Pizza.Pizza(name, dough, toppings); });
        }
    }
}