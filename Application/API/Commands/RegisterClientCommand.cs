using Application.DTO;
using Application.Services;
using AutoMapper;
using Domain.Security;
using MediatR;

namespace Application.Commands;

public class RegisterClientCommand : IRequest<RegisteredClientDTO>
{
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public IDictionary<string, string>? Claims { get; set; }
}

public class RegisterClientCommandHandler : IRequestHandler<RegisterClientCommand, RegisteredClientDTO>
{
    private readonly IAuthManager _authManager;
    private readonly IMapper _mapper;

    public RegisterClientCommandHandler(IAuthManager authManager, IMapper mapper)
    {
        _authManager = authManager;
        _mapper = mapper;
    }

    public async Task<RegisteredClientDTO> Handle(RegisterClientCommand command, CancellationToken cancellationToken)
    {
        var data = await _authManager.RegisterClient(new RegistrationData
        {
            Email = command.Email,
            Roles = command.Roles,
            Claims = command.Claims
        });

        return _mapper.Map<RegisteredClientDTO>(data);
    }
}
