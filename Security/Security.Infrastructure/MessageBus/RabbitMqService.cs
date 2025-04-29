using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Security.Core.Configs;
using Security.Core.MessageBus;
using Security.Core.Services;
using System.Text;
using System.Text.Json;

namespace Security.Infrastructure.MessageBus
{
    public class RabbitMqService(ILoggerService loggerService, IOptions<RabbitMqConfig> settings) : IRabbitMqService
    {
        private RabbitMqConfig Settings { get; } = settings.Value;

        public async Task<IConnection> CreateConnection(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory()
            {
                HostName = Settings.HostName ?? string.Empty,
                UserName = Settings.UserName ?? string.Empty,
                Password = Settings.Password ?? string.Empty
            };

            return await factory.CreateConnectionAsync(cancellationToken);
        }

        public async Task CloseConnection(IConnection connection, IChannel channel, CancellationToken cancellationToken)
        {
            try
            {
                await channel.CloseAsync(cancellationToken);
                await channel.DisposeAsync();
                await connection.CloseAsync(cancellationToken);
                await connection.DisposeAsync();
            }
            catch (Exception ex)
            {
                loggerService.LogInformation(MessageError.FecharConexão(ex.Message));
                throw;
            }
        }

        public async Task CloseConnection(IConnection connection, IChannel channelFirst, IChannel channelSecond,
            CancellationToken cancellationToken)
        {
            try
            {
                await channelFirst.CloseAsync(cancellationToken);
                await channelFirst.DisposeAsync();
                await channelSecond.CloseAsync(cancellationToken);
                await channelSecond.DisposeAsync();
                await connection.CloseAsync(cancellationToken);
                await connection.DisposeAsync();
            }
            catch (Exception ex)
            {
                loggerService.LogInformation(MessageError.FecharConexão(ex.Message));
                throw;
            }
        }

        public async Task PublishMessage<T>(T message, string queue, CancellationToken cancellationToken)
        {
            await using var connection = await CreateConnection(cancellationToken);

            await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

            try
            {
                await channel.QueueDeclareAsync(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);

                var json = JsonSerializer.Serialize(message);

                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(exchange: "", routingKey: queue, body: body, cancellationToken);

                loggerService.LogInformation($"Sucesso ao publicar a mensagem na fila {queue}.");
            }
            catch (Exception ex)
            {
                loggerService.LogError($"Error ao publicar a mensagem. Mensagem: {ex.Message}");
                throw;
            }
            finally
            {
                await CloseConnection(connection, channel, cancellationToken);
            }
        }

    }
}
