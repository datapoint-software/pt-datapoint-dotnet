using System.Threading;
using System.Threading.Tasks;

namespace Datapoint.Mediator
{
    /// <summary>
    /// A message handler.
    /// </summary>
    /// <typeparam name="TMessage">The message type.</typeparam>
    public interface IMessageHandler<TMessage>
    {
        /// <summary>
        /// Handles a message asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ct">The asynchronous task cancellation token.</param>
        /// <returns>The asynchronous task for the message result.</returns>
        Task HandleMessageAsync(TMessage message, CancellationToken ct);
    }
}
