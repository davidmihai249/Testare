using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using ShoppingCart.Model;
using ShoppingCart.Repository;

namespace ShoppingCart
{
    public class Service
    {
        private IRepositoryProduct productRepository;
        private IRepositoryShippingRate shippingRateRepository;
        private IList<Product> cart = new List<Product>();
        private int semMonitors = 0;

        public Service(IRepositoryProduct productRepository, IRepositoryShippingRate shippingRateRepository)
        {
            this.productRepository = productRepository;
            this.shippingRateRepository = shippingRateRepository;
        }

        public IEnumerable<Product> findAllProducts()
        {
            return productRepository.FindAll();
        }

        public IEnumerable<ShippingRate> findAllShippingRates()
        {
            return shippingRateRepository.FindAll();
        }

        public void addToCart(Product product)
        {
            cart.Add(product);
        }

        public Product findProductByName(string name)
        {
            return productRepository.findByProductName(name);
        }

        public ShippingRate findByCountry(string Country)
        {
            return shippingRateRepository.findByCountry(Country);
        }
        
        public IEnumerable<Tuple<Product, int>> returnCart()
        {
            //alreadyChecked = Lista de obiecte deja verificate(avem spre exemplu 2 monitoare si un Keyboard, lista va arata
            // ceva de genul : ["Monitor","Monitor","keyboard"], iar cand formez cartul pentru returnare, e posibil sa apara ceva de genul
            // [[Monitor,2],[Monitor,2],[Keyboard,1], ceea ce nu e corect, se repeta [Monitor,2].
            //Acel alreadyChecked ma ajuta sa evit dublicatele.
            //printCart e o lista de tupluri, primul item o sa fie obiectul(Product, in acest caz), iar in al doilea
            //numarul de Products cu acel nume.(Adica ne va specifica ca avem 2 Monitoare, 1 Keyboard)
            IList<string> alreadyChecked = new List<string>();
            IList<Tuple<Product, int>> printCart = new List<Tuple<Product, int>>();
            foreach (Product p in cart)
            {
                int sem = 1;
                foreach (string check in alreadyChecked)
                {
                    if (p.name.Equals(check))
                    {
                        sem = 0;
                    }
                }

                if (sem == 1)
                {
                    int count = 0;
                    foreach (Product p2 in cart)
                    {
                        if (p.name.Equals(p2.name))
                        {
                            count += 1;
                        }
                    }
                    alreadyChecked.Add(p.name);
                    printCart.Add(new Tuple<Product, int>(p,count));
                }
            }
            return printCart;
        }

        public double calculateSubTotal()
        {
            double Subtotal = 0;
            foreach (Tuple<Product,int> tuple in returnCart())
            {
                Subtotal += tuple.Item2 * tuple.Item1.price;
            }
            return Subtotal;
        }

        public double calculateShipping()
        {
            double Shipping = 0;
            foreach (Tuple<Product,int> tuple in returnCart())
            {
                //Shipping fee-ul fiecarui obiect se calculeaza in functie de tara din care provine.
                //*10 deoarece shipping rate-ul se aplica pe 0.1 kg.
                ShippingRate shippingRate = findByCountry(tuple.Item1.shippedFrom);
                Shipping += ((tuple.Item2*tuple.Item1.weight)*shippingRate.rate)*10;
            }
            return Shipping;
        }

        public double calculateTotal()
        {
            double subtotal = calculateSubTotal();
            double shipping = calculateShipping();
            return subtotal + shipping;
        }

        public Tuple<double,double> calculateTotalWithDiscounts()
        {
            double Subtotal = 0;
            double Shipping = calculateShipping();
            foreach (Tuple<Product, int> tuple in returnCart())
            {
                //Verificare daca cumparam doua monitoare.
                if (tuple.Item1.name == "Monitor" && tuple.Item2 == 2)
                {
                    semMonitors = 1;
                }
            }
            if (returnCart().Count() >= 2)
            {
                Shipping = Shipping - 10;
            }
            foreach (Tuple<Product,int> tuple in returnCart())
            {
                //Reducere 10% pentru Keyboard.
                if (tuple.Item1.name == "Keyboard")
                {
                    Subtotal += tuple.Item2 * tuple.Item1.price - (tuple.Item2 * tuple.Item1.price)/10;
                }
                else
                {
                    //Jumatate de pret pentru lampa daca cumperi 2 monitoare.
                    if (semMonitors == 1 && tuple.Item1.name=="Desk Lamp")
                    {
                        Subtotal += (tuple.Item2 * tuple.Item1.price)/2;
                    }
                    else
                    {
                        Subtotal += tuple.Item2 * tuple.Item1.price;
                    }
                }
                if (tuple.Item2 >= 2 && returnCart().Count()==1)
                {
                    Shipping = Shipping - 10;
                }
            }

            return new Tuple<double, double>(Subtotal,Shipping);
        }
        
        public Tuple<double,double> calculateTotalWithDiscountsWithVAT()
        {
            double Subtotal = 0;
            double Shipping = calculateShipping();
            foreach (Tuple<Product, int> tuple in returnCart())
            {
                //Verificare daca cumparam doua monitoare.
                if (tuple.Item1.name == "Monitor" && tuple.Item2 == 2)
                {
                    semMonitors = 1;
                }
            }
            if (returnCart().Count() >= 2)
            {
                Shipping = Shipping - 10;
            }
            foreach (Tuple<Product,int> tuple in returnCart())
            {
                //Reducere 10% pentru Keyboard.
                if (tuple.Item1.name == "Keyboard")
                {
                    Subtotal += (tuple.Item2 * tuple.Item1.price +((tuple.Item2 * tuple.Item1.price)*19)/100) - (tuple.Item2 * tuple.Item1.price)/10;
                }
                else
                {
                    //Jumatate de pret pentru lampa daca cumperi 2 monitoare.
                    if (semMonitors == 1 && tuple.Item1.name=="Desk Lamp")
                    {
                        Subtotal += (tuple.Item2 * tuple.Item1.price+(((tuple.Item2 * tuple.Item1.price)*19)/100))/2;
                    }
                    else
                    {
                        Subtotal += tuple.Item2 * tuple.Item1.price+((tuple.Item2 * tuple.Item1.price)*19)/100;
                    }
                }
                if (tuple.Item2 >= 2 && returnCart().Count()==1)
                {
                    Shipping = Shipping - 10;
                }
            }

            return new Tuple<double, double>(Subtotal,Shipping);
        }
    }
}