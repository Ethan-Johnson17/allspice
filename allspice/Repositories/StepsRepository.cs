using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class StepsRepository
  {
    private readonly IDbConnection _db;
    public StepsRepository(IDbConnection db)
    {
      _db = db;
    }
    internal List<Step> GetAll()
    {
      string sql = "SELECT * FROM steps;";
      return _db.Query<Step>(sql).ToList();
    }

    internal Step Create(Step newStep)
    {
      string sql = @"
      INSERT INTO steps
      (stepOrder, bodyText, recipeId, creatorId)
      VALUES
      (@stepOrder, @bodyText, @recipeId, @creatorId);
      SELECT LAST_INSERT_ID()
      ;";
      int id = _db.ExecuteScalar<int>(sql, newStep);
      newStep.Id = id;
      return newStep;
    }

    internal Step Get(int id)
    {
      string sql = "SELECT * FROM steps WHERE id = @id;";
      return _db.QueryFirstOrDefault<Step>(sql, new { id });
    }

    internal void RemoveStep(int id)
    {
      string sql = "DELETE FROM steps WHERE id = @id LIMIT 1;";
      _db.Execute(sql, new { id });
    }
  }
}