namespace MassTransitBenchmark.Latency
{
    using System;
    using System.Threading.Tasks;
    using MassTransit;
    using MassTransit.Mediator;


    public class MediatorMessageLatencyTransport :
        IMessageLatencyTransport
    {
        readonly IMessageLatencySettings _settings;
        IMediator _mediator;

        public MediatorMessageLatencyTransport(IMessageLatencySettings settings)
        {
            _settings = settings;
        }

        public Task Send(LatencyTestMessage message)
        {
            return _mediator.Send(message);
        }

        public Task Start(Action<IReceiveEndpointConfigurator> callback, IReportConsumerMetric reportConsumerMetric)
        {
            _mediator = Bus.Factory.CreateMediator(callback);

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_mediator is IAsyncDisposable asyncDisposable)
                await asyncDisposable.DisposeAsync();
        }
    }
}
