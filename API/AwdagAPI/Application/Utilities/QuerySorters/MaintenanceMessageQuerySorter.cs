using System.Linq;
using System.Net;
using Application.Infrastructure.Errors;
using Domain.Models.Entities;
using Persistence.Enums;

namespace Application.Utilities.QuerySorters
{
    public class MaintenanceMessageQuerySorter : QuerySorter
    {
        public static IQueryable<MaintenanceMessage> GetMessagesSortQuery(IQueryable<MaintenanceMessage> dbQuery, GetMessagesOrderBy? orderBy)
        {
            switch (orderBy)
            {
                case GetMessagesOrderBy.StartDateAsc:
                    return dbQuery.OrderBy(m => m.StartDate);

                case GetMessagesOrderBy.StartDateDesc:
                    return dbQuery.OrderByDescending(m => m.StartDate);

                case GetMessagesOrderBy.EndDateAsc:
                    return dbQuery.OrderBy(m => m.EndDate);

                case GetMessagesOrderBy.EndDateDesc:
                    return dbQuery.OrderByDescending(m => m.EndDate);

                case null:
                    return dbQuery.OrderBy(m => m.StartDate);

                default:
                    throw new RestException(HttpStatusCode.BadRequest, new[] { InvalidParameterErrorMessage });
            }
        }
    }
}