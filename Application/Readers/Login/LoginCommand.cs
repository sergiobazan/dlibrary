using Application.Abstractions.Behavior.Messaging;

namespace Application.Readers.Login;

public sealed record LoginCommand(string Email) : ICommand<string>;

public sealed record LoginRequest(string Email);