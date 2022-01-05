using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;

    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Recipe> GetAll()
    {
      string sql = "SELECT * FROM recipes;";
      return _db.Query<Recipe>(sql).ToList();
    }

    internal Recipe Get(int id)
    {
      string sql = "SELECT * FROM recipes WHERE id = @id;";
      return _db.QueryFirstOrDefault<Recipe>(sql, new { id });
    }

    internal Recipe Create(Recipe newRecipe)
    {
      string sql = @"
      INSERT INTO recipes
      (title, subtitle, category, imgUrl, creatorId)
      VALUES
      (@title, @subtitle, @category, @imgUrl, @CreatorId);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newRecipe);
      newRecipe.Id = id;
      return newRecipe;
    }

    internal void Remove(int id)
    {
      string sql = "DELETE FROM recipes WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }

    internal List<RecipeFavoriteViewModel> getFavorites(string id)
    {
      string sql = @"
      SELECT 
      f.*,
      r.* 
      FROM favorites f
      JOIN recipes r ON r.id = f.recipeId
      WHERE f.accountId = @id
      ;";
      return _db.Query<RecipeFavoriteViewModel>(sql, new { id }).ToList();
    }

    internal Recipe Update(Recipe updateRecipe)
    {
      string sql = @"
      UPDATE recipes
      SET
      Title = @Title,
      Subtitle = @Subtitle,
      Category = @Category,
      imgUrl = @ImgUrl
      WHERE id = @Id
      ;";
      int rows = _db.Execute(sql, updateRecipe);
      if (rows <= 0)
      {
        throw new Exception("Recipe was not updated");
      }
      return updateRecipe;
    }

    internal RecipeFavoriteViewModel CreateFavorite(RecipeFavoriteViewModel newFavorite)
    {
      string sql = @"
      INSERT INTO favorites
      (recipeId, accountId, favoriteId)
      VALUES
      (@recipeId, @accountId, @favoriteId);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newFavorite);
      newFavorite.FavoriteId = id;
      return newFavorite;
    }

    internal List<RecipeTryViewModel> getTries()
    {
      string sql = "SELECT * FROM tries;";
      return _db.Query<RecipeTryViewModel>(sql).ToList();
    }

    internal RecipeTryViewModel CreateTryRecipe(RecipeTryViewModel newTry)
    {
      string sql = @"
      INSERT INTO tries
      (recipeId, accountId, tryId)
      VALUES
      (@recipeId, @accountId, @tryId);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newTry);
      newTry.TryId = id;
      return newTry;
    }
  }
}