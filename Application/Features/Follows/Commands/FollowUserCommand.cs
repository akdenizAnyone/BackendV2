using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Follows.Commands{
    public class FollowUserCommand : IRequest
    {
        public int UserId { get; set; }
    }
    public class FollowUserCommandHandler : IRequestHandler<FollowUserCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public FollowUserCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(FollowUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(d => d.ApplicationUserId == _currentUser.UserId, cancellationToken);
            if(user == null)
                throw new ApiException("Forbidden Access ");

            var followed = await _context.Users.FirstOrDefaultAsync(d => d.Id == request.UserId, cancellationToken);
            if(followed == null)
                throw new ApiException($"User {user.Id} Not Found");

            var alreadyFollowed = await _context.Follows.AnyAsync(f => f.FollowId== request.UserId && f.FollowerId == user.Id, cancellationToken);
            if(alreadyFollowed)
                return Unit.Value;

            var follow = new Follow { FollowId = followed.Id, FollowerId = user.Id};
            _context.Follows.Add(follow);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}