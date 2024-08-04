using PosTech.MyFood.WebApi.Features.Orders.Repositories;
using Quartz;

namespace PosTech.MyFood.Jobs;

public class OrderCleanupJob(IOrderQueueRepository orderQueueRepository, ILogger<OrderCleanupJob> logger)
    : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        logger.LogInformation("OrderCleanupJob started.");

        var threshold = DateTime.UtcNow.AddMinutes(-30);
        await orderQueueRepository.CancelOrdersNotPreparingWithinAsync(threshold);

        logger.LogInformation("OrderCleanupJob finished.");
    }
}