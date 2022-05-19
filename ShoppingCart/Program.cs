using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ShoppingCart.Model;
using ShoppingCart.Repository;

namespace ShoppingCart
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("ShoppingCartDB"));
            IRepositoryProduct repositoryProduct = new ProductRepository(props);
            IRepositoryShippingRate repositoryShippingRate = new ShippingRateRepository(props);
            Service service = new Service(repositoryProduct, repositoryShippingRate);
            Run(service);

        }

        public static void Run(Service service)
        {
            Console.WriteLine("-------------------------------");
            Console.WriteLine("1.Show products.");
            Console.WriteLine("2.Show shipping rates.");
            Console.WriteLine("3.Add product to cart.");
            Console.WriteLine("4.Proceed Checkout.");
            Console.WriteLine("5.Proceed Checkout with Discounts.");
            Console.WriteLine("6.Proceed checkout(With VAT).");
            Console.WriteLine("0.Exit.");
            Console.WriteLine("Please choose a command:");
            string cmd = Console.ReadLine();
            Console.WriteLine("-------------------------------");
            while (true)
            {
                if (cmd == "1")
                {
                    foreach (Product p in service.findAllProducts())
                    {
                        Console.WriteLine(p.name + " - " + p.price + "$");
                    }
                }

                if (cmd == "2")
                {
                    foreach (ShippingRate sr in service.findAllShippingRates())
                    {
                        Console.WriteLine(sr.country + " - " + sr.rate + "$");
                    }
                }

                if (cmd == "3")
                {
                    Console.WriteLine("Please insert the name of the product:");
                    string name = Console.ReadLine();
                    Product product = service.findProductByName(name);
                    if (product == null)
                    {
                        Console.WriteLine("No product with the inserted name.");
                    }
                    else
                    {
                        service.addToCart(product);
                        Console.WriteLine("Your current shopping cart:");
                        foreach (Tuple<Product,int> p in service.returnCart())
                        {
                            Console.WriteLine(p.Item1.name + " x " + p.Item2);
                        }
                    }
                }

                if (cmd == "4")
                {
                    Console.WriteLine("Subtotal:$"+service.calculateSubTotal());
                    Console.WriteLine("Shipping:$"+service.calculateShipping());
                    Console.WriteLine("Total:$"+service.calculateTotal());
                }

                if (cmd == "5")
                {
                    Tuple<double, double> tupleCalc = service.calculateTotalWithDiscounts();
                    Console.WriteLine("Subtotal:$"+tupleCalc.Item1);
                    Console.WriteLine("Shipping:$"+tupleCalc.Item2);
                    Console.WriteLine();
                    Console.WriteLine("Discounts:");
                    int semMonitors = 0;
                    foreach (Tuple<Product, int> tuple in service.returnCart())
                    {
                        //Verificare daca cumparam doua monitoare.
                        if (tuple.Item1.name == "Monitor" && tuple.Item2 == 2)
                        {
                            semMonitors = 1;
                        }
                    }
                    if (service.returnCart().Count() >= 2)
                    {
                        Console.WriteLine("$10 off shipping: -$10");
                    }
                    foreach (Tuple<Product,int> tuple in service.returnCart())
                    {
                        //Reducere 10% pentru Keyboard.
                        if (tuple.Item1.name == "Keyboard")
                        {
                            Console.WriteLine("10% off keyboards: -$4.09");
                        }
                        else
                        {
                            //Jumatate de pret pentru lampa daca cumperi 2 monitoare.
                            if (semMonitors == 1 && tuple.Item1.name=="Desk Lamp")
                            {
                                Console.WriteLine("50% desk lamp(2 Monitors bought): -$44.99");
                            }
                        }
                        if (tuple.Item2 >= 2 && service.returnCart().Count()==1)
                        {
                            Console.WriteLine("$10 off shipping: -$10");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Total:$"+(tupleCalc.Item1+tupleCalc.Item2));
                }
                if (cmd == "6")
                {
                    Tuple<double, double> tupleCalc = service.calculateTotalWithDiscountsWithVAT();
                    Console.WriteLine("Subtotal:$"+tupleCalc.Item1);
                    Console.WriteLine("Shipping:$"+tupleCalc.Item2);
                    Console.WriteLine();
                    Console.WriteLine("Discounts:");
                    int semMonitors = 0;
                    foreach (Tuple<Product, int> tuple in service.returnCart())
                    {
                        //Verificare daca cumparam doua monitoare.
                        if (tuple.Item1.name == "Monitor" && tuple.Item2 == 2)
                        {
                            semMonitors = 1;
                        }
                    }
                    if (service.returnCart().Count() >= 2)
                    {
                        Console.WriteLine("$10 off shipping: -$10");
                    }
                    foreach (Tuple<Product,int> tuple in service.returnCart())
                    {
                        //Reducere 10% pentru Keyboard.
                        if (tuple.Item1.name == "Keyboard")
                        {
                            Console.WriteLine("10% off keyboards: -$4.09");
                        }
                        else
                        {
                            //Jumatate de pret pentru lampa daca cumperi 2 monitoare.
                            if (semMonitors == 1 && tuple.Item1.name=="Desk Lamp")
                            {
                                Console.WriteLine("50% desk lamp(2 Monitors bought): -$44.99");
                            }
                        }
                        if (tuple.Item2 >= 2 && service.returnCart().Count()==1)
                        {
                            Console.WriteLine("$10 off shipping: -$10");
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine("Total:$"+(tupleCalc.Item1+tupleCalc.Item2));
                }
                
                if (cmd == "0")
                {
                    break;
                }
                Console.WriteLine("-------------------------------");
                Console.WriteLine("1.Show products.");
                Console.WriteLine("2.Show shipping rates.");
                Console.WriteLine("3.Add product to cart.");
                Console.WriteLine("4.Proceed Checkout.");
                Console.WriteLine("5.Proceed Checkout with Discounts.");
                Console.WriteLine("6.Proceed checkout(With VAT).");
                Console.WriteLine("0.Exit.");
                Console.WriteLine("Please choose a command:");
                cmd = Console.ReadLine();
                Console.WriteLine("-------------------------------");
            }
        }
        
        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];
            
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}