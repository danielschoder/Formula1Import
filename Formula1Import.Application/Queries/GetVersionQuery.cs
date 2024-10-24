using Formula1Import.Contracts.Responses;
using MediatR;

namespace Formula1Import.Application.Queries;

public class GetVersionQuery : IRequest<Alive> { }
