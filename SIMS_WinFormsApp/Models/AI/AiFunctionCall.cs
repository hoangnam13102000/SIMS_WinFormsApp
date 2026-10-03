namespace SIMS_WinFormsApp.Models.AI
{
    public sealed class AiFunctionCall
    {
        public AiFunctionCall(string name, string argumentsJson)
        {
            Name = name;
            ArgumentsJson = argumentsJson;
        }

        public string Name { get; private set; }
        public string ArgumentsJson { get; private set; }
    }
}
