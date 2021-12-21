using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class IngredientsService
  {
    private readonly IngredientsRepository _repo;
    public IngredientsService(IngredientsRepository repo)
    {
      _repo = repo;
    }
    internal List<Ingredient> Get()
    {
      return _repo.GetAll();
    }

    internal object Get(int id)
    {
      Ingredient found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Ingredient Create(Ingredient newIngredient)
    {
      return _repo.Create(newIngredient);
    }

    // internal void Remove(int id, string userId)
    // {
    //   Ingredient ingredient = Get(id);
    //   if (ingredient.CreatorId != userId)
    //   {
    //     throw new Exception("You are not allowed to remove this recipe");
    //   }
    //   _repo.Remove(id);
    // }
  }
}