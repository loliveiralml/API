using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var configuration = app.Configuration;

ProductRepository.Inicializa(configuration);

app.MapGet("/", () => "Mapeamento via API!");

app.MapGet("/user", () => new { Name = "Leandro de OLiveira", Age = "47", Sex = "Masc" });

app.MapPost("/product", (Product product) =>
{
    //return product.code + "-" + product.name;});
    ProductRepository.addProdutos(product);

    return Results.Created($"/product {product.code}", product.code);

});
app.MapGet("/product", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return "Data Inicial : " + dateStart + " - " + "DataFinal : " + dateEnd;
});

app.MapGet("/product/{code}", ([FromRoute] string code) =>
{
    //return "Codigo de Retorno : "+ code;});
    var prd = ProductRepository.listaProdutos(code);
    if (prd != null)
    {
        return Results.Ok(prd);

    }
    else return Results.NotFound(prd);
});

app.MapGet("/getproductbyHeader", (HttpRequest request) =>
{

    return request.Headers["ProdHeaders"].ToString();
});

app.MapPut("/product", (Product product) =>
{
    var lisprd = ProductRepository.listaProdutos(product.code);
    lisprd.name = product.name;
    return Results.Ok();
});


app.MapDelete("/product/{code}", ([FromRoute] string code) =>
{
    var lisprd = ProductRepository.listaProdutos(code);
   
    if (lisprd != null)
    {
         ProductRepository.removeProdutos(lisprd);
            return Results.Ok(lisprd);
    }   return Results.NotFound("Não existe o produto para ser deletado");
 
});

app.MapGet("/configuration/database", (IConfiguration configuration)=>{
    return Results.Ok($"{configuration["database:connection"]}/{configuration["database:Port"]}");
});
app.Run();
