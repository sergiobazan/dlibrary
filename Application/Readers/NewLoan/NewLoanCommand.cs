using Application.Abstractions.Behavior.Messaging;

namespace Application.Readers.NewLoan;

public sealed record NewLoanCommand(Guid ReaderId, Guid BookId, int NDays) : ICommand<Guid>;

public sealed record NewLoanRequest(Guid ReaderId, Guid BookId, int NDays);