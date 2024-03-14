using MediatR;

namespace MediatSample.Application.Notifications;

public class PessoaExcluidaNotification : INotification
{
    public int Id { get; set; }
    public bool IsEfetivado { get; set; }
}
