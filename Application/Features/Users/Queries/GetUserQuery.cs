using MediatR;
using Application.Features.Users.Dtos;
using System.Threading.Tasks;
using System.Threading;
using Application.Interfaces.Repositories;
using AutoMapper;
using Application.Exceptions;
using Application.Wrappers;
using Domain.Entities;

namespace Application.Features.Users.Queries{
    public class GetUserQuery:IRequest<UserDto>{
        public int Id{get;set;}
    }

    public class GetUserQueryHandler : IRequestHandler<GetUserQuery,UserDto>{
        private readonly IMapper _mapper;
        private readonly IUserRepositoryAsync _userRepository;

        public GetUserQueryHandler(IUserRepositoryAsync userRepository,IMapper mapper)
        {
            _mapper=mapper;
            _userRepository=userRepository;
        }


        public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user =await _userRepository.GetByIdAsync(request.Id);
            if (user==null)
                throw new ApiException("User Not Found");

            var mappedUser=_mapper.Map<UserDto>(user);
            return mappedUser;
        }
    }


}
