using AIHR.EventScheduler.Contracts.Interfaces;

namespace AIHR.EventSchedulerInfrastructure.Dates.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Today => DateTime.UtcNow.Date;

        public DateTime Now => DateTime.UtcNow;
    }
}
