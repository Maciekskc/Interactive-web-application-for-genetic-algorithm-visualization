using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos.NewFolder.Response;
using Application.Utilities;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IFishService
    {
        /// <summary>
        /// Metoda zwracająca wszystkie ryby z wybranego akwarium
        /// </summary>
        /// <param name="aquariumId"></param>
        /// <returns></returns>
        Task<ServiceResponse<List<GetFishFromAquariumResponse>>> GetFishesFromAquarium(Guid aquariumId);
    }
}
