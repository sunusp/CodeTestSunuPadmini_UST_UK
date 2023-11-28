namespace DomainModel.Customer
{
    public class CustomerMessageSchedule
    {
        public int CustomerId { get; set; }
        public TimeSpan StartsFrom { get; set; }
        public TimeSpan EndsOn { get; set; }
        public TimeSpan ScheduleTimeOfDay { get; set; }
    }
}
