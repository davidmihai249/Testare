using System;
using System.Collections.Generic;
using System.Data;
using ShoppingCart.Model;
using tasks.repository;

namespace ShoppingCart.Repository
{
    public class ShippingRateRepository:IRepositoryShippingRate
    {
        IDictionary<String, string> props;

        public ShippingRateRepository(IDictionary<string, string> props)
        {
            this.props = props;
        }

        public void Save(ShippingRate elem)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(ShippingRate elem)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ShippingRate elem, int ID)
        {
            throw new System.NotImplementedException();
        }

        public ShippingRate FindById(int ID)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ShippingRate> FindAll()
        {
            IDbConnection con = DBUtils.getConnection(props);
            IList<ShippingRate> shippingRates = new List<ShippingRate>();
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,country,rate from shippingrates";
                using (var dataR = comm.ExecuteReader())
                {
                    while (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string country = dataR.GetString(1);
                        int rate = dataR.GetInt32(2);
                        ShippingRate shippingRate = new ShippingRate(country, rate);
                        shippingRate.id = id;
                        shippingRates.Add(shippingRate);
                    }
                }
            }
            return shippingRates;
        }

        public ShippingRate findByCountry(string country)
        {
            IDbConnection con = DBUtils.getConnection(props);
            using (var comm = con.CreateCommand())
            {
                comm.CommandText = "select id,country,rate from shippingrates where country=@country";
                IDbDataParameter paramId = comm.CreateParameter();
                paramId.ParameterName = "@country";
                paramId.Value = country;
                comm.Parameters.Add(paramId);

                using (var dataR = comm.ExecuteReader())
                {
                    if (dataR.Read())
                    {
                        int id = dataR.GetInt32(0);
                        string country2 = dataR.GetString(1);
                        int rate = dataR.GetInt32(2);
                        ShippingRate shippingRate = new ShippingRate(country2, rate);
                        shippingRate.id = id;
                        return shippingRate;
                    }
                }
            }
            return null;
        }
    }
}