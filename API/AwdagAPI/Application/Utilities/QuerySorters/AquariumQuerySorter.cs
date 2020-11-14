using System.Linq;
using System.Net;
using Application.Infrastructure.Errors;
using Domain.Models.Entities;
using Persistence.Enums;

namespace Application.Utilities.QuerySorters
{
    public class AquariumQuerySorter : QuerySorter
    {
        public static IQueryable<Aquarium> GetAquariumsSortQuery(IQueryable<Aquarium> dbQuery, GetAquariumsOrderBy? orderBy)
        {
            switch (orderBy)
            {
                case GetAquariumsOrderBy.IdAsc:
                    return dbQuery.OrderBy(u => u.Id);

                case GetAquariumsOrderBy.IdDesc:
                    return dbQuery.OrderByDescending(u => u.Id);

                case GetAquariumsOrderBy.CapacityAsc:
                    return dbQuery.OrderBy(u => u.Capacity);

                case GetAquariumsOrderBy.CapacityDesc:
                    return dbQuery.OrderByDescending(u => u.Capacity);

                case null:
                    return dbQuery.OrderBy(u => u.Id);

                default:
                    throw new RestException(HttpStatusCode.BadRequest, new[] { InvalidParameterErrorMessage });
            }
        }
    }
}
