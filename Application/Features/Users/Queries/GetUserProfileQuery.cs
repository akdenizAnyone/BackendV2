using MediatR;
using Application.Features.Users.Dtos;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using AutoMapper;
using Application.Exceptions;
using Application.Wrappers;
using Domain.Entities;
using Application.Interfaces;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Application.Features.Users.ViewModels;

namespace Application.Features.Users.Queries
{
    public class GetUserProfileQuery : IRequest<UserProfileVM>
    {
        public string Username { get; set; }
    }
    public class GetUserProfileQueryHandler : IRequestHandler<GetUserProfileQuery, UserProfileVM>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepositoryAsync _userRepository;
        private readonly IApplicationDbContext _context;

        public GetUserProfileQueryHandler(IUserRepositoryAsync userRepository, IMapper mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _context = context;
        }


        async Task<UserProfileVM> IRequestHandler<GetUserProfileQuery, UserProfileVM>.Handle(GetUserProfileQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByUsername(request.Username);
            if (user == null)
                throw new ApiException("User Not Found");

            // var followedCount = await _userRepository.GetUserFollowedCount(user.Id);
            // var followersCount = await _userRepository.GetUserFollwersCount(user.Id);
            // var postCount = await _userRepository.GetUserPostCount(user.Id);



            // var userResult=_context.Users
            //     .ProjectTo<UserProfileVM>(_mapper.ConfigurationProvider)
            //     .FirstOrDefaultAsync(u=>u.Username==request.username,cancellationToken);

            // if(user==null)
            //     throw new ApiException($"User Not found {request.username}");


            var followedCount=_context.Follows.Where(f=>f.FollowerId==user.Id).Count(); 
            var followersCount=_context.Follows.Where(f=>f.FollowId==user.Id).Count(); 
            var postCount =_context.Posts.Where(p=>p.CreatedBy==user.ApplicationUserId).Count();

            var userProfileViewModel=_mapper.Map<UserProfileVM>(user);
            userProfileViewModel.FollowedCount=followedCount;
            userProfileViewModel.FollowersCount=followersCount;
            userProfileViewModel.PostsCount=postCount;

            return userProfileViewModel;
        }
    }


}
