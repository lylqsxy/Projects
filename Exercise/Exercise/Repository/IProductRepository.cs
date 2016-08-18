using System.Collections.Generic;
using Exercise.Model;

namespace Exercise.Repository
{
    interface IProductRepository
    {
        List<Product> InitialiseProduct();
    }
}