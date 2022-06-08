using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Likes.Commands{
    public class CreateLikeCommand : IRequest
    {
        public int PostId { get; set; }
    }

    public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateLikeCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(d => d.UserName== _currentUser.UserId, cancellationToken);
            if(user == null)
                throw new ApiException("Forbidden Access");

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);
            if(post == null)
                throw new ApiException($"Post {post.Id} Not Found");


            var like = await _context.Likes.FirstOrDefaultAsync(l => l.UserId== user.Id && l.PostId == request.PostId, cancellationToken);
            if(like == null) {
                _context.Likes.Add(new Like{UserId=user.Id,PostId=post.Id});
                await _context.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}