namespace Genzai.CosmosDb.Model
{
    /// <summary>
    /// Command base, provides id and partitionKey for CosmosDb access.
    /// </summary>
    public abstract class CosmosCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CosmosCommand"/> class.
        /// </summary>
        /// <param name="id">Entity Id.</param>
        /// <param name="partitionKey">Partition key.</param>
        protected CosmosCommand(string id, string partitionKey)
        {
            this.Id = id;
            this.PartitionKey = partitionKey;
        }

        /// <summary>
        /// Cosmos Entity ID.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// Cosmos Entity partition key.
        /// </summary>
        public string PartitionKey { get; }
    }
}