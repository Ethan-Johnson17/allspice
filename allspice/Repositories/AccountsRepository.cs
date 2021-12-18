using System.Collections.Generic;
using System.Data;
using System.Linq;
using allspice.Models;
using Dapper;

namespace allspice.Repositories
{
  public class AccountsRepository
  {
    private readonly IDbConnection _db;

    public AccountsRepository(IDbConnection db)
    {
      _db = db;
    }

    internal Account GetByEmail(string userEmail)
    {
      string sql = "SELECT * FROM accounts WHERE email = @userEmail";
      return _db.QueryFirstOrDefault<Account>(sql, new { userEmail });
    }

    internal Account GetById(string id)
    {
      string sql = "SELECT * FROM accounts WHERE id = @id";
      return _db.QueryFirstOrDefault<Account>(sql, new { id });
    }

    internal Account Create(Account newAccount)
    {
      string sql = @"
            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)";
      _db.Execute(sql, newAccount);
      return newAccount;
    }

    internal Account Edit(Account update)
    {
      string sql = @"
            UPDATE accounts
            SET 
              name = @Name,
              picture = @Picture
            WHERE id = @Id;";
      _db.Execute(sql, update);
      return update;
    }

    // REVIEW likely will encounter errors, will need to review and problem solve here
    internal List<RecipeFavoriteViewModel> GetByRecipeFavorite(int recipeId)
    {
      string sql = @"
      SELECT
      a.*,
      acctRecipes.id AS accountRecipeId
      FROM accountrecipes acctRecipes
      JOIN accounts a ON acctRecipes.accountId = a.id
      WHERE acctRecipes.recipeId = @recipeId;";
      return _db.Query<RecipeFavoriteViewModel>(sql, new { recipeId }).ToList();
    }
  }
}
