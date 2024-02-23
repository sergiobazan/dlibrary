using Application.Abstractions.Behavior.Messaging;

namespace Application.Readers.Get;

public sealed record GetReaderQuery(Guid Id) : IQuery<ReaderResponse>;
