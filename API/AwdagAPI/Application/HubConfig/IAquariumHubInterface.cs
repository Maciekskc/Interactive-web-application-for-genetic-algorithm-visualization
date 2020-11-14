using System.Threading.Tasks;
using Application.Dtos.Hub;

namespace Application.HubConfig
{
    public interface IAquariumHubInterface
    {
        /// <summary>
        /// Sending Data with fishes and foods from aquarium to client through hub
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task TransferData(HubTransferData data);

        /// <summary>
        /// Sending Message to Clients
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        Task SendMessage(string message);
    }
}
