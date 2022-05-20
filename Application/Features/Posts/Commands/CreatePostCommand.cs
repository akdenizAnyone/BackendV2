using System.Threading;
using System.Threading.Tasks;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Application.Interfaces;
using System;

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

        public CreatePostCommandHandler(IMapper mapper, IApplicationDbContext context, IUserRepositoryAsync userRepositoryAsync)
        {

            _mapper = mapper;
            _context = context;

        }
        public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {

            
            var post = new Post { Content = request.Content, MediaId = request.MediaId, AnswerToId = request.AnswerToId };
            

            _context.Posts.Add(post);
            await _context.SaveChangesAsync(cancellationToken);
            return post.Id;
        }
    }
}
