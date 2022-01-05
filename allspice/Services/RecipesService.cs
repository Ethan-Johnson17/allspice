using System;
using System.Collections.Generic;
using allspice.Models;
using allspice.Repositories;

namespace allspice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _repo;
    public RecipesService(RecipesRepository repo)
    {
      _repo = repo;
    }
    internal List<Recipe> Get()
    {
      return _repo.GetAll();
    }

    internal Recipe Get(int id)
    {
      Recipe found = _repo.Get(id);
      if (found == null)
      {
        throw new Exception("Invalid Id");
      }
      return found;
    }

    internal Recipe Create(Recipe newRecipe)
    {
      return _repo.Create(newRecipe);
    }

    internal Recipe Update(Recipe updateRecipe)
    {
      Recipe oldRecipe = Get(updateRecipe.Id);
      updateRecipe.Title = updateRecipe.Title != null ? updateRecipe.Title : oldRecipe.Title;
      updateRecipe.Subtitle = updateRecipe.Subtitle != null ? updateRecipe.Subtitle : oldRecipe.Subtitle;
      updateRecipe.Category = updateRecipe.Category != null ? updateRecipe.Category : oldRecipe.Category;
      updateRecipe.imgUrl = updateRecipe.imgUrl != null ? updateRecipe.imgUrl : oldRecipe.imgUrl;
      return _repo.Update(updateRecipe);
    }
    internal void Remove(int id, string userId)
    {
      Recipe recipe = Get(id);
      if (recipe.CreatorId != userId)
      {
        throw new Exception("You are not allowed to remove this recipe");
      }
      _repo.Remove(id);
    }

    internal List<RecipeFavoriteViewModel> GetFavorites(string id)
    {
      return _repo.getFavorites(id);
    }

    internal RecipeFavoriteViewModel CreateFavorite(RecipeFavoriteViewModel newFavorite)
    {
      return _repo.CreateFavorite(newFavorite);
    }

    internal List<RecipeTryViewModel> GetTries()
    {
      return _repo.getTries();
    }
    internal RecipeTryViewModel CreateTryRecipe(RecipeTryViewModel newTry)
    {
      return _repo.CreateTryRecipe(newTry);
    }

  }
}