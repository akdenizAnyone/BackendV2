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

namespace Application.Features.Posts.Queries{
    public class GetPostAnswerQuery : IRequest<IEnumerable<PostDto>>
    {
        public int PostId { get; set; }
        public int? BeforeId { get; set; } = null;
        public int? Count { get; set; } = null;
    }

    public class GetPostAnswerHandler : IRequestHandler<GetPostAnswerQuery, IEnumerable<PostDto>>
    {
        private readonly IMapper _mapper;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetPostAnswerHandler(IApplicationDbContext context, IMapper mapper, ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<PostDto>> Handle(GetPostAnswerQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Posts
                .AsNoTracking()
                .Where(p => p.AnswerToId == request.PostId);

            if(request.BeforeId.HasValue)
                query = query.Where(p => p.Id < request.BeforeId);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.ApplicationUserId == _currentUser.UserId, cancellationToken);

            var result=await query.OrderByDescending(p => p.Created)
                .Take(request.Count ?? 20)
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);
            
            return result;
        }
    }
}