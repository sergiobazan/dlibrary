using Infrastructure.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Infrastructure.BackgroundJobs;

public class ProcessOutboxMessagesJob : IJob
{
    private readonly ILogger<ProcessOutboxMessagesJob> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;

    private static readonly JsonSerializerSettings SerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };

    public ProcessOutboxMessagesJob(ILogger<ProcessOutboxMessagesJob> logger, ApplicationDbContext context, IPublisher publisher)
    {
        _logger = logger;
        _context = context;
        _publisher = publisher;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Begin to process outbox messages");

        List<OutboxMessage> outboxMessages = await GetOutboxMessages();

        if (outboxMessages.Count == 0)
        {
            _logger.LogInformation("Finish processing outbox messages - no more messages to process");
            return;
        }

        foreach (OutboxMessage outboxMessage in outboxMessages)
        {
            try
            {
                var domainEvent = JsonConvert.DeserializeObject(outboxMessage.Content, SerializerSettings)!;

                await _publisher.Publish(domainEvent);
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception while processing outbox message {MessageId}", outboxMessage.Id);

                outboxMessage.Error = exception.Message;
            }

            outboxMessage.ProcessOnUtc = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync(context.CancellationToken);
    }

    private async Task<List<OutboxMessage>> GetOutboxMessages()
    {
        return await _context
            .Set<OutboxMessage>()
            .Where(outbox => outbox.ProcessOnUtc == null)
            .OrderBy(outbox => outbox.OccurredOnUtc)
            .Take(20)
            .ToListAsync();
    }
}
