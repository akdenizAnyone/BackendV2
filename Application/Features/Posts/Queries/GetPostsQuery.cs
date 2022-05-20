using MediatR;

using Application.Features.Posts.Dtos;
using AutoMapper;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using AutoMapper.QueryableExtensions;
using Application.Exceptions;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;
using Application.Extensions;

namespace Application.Features.Posts.Queries{
    public class GetPostsQuery : IRequest<IEnumerable<PostDto2>>
    {
        public int? BeforeId { get; set; } = null;
        public int? Count { get; set; } = null;
    }

    public class GetPostsQueryHandler : IRequestHandler<GetPostsQuery, IEnumerable<PostDto2>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetPostsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<PostDto2>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ApplicationUserId == _currentUser.UserId, cancellationToken);
            var postsQuery = _context.Posts.AsNoTracking().Where(p => !p.AnswerToId.HasValue);

            if(user != null)
            {
                //Todo : it's not working as want
                postsQuery = postsQuery.Where(Post.AuthorFollowedBy(user.Id));
                postsQuery = postsQuery.Where(Post.AuthorFollowedBy(user.Id)
                    .Or(Post.LikedBySomeoneFollowedBy(user.Id)
                    .Or(Post.RePostedBySomeoneFollowedBy(user.Id))
                    .Or(p => p.UserId== user.Id)));
            }
            if(request.BeforeId.HasValue)
                postsQuery = postsQuery.Where(p => p.Id < request.BeforeId);

            return (IEnumerable<PostDto2>)await postsQuery.OrderByDescending(p => p.Created)
                .Take(request.Count ?? 20)
                .ProjectTo<PostDto2>(_mapper.ConfigurationProvider,new { userId = user?.Id ?? 0})
                .ToListAsync(cancellationToken);
        }

    }
}