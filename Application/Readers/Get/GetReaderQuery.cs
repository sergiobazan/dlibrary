using MediatR;

namespace Application.Readers.Get;

public sealed record GetReaderQuery(Guid Id) : IRequest<ReaderResponse>;
