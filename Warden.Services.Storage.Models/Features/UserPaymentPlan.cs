using System;

namespace Warden.Services.Storage.Models.Features
{
    public class UserPaymentPlan
    {
        public Guid PlanId { get; set; }
        public string Name { get; set; }
        public decimal MonthlyPrice { get; set; }
    }
}