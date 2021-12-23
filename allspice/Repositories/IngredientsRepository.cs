using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class IngredientsRepository
  {
    private readonly IDbConnection _db;
    public IngredientsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Ingredient> GetAll()
    {
      string sql = "SELECT * FROM ingredients;";
      return _db.Query<Ingredient>(sql).ToList();
    }

    internal Ingredient Get(int id)
    {
      string sql = "SELECT * FROM ingredients WHERE id = @id;";
      return _db.QueryFirstOrDefault<Ingredient>(sql, new { id });
    }

    internal Ingredient Create(Ingredient newIngredient)
    {
      string sql = @"
      INSERT INTO ingredients
      (inName, quantity, recipeId, creatorId)
      VALUES
      (@inName, @quantity, @recipeId, @creatorId);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newIngredient);
      newIngredient.Id = id;
      return newIngredient;
    }

    internal void RemoveIngredient(int id)
    {
      string sql = "DELETE FROM ingredients WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}