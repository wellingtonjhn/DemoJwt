using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace DemoJwt.Application.Core
{
    /// <summary>
    /// Cria uma instãncia de <see cref="Response"/> que pode ser usado para retornar mensagens ou objetos para a Api
    /// <remarks>Nunca retorne diretamente uma instância de <see cref="Response"/>.
    /// Use as propriedades <see cref="Messages"/> ou <see cref="Value"/> na Api em conjunto com códigos HTTP que fazem sentido.</remarks>
    /// </summary>
    public class Response
    {
        private IList<Notification> _messages { get; } = new List<Notification>();

        public IReadOnlyCollection<Notification> Messages => _messages.ToList();

        public bool HasMessages => _messages.Any();

        public object Value { get; private set; }

        /// <summary>
        /// Cria um novo objeto de retorno para a api
        /// </summary>
        public Response()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="object">Objeto que deverá ser serializado pela Api</param>
        public Response(object @object) : this()
        {
            AddValue(@object);
        }

        /// <summary>
        /// Adiciona um objeto que deverá ser serializado e retornado pela Api
        /// </summary>
        /// <param name="object">Objeto que deverá ser serializado pela Api</param>
        public void AddValue(object @object)
        {
            Value = @object;
        }

        /// <summary>
        /// Adiciona mensagem de retorno
        /// </summary>
        /// <param name="notification">Mensagem que deverá ser retornada pela Api</param>
        public void AddNotification(Notification notification)
        {
            _messages.Add(notification);
        }

        /// <summary>
        /// Adiciona mensagens de retorno
        /// </summary>
        /// <param name="notifications">Notificações</param>
        /// <param name="type">Tipo de notificação</param>
        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            foreach (var notification in notifications)
            {
                AddNotification(notification);
            }
        }

        public override string ToString()
        {
            return string.Join(" - ", Messages.Select(x => x.Message));
        }
    }
}