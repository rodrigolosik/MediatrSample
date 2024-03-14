using MediatR;
using MediatSample.Application.Notifications;

namespace MediatSample.Application.EventHandlers;

public class LogEventHandler : INotificationHandler<PessoaCriadaNotification>,
                                INotificationHandler<PessoaAlteradaNotification>,
                                INotificationHandler<PessoaExcluidaNotification>,
                                INotificationHandler<ErroNotification>
{
    public Task Handle(PessoaAlteradaNotification notification, CancellationToken cancellationToken) =>
        Task.Run(() => Console.WriteLine($"ALTERACAO: '{notification.Id} - {notification.Nome} - {notification.Idade} - {notification.Sexo} - {notification.IsEfetivado}'"));

    public Task Handle(PessoaCriadaNotification notification, CancellationToken cancellationToken) =>
        Task.Run(() => Console.WriteLine($"CRIACAO: '{notification.Id} - {notification.Nome} - {notification.Idade} - {notification.Sexo}'"));

    public Task Handle(PessoaExcluidaNotification notification, CancellationToken cancellationToken) =>
        Task.Run(() => Console.WriteLine($"EXCLUSAO: '{notification.Id} - {notification.IsEfetivado}'"));

    public Task Handle(ErroNotification notification, CancellationToken cancellationToken) =>
        Task.Run(() => Console.WriteLine($"ERRO: '{notification.Excecao} \n {notification.PilhaErro}'"), cancellationToken);
}
