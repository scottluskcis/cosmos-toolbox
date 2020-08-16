namespace CosmosToolbox.App
{
    public interface IApplicationArgs
    {
         bool OnlyValidate { get; }
         bool CreateContainers { get; }
         bool CreateDatabase { get; }
         bool LoadSeedData { get; }
         bool LoadScripts { get; }
         bool ConfigureAll { get; }
    }
}