using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace  Application.Features.RePosts.Commands{
    
    public class RemoveRePostCommand:IRequest{
        public int PostId{get;set;}
    }

    public class RemoveRePostCommandHandler : IRequestHandler<RemoveRePostCommand>
    {

        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public RemoveRePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(RemoveRePostCommand request, CancellationToken cancellationToken)
        {

            var user = await _context.Users.FirstOrDefaultAsync(d => d.UserName== _currentUser.UserId, cancellationToken);

            if(user == null)
                throw new ApiException("Forbidden Acces");

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);
            if(post == null)
                throw new ApiException($"Post {post.Id} Not Found");

            var rePost = await _context.RePosts.FirstOrDefaultAsync(r => r.UserId== user.Id && r.PostId == request.PostId, cancellationToken);
            if(rePost != null) {
                _context.RePosts.Remove(rePost);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return Unit.Value;


        }
    }
}