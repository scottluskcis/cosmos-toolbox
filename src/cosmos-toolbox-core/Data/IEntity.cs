namespace CosmosToolbox.Core.Data
{
    public interface IEntity
    {
        string Type { get; }
        string PartitionKey { get; }
        string GetId();
    }
}