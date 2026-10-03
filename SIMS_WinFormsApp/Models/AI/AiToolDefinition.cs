namespace SIMS_WinFormsApp.Models.AI
{
    public sealed class AiToolDefinition
    {
        public AiToolDefinition(string name, string description, string parametersSchemaJson)
        {
            Name = name;
            Description = description;
            ParametersSchemaJson = parametersSchemaJson;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ParametersSchemaJson { get; private set; }
    }
}
