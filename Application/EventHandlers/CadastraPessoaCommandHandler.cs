using Mapster;
using MediatR;
using MediatSample.Application.Commands;
using MediatSample.Application.Models;
using MediatSample.Application.Notifications;
using MediatSample.Repositories;

namespace MediatSample.Application.EventHandlers;

public class CadastraPessoaCommandHandler : IRequestHandler<CadastraPessoaCommand, string>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Pessoa> _repository;

    public CadastraPessoaCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<string> Handle(CadastraPessoaCommand request, CancellationToken cancellationToken)
    {
        var pessoa = request.Adapt<Pessoa>();
        var pessoaCriadaNotification = pessoa.Adapt<PessoaCriadaNotification>();

        try
        {
            await _repository.Add(pessoa);

            await _mediator.Publish(pessoaCriadaNotification);

            return await Task.FromResult("Pessoa criada com sucesso.");
        }
        catch (Exception ex)
        {
            await _mediator.Publish(pessoaCriadaNotification);
            await _mediator.Publish(new ErroNotification() { Excecao = ex.Message, PilhaErro = ex.StackTrace });

            return await Task.FromResult("Ocorreu um erro no momento da criação.");
        }
    }
}
