using Application.Abstractions.Behavior.Messaging;

namespace Application.Categories.Create;

public sealed record CreateCategoryCommand(string name, string description) : ICommand;