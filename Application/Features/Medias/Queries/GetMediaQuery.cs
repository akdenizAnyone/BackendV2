using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Medias.Queries{
    public class GetMediaQuery : IRequest<Media>
    {
        public Guid Id { get; set; }
    }
    public class GetMediaQueryHandler : IRequestHandler<GetMediaQuery, Media>
    {
        private readonly IApplicationDbContext _context;

        public GetMediaQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Media> Handle(GetMediaQuery request, CancellationToken cancellationToken)
        {
            var media = await _context.Medias.FirstOrDefaultAsync(m => m.GudId== request.Id, cancellationToken);

            if(media == null)
                throw new ApiException($"Media {media.Id} not Found");
            return media;
        }
    }
}