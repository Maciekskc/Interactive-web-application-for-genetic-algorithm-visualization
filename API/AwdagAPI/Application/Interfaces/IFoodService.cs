using System.Threading.Tasks;
using Application.Dtos.Food.Requests;
using Application.Utilities;

namespace Application.Interfaces
{
    public interface IFoodService
    {
        Task<ServiceResponse> CreateAdditionalFoodAsync(int aquariumId, CreateFoodRequest request = null);
    }
}
