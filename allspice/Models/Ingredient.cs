namespace allspice.Models
{
  public class Ingredient
  {
    public int Id { get; set; }
    public string InName { get; set; }
    public string Quantity { get; set; }
    public int RecipeId { get; set; }
    public string CreatorId { get; set; }

  }
}