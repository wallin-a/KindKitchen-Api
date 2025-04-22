namespace recipe_app_api.Data.Entities
{
    public class StepEntity
    {
        public int Id { get; set; }
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public required string Instruction { get; set; }
    }
}
