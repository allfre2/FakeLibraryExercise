using Application.DTO;
using Application.Services;
using AutoMapper;
using Domain.Security;
using MediatR;

namespace Application.Commands;

public class LoginCommand : IRequest<AccessTokenDTO>
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, AccessTokenDTO>
{
    private readonly IMapper _mapper;
    private readonly ITokenGeneratorService _tokenService;

    public LoginCommandHandler(IMapper mapper, ITokenGeneratorService tokenService)
    {
        _mapper = mapper;
        _tokenService = tokenService;
    }

    public async Task<AccessTokenDTO> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var accessToken = await _tokenService.GenerateAccessToken(new ClientCredentials
        {
            ClientId = command.ClientId,
            ClientSecret = command.ClientSecret,
        });

        return _mapper.Map<AccessTokenDTO>(accessToken);
    }
}
