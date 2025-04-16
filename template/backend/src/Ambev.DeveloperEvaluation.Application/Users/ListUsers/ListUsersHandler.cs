using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    public class ListUsersHandler :IRequestHandler<ListUsersCommand, List<GetUserResult>>
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ListUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUserResult>> Handle(ListUsersCommand request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsersAsync(request.PageNumber, request.PageSize, cancellationToken);

            var results = users.Select(x => _mapper.Map<GetUserResult>(x)).ToList();

            return results;
        }
    }
   
}
