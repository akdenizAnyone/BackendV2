using System.Threading;
using System.Threading.Tasks;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Features.Users.Command
{

    public class CreateUserCommand : IRequest<int>
    {
        public string ApplicationId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IUserRepositoryAsync _userRepositoryAsync;

        public CreateUserCommandHandler(IMapper mapper, IApplicationDbContext context, IUserRepositoryAsync userRepositoryAsync)
        {

            _mapper = mapper;
            _context = context;
            _userRepositoryAsync = userRepositoryAsync;

        }
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var entity = new User
            {
                ApplicationUserId = request.ApplicationId,
                UserName = request.UserName,
                Email = request.Email,
                FullName = request.FullName.Trim()
            };


            await _context.Users.AddAsync(entity);
            // var user1 = await _userRepositoryAsync.AddAsync(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
