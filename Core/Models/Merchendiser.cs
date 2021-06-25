namespace Domain.Core.Models
{
    public class Merchendiser : User
    {
        public int? CurrentShiftId { get; set; }
        public Workshift CurrentShift { get; set; }

        public Merchendiser() : base(Role.Merchendiser)
        {

        }
    }
}
