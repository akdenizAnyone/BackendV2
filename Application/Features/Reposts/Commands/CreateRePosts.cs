using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace  Application.Features.RePosts.Commands{
    
    public class CreateRePostCommand:IRequest{
        public int PostId{get;set;}
    }

    public class CreateRePostCommandHandler : IRequestHandler<CreateRePostCommand>
    {

        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateRePostCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<Unit> Handle(CreateRePostCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(d => d.UserName== _currentUser.UserId, cancellationToken);
            if(user == null)
                throw new ApiException("Forbidden Access");

            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == request.PostId, cancellationToken);
            if(post == null)
                throw new ApiException($"Post {post.Id} Not Found");



            var alreadyRePosted = await _context.RePosts.AnyAsync(r => r.PostId == post.Id && r.UserId== user.Id, cancellationToken);
            if(alreadyRePosted) 
                return Unit.Value;
            var rePost = new RePost { PostId = post.Id };

            _context.RePosts.Add(rePost);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;


        }
    }
}