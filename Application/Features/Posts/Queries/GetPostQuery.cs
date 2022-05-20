using MediatR;

using Application.Features.Posts.Dtos;
using AutoMapper;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using AutoMapper.QueryableExtensions;
using Application.Exceptions;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using System.Linq;

namespace Application.Features.Posts.Queries
{
    public class GetPostQuery : IRequest<PostDto>
    {
        public int Id { get; set; }
    }

    public class GetPostQueryHandler : IRequestHandler<GetPostQuery, PostDto>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepositoryAsync _postRepositoryAsync;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public GetPostQueryHandler(IApplicationDbContext context, IMapper mapper, IPostRepositoryAsync postRepositoryAsync, ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
            _mapper = mapper;
            _postRepositoryAsync = postRepositoryAsync;
            _context = context;
        }

        public async Task<PostDto> Handle(GetPostQuery request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.ApplicationUserId == _currentUser.UserId, cancellationToken);

            var post = await _context.Posts
                .ProjectTo<PostDto>(_mapper.ConfigurationProvider, new { userId = user?.Id ?? 0 })
                .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

            if (post == null)
                throw new ApiException($"Post ${request.Id} Not Found");


            //LikedByMe,Likes,RePostedByMw,RePosts,Answers map them manually.
            if (user != null)
            {
                post.LikedByMe = _postRepositoryAsync.IsLikedBy(post.Id, user.Id);
                post.RePostedByMe = _postRepositoryAsync.IsRepostedBy(post.Id, user.Id);
            }
            post.Likes = _postRepositoryAsync.CountLikes(post.Id);
            post.RePosts = _postRepositoryAsync.CountReposts(post.Id);
            post.Answers = _postRepositoryAsync.CountAnswers(post.Id);

            return post;
        }
    }
}