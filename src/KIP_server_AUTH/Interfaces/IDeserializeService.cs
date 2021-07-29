using System.Collections.Generic;
using System.Threading.Tasks;

namespace KIP_server_AUTH.Interfaces
{
    /// <summary>
    /// Interface of deserialize service.
    /// </summary>
    public interface IDeserializeService
    {
        /// <summary>
        /// Json Deserializer.
        /// </summary>
        /// <typeparam name="T">The model type.</typeparam>
        /// <param name="url">The json url.</param>
        /// <returns>Built model from json.</returns>
        Task<IEnumerable<T>> ExecuteAsync<T>(string url);
    }
}
