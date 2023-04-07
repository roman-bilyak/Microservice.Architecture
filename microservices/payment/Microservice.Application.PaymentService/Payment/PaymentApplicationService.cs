using Microservice.Application;
using Microsoft.AspNetCore.Authorization;

namespace Microservice.PaymentService.Payment;

[Authorize]
internal class PaymentApplicationService : ApplicationService, IPaymentApplicationService
{
}