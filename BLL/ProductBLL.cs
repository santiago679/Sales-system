using SalesSystem.DAL;
using SalesSystem.Entities;
using System;
using System.Collections.Generic;

namespace SalesSystem.BLL
{
    public class ProductBLL
    {
        private readonly ProductDAL productDAL;

        public ProductBLL()
        {
            productDAL = new ProductDAL();
        }

        public string AddProduct(Product product)
        {
            if (product == null)
                return "El producto no puede ser nulo";

            if (string.IsNullOrWhiteSpace(product.ProductName))
                return "El nombre del producto es obligatorio";

            if (product.Price <= 0)
                return "El precio debe ser mayor que 0";

            try
            {
                productDAL.AddProduct(product);
                return "Producto añadido correctamente ✅";
            }
            catch (Exception ex)
            {
                return $"Ha ocurrido un error al añadir el producto: {ex.Message}";
            }
        }



        public List<Product> GetAllProducts()
        {
            return productDAL.GetAllProducts();
        }

        public Product GetProductById(int id)
        {
            return productDAL.GetProductById(id);
        }

        public void UpdateProduct(Product product)
        {
            productDAL.UpdateProduct(product);
        }

        public void DeleteProduct(int id)
        {
            productDAL.DeleteProduct(id);
        }
    }
}
