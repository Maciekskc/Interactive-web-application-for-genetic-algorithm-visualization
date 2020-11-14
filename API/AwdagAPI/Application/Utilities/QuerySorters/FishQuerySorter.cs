using System.Linq;
using System.Net;
using Application.Infrastructure.Errors;
using Domain.Models;
using Persistence.Enums;

namespace Application.Utilities.QuerySorters
{
    public class FishQuerySorter : QuerySorter
    {
        public static IQueryable<Fish> GetFishesSortQuery(IQueryable<Fish> dbQuery, GetFishesOrderBy? orderBy)
        {
            switch (orderBy)
            {
                case GetFishesOrderBy.NameAsc:
                    return dbQuery.OrderBy(f => f.Name);

                case GetFishesOrderBy.NameDesc:
                    return dbQuery.OrderByDescending(f => f.Name);

                case GetFishesOrderBy.VelocityAsc:
                    return dbQuery.OrderBy(f => f.PhysicalStatistic.V);

                case GetFishesOrderBy.VelocityDesc:
                    return dbQuery.OrderByDescending(f => f.PhysicalStatistic.V);

                case GetFishesOrderBy.TimeAliveAsc:
                    return dbQuery.OrderByDescending(f => f.LifeTimeStatistic.BirthDate);

                case GetFishesOrderBy.TimeAliveDesc:
                    return dbQuery.OrderBy(f => f.LifeTimeStatistic.BirthDate);

                case null:
                    return dbQuery.OrderBy(f => f.Name);

                default:
                    throw new RestException(HttpStatusCode.BadRequest, new[] { InvalidParameterErrorMessage });
            }
        }
    }
}