using System;
using System.Threading.Tasks;
using allspice.Models;
using allspice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace allspice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class IngredientsController : ControllerBase
  {
    private readonly IngredientsService _ins;
    public IngredientsController(IngredientsService ins)
    {
      _ins = ins;
    }

    // [HttpGet]
    // public ActionResult<IEnumerable<Ingredient>> Get()
    // {
    //   try
    //   {
    //     var ingredients = _ins.Get();
    //     return Ok(ingredients);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    [HttpGet("{id}")]
    public ActionResult<Ingredient> Get(int id)
    {
      try
      {
        var ingredient = _ins.Get(id);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    // [HttpPost]
    // [Authorize]
    // public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient newIngredient)
    // {
    //   try
    //   {
    //     Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
    //     Ingredient ingredient = _ins.Create(newIngredient);
    //     return Ok(ingredient);
    //   }
    //   catch (Exception e)
    //   {
    //     return BadRequest(e.Message);
    //   }
    // }

    [HttpPut("{id}")]
    public ActionResult<Ingredient> Create([FromBody] Ingredient updatedIngredient, int id)
    {
      try
      {
        updatedIngredient.Id = id;
        Ingredient ingredient = _ins.Update(updatedIngredient);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _ins.RemoveIngredient(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}