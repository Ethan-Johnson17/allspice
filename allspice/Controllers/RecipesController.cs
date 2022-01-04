using System;
using System.Collections.Generic;
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
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _rs;
    private readonly AccountService _acctService;
    private readonly IngredientsService _ins;
    private readonly StepsService _ss;

    public RecipesController(RecipesService rs, AccountService acctService, IngredientsService ins, StepsService ss)
    {
      _rs = rs;
      _acctService = acctService;
      _ins = ins;
      _ss = ss;
    }
    #region Recipes

    [HttpGet]
    public ActionResult<IEnumerable<Recipe>> get()
    {
      try
      {
        var recipes = _rs.Get();
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Recipe> Get(int id)
    {
      try
      {
        var recipe = _rs.Get(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newRecipe.CreatorId = userInfo.Id;
        Recipe recipe = _rs.Create(newRecipe);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    [Authorize]

    public ActionResult<Recipe> Create([FromBody] Recipe updateRecipe, int id)
    {
      try
      {
        updateRecipe.Id = id;
        Recipe recipe = _rs.Update(updateRecipe);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<Recipe>> Remove(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        _rs.Remove(id, userInfo.Id);
        return Ok("Deleted");
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    #endregion

    #region Ingredients

    [HttpGet("{id}/ingredients")]
    public ActionResult<IEnumerable<Ingredient>> GetIngredients()
    {
      try
      {
        var ingredients = _ins.Get();
        return Ok(ingredients);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPost("{id}/ingredients")]
    [Authorize]
    public async Task<ActionResult<Ingredient>> Create([FromBody] Ingredient newIngredient)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newIngredient.CreatorId = userInfo.Id;
        Ingredient ingredient = _ins.Create(newIngredient);
        return Ok(ingredient);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    #endregion

    #region Steps
    [HttpGet("{id}/steps")]
    public ActionResult<IEnumerable<Step>> GetStep()
    {
      try
      {
        var steps = _ss.Get();
        return Ok(steps);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }


    [HttpPost("{id}/steps")]
    [Authorize]
    public async Task<ActionResult<Step>> CreateStep([FromBody] Step newStep)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newStep.CreatorId = userInfo.Id;
        Step step = _ss.Create(newStep);
        return Ok(step);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
    #endregion
  }
}