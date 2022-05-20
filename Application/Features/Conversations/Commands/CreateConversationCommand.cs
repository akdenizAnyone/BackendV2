using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Conversations.Commands
{
    public class CreateConversationCommand : IRequest<int>
    {
        public IEnumerable<int> Members { get; set; }

    }
    public class CreateConversationCommandHandler : IRequestHandler<CreateConversationCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUser;

        public CreateConversationCommandHandler(IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
            _context = context;
        }

        public async Task<int> Handle(CreateConversationCommand request, CancellationToken cancellationToken)
        {
            var members = await _context.Users
                .Where(u => request.Members.Any(id => id == u.Id) || u.ApplicationUserId == _currentUser.UserId)
                .ToListAsync(cancellationToken);

            var notFounds = request.Members.Where(id => !members.Any(found => found.Id == id)).ToList();

            if (notFounds.Any())
                throw new ApiException("User Not Found");


            var conversation=new Conversation{};

            foreach(var item in members){
                conversation.Members.Add(new UserConversation{UserId=item.Id,ConversationId=conversation.Id});    
            }


            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync(cancellationToken);

            return conversation.Id;
        }
    }
}