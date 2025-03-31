using System;
using System.Collections.Generic;

public class Pizza
{
    public string Size { get; set; }
    public string Dough { get; set; }
    public string Sauce { get; set; }
    public List<string> Toppings { get; set; }

    public Pizza()
    {
        Toppings = new List<string>();
    }

    public override string ToString()
    {
        return $"Пицца с размером {Size}, тестом {Dough}, соусом {Sauce}, начинка: {string.Join(", ", Toppings)}.";
    }
}

public abstract class PizzaBuilder
{
    protected Pizza pizza;

    public PizzaBuilder()
    {
        pizza = new Pizza();
    }

    public abstract void SetSize();
    public abstract void SetDough();
    public abstract void SetSauce();
    public abstract void AddToppings();

    public Pizza GetPizza()
    {
        return pizza;
    }
}

public class ConcretePizzaBuilder : PizzaBuilder
{
    public override void SetSize()
    {
        pizza.Size = "Большая";  
    }

    public override void SetDough()
    {
        pizza.Dough = "Тонкое";  
    }

    public override void SetSauce()
    {
        pizza.Sauce = "Томатный"; 
    }

    public override void AddToppings()
    {
        pizza.Toppings.Add("Моцарелла");
        pizza.Toppings.Add("Пепперони");
    }
}

public class PizzaDirector
{
    private PizzaBuilder pizzaBuilder;

    public PizzaDirector(PizzaBuilder builder)
    {
        pizzaBuilder = builder;
    }

    public Pizza Construct()
    {
        pizzaBuilder.SetSize();
        pizzaBuilder.SetDough();
        pizzaBuilder.SetSauce();
        pizzaBuilder.AddToppings();
        return pizzaBuilder.GetPizza();
    }
}

class Program
{
    static void Main(string[] args)
    {
        PizzaBuilder builder = new ConcretePizzaBuilder();

        PizzaDirector director = new PizzaDirector(builder);

        Pizza pizza = director.Construct();

        Console.WriteLine(pizza.ToString());
    }
}
