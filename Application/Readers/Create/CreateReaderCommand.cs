using Application.Abstractions.Behavior.Messaging;

namespace Application.Readers.Create;

public sealed record CreateReaderCommand(string Name, string LastName, string Email) : ICommand<string>;
