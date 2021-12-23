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
      (title, subtitle, category, creatorId)
      VALUES
      (@title, @subtitle, @category, @CreatorId);
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

    internal List<RecipeFavoriteViewModel> getFavorites()
    {
      string sql = "SELECT * FROM favorites;";
      return _db.Query<RecipeFavoriteViewModel>(sql).ToList();
    }

    internal RecipeFavoriteViewModel CreateFavorite(RecipeFavoriteViewModel newFavorite)
    {
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
    }
  }
}