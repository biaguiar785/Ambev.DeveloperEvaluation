using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    public class ListUsersCommand : IRequest<List<GetUserResult>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public ListUsersCommand(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
