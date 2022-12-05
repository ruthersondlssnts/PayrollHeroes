using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TPS.Infrastructure;

namespace TPS.Services.Utility
{
    public class MessageBroker : IDisposable, IMessageBroker
    {
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;
        private EventingBasicConsumer _consumer;
        private readonly ConfigurationSettings _configuration;
        public event EventHandler<BasicDeliverEventArgs> MessageReceived;

        private string _hostName = string.Empty;
        private string _userName = string.Empty;
        private string _password = string.Empty;
        private string _virtualHost = string.Empty;
        private string _parentExchange = string.Empty;
        private string _exchange = string.Empty;
        private string _queue = string.Empty;
        private string _consumerName = string.Empty;
        private string _routingKey = string.Empty;

        public MessageBroker(ConfigurationSettings configuration)
        {
            _configuration = configuration;
        }
        public void Connect()
        {
            _hostName = _configuration.RabbitHost;
            _userName = _configuration.RabbitUsername;
            _password = _configuration.RabbitPassword;
            _virtualHost = _configuration.VirtualHost;
            _parentExchange = "";
            _exchange = _configuration.Exchange;
            _queue = "";
            _consumerName = "";
            _routingKey = "";

            if (_connection == null)
            {
                if (_connectionFactory == null)
                    _connectionFactory = new ConnectionFactory
                    {
                        HostName = _hostName,
                        Port = 5672,
                        UserName = _userName,
                        Password = _password,
                        VirtualHost = _virtualHost,
                        RequestedHeartbeat = TimeSpan.FromSeconds(60),
                        NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
                    };

                _connection = _connectionFactory.CreateConnection();

                if (_model == null)
                    _model = _connection.CreateModel();

                if (string.IsNullOrEmpty(_exchange))
                {
                    _model.ExchangeDeclare(_parentExchange, "topic", true, false, null);
                }

                if (!string.IsNullOrEmpty(_queue))
                {
                    _model.ExchangeDeclare(_parentExchange, "topic", true, false, null);
                    _model.ExchangeDeclare(_exchange, "topic", true, false, null);
                    _model.ExchangeBind(_exchange, _parentExchange, _routingKey, null);

                    _model.QueueDeclare(_queue, true, false, false, null);
                    _model.QueueBind(_queue, _exchange, _routingKey, null);

                    _model.BasicQos(0, 1, false);
                    _consumer = new EventingBasicConsumer(_model);
                    _consumer.Received += HandleMessage;

                    _model.BasicConsume(_queue, false, $"{_consumerName}.{Guid.NewGuid()}", _consumer);
                }
            }
        }

        private void HandleMessage(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                MessageReceived(sender, e);

                _consumer.Model.BasicAck(e.DeliveryTag, false);
            }
            catch
            {
                _consumer.Model.BasicNack(e.DeliveryTag, false, true);
                Thread.Sleep(5000);
            }
        }

        public void Publish<T>(T payload, string messageType, string routingKey, string correlationId)
        {
            var properties = _model.CreateBasicProperties();
            properties.CorrelationId = correlationId;
            properties.DeliveryMode = 2;
            properties.Type = messageType;

            var payloadBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(payload));

            _model.BasicPublish(_parentExchange, routingKey, properties, payloadBytes);
        }

        public void Publish(string payload, string messageType, string routingKey, string correlationId)
        {
            var properties = _model.CreateBasicProperties();
            properties.CorrelationId = correlationId;
            properties.DeliveryMode = 2;
            properties.Type = messageType;

            var payloadBytes = Encoding.UTF8.GetBytes(payload);

            _model.BasicPublish(_exchange, routingKey, properties, payloadBytes);
        }

        public void Dispose()
        {
            _model.Dispose();
            _connection.Dispose();
        }
    }
}
