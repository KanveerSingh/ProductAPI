using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace ProductAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ProductController : ControllerBase
  {
    
    private readonly IMongoCollection<Model.Product> _producttblCollection;

    public ProductController(IMongoDatabase database)
    {
      _producttblCollection = database.GetCollection<Model.Product>("ProductDB");
    }

    [HttpGet]
    public IEnumerable<Model.Product> Get()
    {
      var result = _producttblCollection.Find(_ => true).ToList();
      return result;
    }

    [HttpGet]
    [Route("producttest")]
    public string TestPage()
    {
      return "Product Test page hit  successfully";
    }

    [HttpPost]
    public IActionResult SaveProduct([FromBody] Model.Product product)
    {
      try
      {
        _producttblCollection.InsertOne(product);
        return Ok("Product created successfully.");
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"An error occurred while saving the product in productdb: {ex.Message}");
      }
    }
  }
}
