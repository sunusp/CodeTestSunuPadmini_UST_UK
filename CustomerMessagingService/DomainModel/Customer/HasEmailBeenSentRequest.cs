using System.ComponentModel;

namespace CustomerAPI.RequestModels
{
    public class HasEmailBeenSentRequest
    {
        public int CustomerId { get; set; }

        public string TimeOfDay { get; set; }
    }
}
