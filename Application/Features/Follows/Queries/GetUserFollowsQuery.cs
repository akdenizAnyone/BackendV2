using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Follows.ViewModels;
using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Follows.Queries{
    public class GetUserFollowsQuery : IRequest<FollowsVM>
    {
        public int UserId { get; set; }
    }
    public class GetUserFollowsQueryHandler : IRequestHandler<GetUserFollowsQuery, FollowsVM>
    {
        private readonly IApplicationDbContext _context;

        public GetUserFollowsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FollowsVM> Handle(GetUserFollowsQuery request, CancellationToken cancellationToken)
        {
            var follows = await _context.Follows
                .AsNoTracking()
                .Where(f => f.FollowId == request.UserId || f.FollowerId == request.UserId)
                .ToListAsync(cancellationToken);

            return new FollowsVM {
                FollowedIds = follows.Where(f => f.FollowerId == request.UserId).Select(f => f.FollowId),
                FollowerIds = follows.Where(f => f.FollowId == request.UserId).Select(f => f.FollowerId)
            };
        }
    }
}