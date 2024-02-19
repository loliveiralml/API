using System.Reflection.Metadata.Ecma335;

public static class ProductRepository
{

   public static List<Product> Products;

   public static void Inicializa(IConfiguration configuration)
   { //ingestão de dependencia para parametros do appsetting
      var prd = configuration.GetSection("Products").Get<List<Product>>(); //pega valor de produto listado no appsetting
      Products = prd;
   }
   public static void addProdutos(Product product)
   {

      if (Products == null)
         Products = new List<Product>();
      Products.Add(product);

      Results.Created("/products" + product.code, product.code);
   }

   public static Product listaProdutos(string code)
    {
      return Products.FirstOrDefault(p => p.code == code);

   }

   public static void removeProdutos(Product produto)
   {
      Products.Remove(produto);

   }

}