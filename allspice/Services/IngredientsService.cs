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

    internal Ingredient Get(int id)
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

    internal void RemoveIngredient(int id, string userId)
    {
      Ingredient ingredient = Get(id);
      if (ingredient.CreatorId != userId)
      {
        throw new Exception("You are not allowed to remove this ingredient");
      }
      _repo.RemoveIngredient(id);
    }

    internal Ingredient Update(Ingredient updatedIngredient)
    {
      Ingredient oldIngredient = Get(updatedIngredient.Id);
      updatedIngredient.InName = updatedIngredient.InName != null ? updatedIngredient.InName : oldIngredient.InName;
      updatedIngredient.Quantity = updatedIngredient.Quantity != null ? updatedIngredient.Quantity : oldIngredient.Quantity;
      updatedIngredient.RecipeId = updatedIngredient.RecipeId != 0 ? updatedIngredient.RecipeId : oldIngredient.RecipeId;
      updatedIngredient.CreatorId = updatedIngredient.CreatorId != null ? updatedIngredient.CreatorId : oldIngredient.CreatorId;
      return _repo.Update(updatedIngredient);
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