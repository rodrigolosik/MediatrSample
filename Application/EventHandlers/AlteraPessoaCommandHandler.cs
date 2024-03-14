using Mapster;
using MediatR;
using MediatSample.Application.Commands;
using MediatSample.Application.Models;
using MediatSample.Application.Notifications;
using MediatSample.Repositories;

namespace MediatSample.Application.EventHandlers;

public class AlteraPessoaCommandHandler : IRequestHandler<AlteraPessoaCommand, string>
{
    private readonly IMediator _mediator;
    private readonly IRepository<Pessoa> _repository;
    public AlteraPessoaCommandHandler(IMediator mediator, IRepository<Pessoa> repository)
    {
        _mediator = mediator;
        _repository = repository;
    }

    public async Task<string> Handle(AlteraPessoaCommand request, CancellationToken cancellationToken)
    {
        var pessoa = request.Adapt<Pessoa>();
        var pessoaAlteradaNotification = pessoa.Adapt<PessoaAlteradaNotification>();

        try
        {
            await _repository.Edit(pessoa);

            await _mediator.Publish(pessoaAlteradaNotification);

            return await Task.FromResult("Pessoa alterada com sucesso.");

        }
        catch (Exception ex)
        {
            await _mediator.Publish(pessoaAlteradaNotification);
            await _mediator.Publish(new ErroNotification() {Excecao = ex.Message, PilhaErro = ex.StackTrace });
            return await Task.FromResult("Ocorreu um erro no momento da alteração.");
        }
    }
}
