using MediatR;

using Application.Features.Posts.Dtos;
using AutoMapper;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using AutoMapper.QueryableExtensions;
using Application.Exceptions;
using System.Threading.Tasks;
using System.Linq;
using Domain.Entities;
using Application.Extensions;
using System.Collections.Generic;

namespace Application.Features.Posts.Queries{
    public class GetUserPostsQuery : IRequest<IEnumerable<PostDto>>
    {
        public int UserId { get; set; }
        public int? BeforeId { get; set; } = null;
        public int? Count { get; set; } = null;
    }

    public class GetUserPostsQueryHandler : IRequestHandler<GetUserPostsQuery, IEnumerable<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public int Id { get; set; }
        public GetUserPostsQueryHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
        }


        async Task<IEnumerable<PostDto>> IRequestHandler<GetUserPostsQuery, IEnumerable<PostDto>>.Handle(GetUserPostsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Posts
                .AsNoTracking()
                .Where(p => !p.AnswerToId.HasValue)
                .Where(Post.IsRePostedBy(request.UserId).Or(p => p.UserId== request.UserId));

            if(request.BeforeId.HasValue)
                query = query.Where(p => p.Id < request.BeforeId);

            // var user = await _context.Users.FirstOrDefaultAsync(u => u.ApplicationUserId == _currentUser.UserId, cancellationToken);

            return await query.OrderByDescending(p => p.Created)
                .Take(request.Count ?? 20)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
        }
    }
}