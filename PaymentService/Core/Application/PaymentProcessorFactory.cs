using Application.Booking.DTO;
using PaymentApplication.MercadoPago;
using Application.Payment.Ports;

namespace PaymentApplication
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {

        public IPaymentProcessor GetPaymentProcessor(SupportedPaymentProviders selectedPaymentProvider)
        {
            switch (selectedPaymentProvider)
            {
                case SupportedPaymentProviders.MercadoPago:
                    return new MercadoPagoAdapter();

                default: return new NotImplementedPaymentProvider();
            }
        }
    }
}
