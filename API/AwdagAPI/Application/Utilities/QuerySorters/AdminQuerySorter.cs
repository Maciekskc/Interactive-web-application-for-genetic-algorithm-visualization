using System.Linq;
using System.Net;
using Application.Infrastructure.Errors;
using Domain.Models.Entities;
using Persistence.Enums;

namespace Application.Utilities.QuerySorters
{
    public class AdminQuerySorter : QuerySorter
    {
        public static IQueryable<ApplicationUser> GetUsersSortQuery(IQueryable<ApplicationUser> dbQuery, GetUsersOrderBy? orderBy)
        {
            switch (orderBy)
            {
                case GetUsersOrderBy.LastNameAsc:
                    return dbQuery.OrderBy(u => u.LastName);

                case GetUsersOrderBy.LastNameDesc:
                    return dbQuery.OrderByDescending(u => u.LastName);

                case GetUsersOrderBy.EmailAsc:
                    return dbQuery.OrderBy(u => u.Email);

                case GetUsersOrderBy.EmailDesc:
                    return dbQuery.OrderByDescending(u => u.Email);

                case null:
                    return dbQuery.OrderBy(u => u.LastName);

                default:
                    throw new RestException(HttpStatusCode.BadRequest, new[] { InvalidParameterErrorMessage });
            }
        }
    }
}