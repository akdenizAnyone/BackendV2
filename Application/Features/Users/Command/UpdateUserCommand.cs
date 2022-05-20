using System.Threading;
using System.Threading.Tasks;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using Application.Interfaces;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Users.Command
{

    public class UpdateUserCommand : IRequest<int>
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public Guid? PictureId { get; set; }
        public Guid? BannerId { get; set; }
    }
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepositoryAsync _userRepositoryAsync;
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        public UpdateUserCommandHandler(ICurrentUserService currentUserService, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _context=context;

        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.ApplicationUserId==_currentUserService.UserId,cancellationToken);
            if (user == null)
                throw new ApplicationException("User Not Found");

            user.FullName = request.FullName;
            user.Description = request.Description;
            user.Location = request.Location;
            user.Website = request.Website;
            user.PictureId = request.PictureId;
            user.BannerId = request.BannerId;
            _context.Users.Update(user);

            await _context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

    }
}
