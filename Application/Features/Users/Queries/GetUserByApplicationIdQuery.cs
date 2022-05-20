using MediatR;
using Application.Features.Users.Dtos;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using AutoMapper;
using Application.Exceptions;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Features.Users.Queries
{
    public class GetUserByApplicationIdQuery : IRequest<UserDto>
    {
        public string ApplicationUserId { get; set; }
    }

    public class GetUserByApplicationIdQueryHandler : IRequestHandler<GetUserByApplicationIdQuery,UserDto>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepositoryAsync _userRepository;
        public GetUserByApplicationIdQueryHandler(IUserRepositoryAsync userRepository,IMapper mapper)
        {
            _mapper=mapper;
            _userRepository=userRepository;
        }

        async Task<UserDto> IRequestHandler<GetUserByApplicationIdQuery, UserDto>.Handle(GetUserByApplicationIdQuery request, CancellationToken cancellationToken)
        {
            var user=await _userRepository.GetUserByApplicationId(request.ApplicationUserId);
            if (user==null)
                throw new ApiException("User Not Found");
            var mappedUser=_mapper.Map<UserDto>(user); 
            return mappedUser; 
        }
    }

}
