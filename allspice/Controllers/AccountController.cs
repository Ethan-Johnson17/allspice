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
  [Route("[controller]")]
  public class AccountController : ControllerBase
  {
    private readonly AccountService _accountService;
    private readonly RecipesService _rs;


    public AccountController(AccountService accountService, RecipesService rs)
    {
      _accountService = accountService;
      _rs = rs;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<Account>> Get()
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        return Ok(_accountService.GetOrCreateProfile(userInfo));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("/[controller]/favorites")]
    [Authorize]

    public ActionResult<IEnumerable<RecipeFavoriteViewModel>> GetFavorites()
    {
      try
      {
        List<RecipeFavoriteViewModel> favorites = _rs.GetFavorites();
        return Ok(favorites);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("/[controller]/favorites")]
    [Authorize]
    public async Task<ActionResult<RecipeFavoriteViewModel>> Create([FromBody] RecipeFavoriteViewModel newFavorite)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newFavorite.AccountId = userInfo.Id;
        RecipeFavoriteViewModel favorite = _rs.CreateFavorite(newFavorite);
        return Ok(favorite);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("/[controller]/recipes-to-try")]
    [Authorize]

    public ActionResult<IEnumerable<RecipeTryViewModel>> GetTries()
    {
      try
      {
        List<RecipeTryViewModel> tries = _rs.GetTries();
        return Ok(tries);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost("/[controller]/recipes-to-try")]
    [Authorize]
    public async Task<ActionResult<RecipeTryViewModel>> CreateTry([FromBody] RecipeTryViewModel newTry)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newTry.AccountId = userInfo.Id;
        RecipeTryViewModel tryRecipe = _rs.CreateTryRecipe(newTry);
        return Ok(tryRecipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }


}