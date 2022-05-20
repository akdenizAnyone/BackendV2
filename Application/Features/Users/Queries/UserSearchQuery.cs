using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Users.Dtos;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Linq;
using Application.Interfaces;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
namespace Application.Features.Users.Queries
{
    public class UserSearchQuery : IRequest<IEnumerable<UserSearchDto>>
    {
        public string Search { get; set; }

    }

    public class UserSearchQueryHandler : IRequestHandler<UserSearchQuery, IEnumerable<UserSearchDto>>
    {
        private readonly IUserRepositoryAsync _userRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserSearchQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper, IApplicationDbContext context,ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _currentUserService=currentUserService;
            _context = context;
            _mapper = mapper;
        }
        async Task<IEnumerable<UserSearchDto>> IRequestHandler<UserSearchQuery, IEnumerable<UserSearchDto>>.Handle(UserSearchQuery request, CancellationToken cancellationToken)
        {
            var result = await _context.Users.Where(u => u.UserName.ToLower().Contains(request.Search.ToLower())
                || u.FullName.ToLower().Contains(request.Search.ToLower()))
                .OrderByDescending(u=>u.UserName.ToLower()==request.Search.ToLower())
                .ThenByDescending(u=>u.Followers.Any(d=>d.Follower.ApplicationUserId==_currentUserService.UserId))
                .ProjectTo<UserSearchDto>(_mapper.ConfigurationProvider)
                .Take(10)
                .ToListAsync();
            // var search= 
            return result;
        }
    }
}