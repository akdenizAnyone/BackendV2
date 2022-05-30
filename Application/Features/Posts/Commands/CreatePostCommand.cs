using System.Threading;
using System.Threading.Tasks;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Application.Interfaces;
using System;
using System.Linq;

namespace Application.Features.Posts.Command
{

    public class CreatePostCommand : IRequest<int>
    {
        public string Content{ get; set; }
        public Guid? MediaId { get; set; }
        public int? AnswerToId{ get; set; }
    }
    public class CreatePostCommandHandler: IRequestHandler<CreatePostCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        private readonly IAuthenticatedUserService _authenticatedUserService;

        public CreatePostCommandHandler(IMapper mapper, IApplicationDbContext context, IUserRepositoryAsync userRepositoryAsync,IAuthenticatedUserService userService)
        {

            _mapper = mapper;
            _context = context;
            _authenticatedUserService=userService;

        }
        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {

            var userId=_authenticatedUserService.UserId; 
            var userIntId=_context.Users.FirstOrDefault(x=>x.ApplicationUserId==userId).Id;
            var post = new Post { Content = request.Content, MediaId = request.MediaId, AnswerToId = request.AnswerToId,UserId=userIntId }jk;
            

            _context.Posts.Add(post);
            await _context.SaveChangesAsync(cancellationToken);
            return post.Id;
        }
    }
}
