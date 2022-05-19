using System;
using System.Collections.Generic;
using System.Data;
using ShoppingCart.Model;
using tasks.repository;

namespace ShoppingCart
{
    public class ProductRepository:IRepositoryProduct
    {
        IDictionary<String, string> props;

        public ProductRepository(IDictionary<string, string> props)
        {
            this.props = props;
        }

        public void Save(Product elem)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Product elem)
        {
            throw new System.NotImplementedException();
        }

        public void Update(Product elem, int ID)
        {
            throw new System.NotImplementedException();
        }

        public Product FindById(int ID)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Product> FindAll()
        {
            IDbConnection con = DBUtils.getConnection(props);
            IList<Product> products = new List<Product>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,name,price,shippedfrom,weight from products";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string name = dataR.GetString(1);
                        double price = dataR.GetDouble(2);
                        string shippedfrom = dataR.GetString(3);
                        double weight = dataR.GetDouble(4);
                        Product product = new Product(name, price, shippedfrom, weight);
                        product.id = id;
                        products.Add(product);
                    }
                }
            }
            return products;
        }

        public Product findByProductName(string name)
        {
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,name,price,shippedfrom,weight from products where name=@name";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@name";
                paramId.Value = name;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string name2 = dataR.GetString(1);
                        double price = dataR.GetDouble(2);
                        string shippedfrom = dataR.GetString(3);
                        double weight = dataR.GetDouble(4);
                        Product product = new Product(name2, price, shippedfrom, weight);
                        product.id = id;
                        return product;
                    }
                }
            }
            return null;
        }
    }
}