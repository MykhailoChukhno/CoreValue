using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Pizza
{
    class Program
    {
        static void Main(string[] args)
        {
            PizzaOrder[] pizzaOrders = JsonConvert.DeserializeObject<PizzaOrder[]>(Properties.Resources.Pizzas);
            CountPizzaConfigurations countPizzaArray = new CountPizzaConfigurations(pizzaOrders);
            countPizzaArray.SortConfigurations();
            countPizzaArray.ShowFirstNConfigurations(20);
            Console.ReadKey();
        }
    }

    public class PizzaOrder
    {
        public string[] toppings { get; set; }
    }
    public class PizzaOrderCount : PizzaOrder
    {
        public int count { get; set; }
        public PizzaOrderCount(PizzaOrder pizzaOrder)
        {
            this.toppings = pizzaOrder.toppings;
            this.count = 1;
        }
    }
    public class CountPizzaConfigurations
    {
        public List<PizzaOrderCount> pizzaOrderCount = new List<PizzaOrderCount>();
        public CountPizzaConfigurations(PizzaOrder[] pizzaOrders)
        {
            foreach (PizzaOrder iPizzaOrder in pizzaOrders)
            {
                this.AddPizzaConfigurations(iPizzaOrder);
            }
        }
        public void AddPizzaConfigurations(PizzaOrder pizzaOrder)
        {
            bool addPrap = false;
            for (int i = 0; i < pizzaOrderCount.Count; i++)
            {
                if (!addPrap & pizzaOrderCount[i].toppings.Length == pizzaOrder.toppings.Length)
                {
                    bool iPrap = true;
                    foreach (string iToppings in pizzaOrder.toppings)
                    {
                        if (Array.IndexOf(pizzaOrderCount[i].toppings, iToppings) == -1)
                        {
                            iPrap = false;
                        }
                    }
                    if (iPrap)
                    {
                        pizzaOrderCount[i].count++;
                        addPrap = true;
                    }
                }
            }
            if (!addPrap)
                this.pizzaOrderCount.Add(new PizzaOrderCount(pizzaOrder));
        }
        public void SortConfigurations()
        {
            pizzaOrderCount.Sort(delegate (PizzaOrderCount first, PizzaOrderCount second)
            {
                return second.count.CompareTo(first.count);
            });
        }
        public void ShowFirstNConfigurations(int count)
        {
            for (int i = 0; i < count && i < this.pizzaOrderCount.Count; i++)
            {
                Console.WriteLine(String.Format("\nCount : {0}", pizzaOrderCount[i].count));
                foreach (string iStr in pizzaOrderCount[i].toppings)
                {
                    Console.WriteLine(iStr);
                }
            }
        }
    }
}
