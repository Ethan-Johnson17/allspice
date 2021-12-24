namespace allspice.Models
{
  public class Step
  {
    public int Id { get; set; }
    public int StepOrder { get; set; }
    public string bodyText { get; set; }
    public int RecipeId { get; set; }
    public string CreatorId { get; set; }
  }
}