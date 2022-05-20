using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Features.Medias.Commands{
    public class CreateMediaCommand : IRequest<Guid>
    {
        public string FileName { get; set; }
        public long Length { get; set; }
        public string ContentType { get; set; }
        public Func<Stream> OpenReadStream { get; set; }
    }

    public class CreateMediaCommandHandler : IRequestHandler<CreateMediaCommand, Guid>
    {
        private readonly IApplicationDbContext _context;

        public CreateMediaCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        
        public async Task<Guid> Handle(CreateMediaCommand request, CancellationToken cancellationToken)
        {
            using var stream = new MemoryStream();
            await request.OpenReadStream().CopyToAsync(stream, cancellationToken);

            var media = new Media {
                Content = stream.ToArray(),
                FileName = request.FileName,
                ContentType = request.ContentType
            };

            _context.Medias.Add(media);
            await _context.SaveChangesAsync(cancellationToken);

            return media.GudId;
        }
    }
}