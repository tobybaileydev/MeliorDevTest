namespace Melior.InterviewQuestion.Helpers;

public class PaymentHelper(
    IPaymentSchemeHelper paymentSchemeHelper
    ) : IPaymentHelper
{
    public MakePaymentResult DecidePaymentResult(MakePaymentRequest request, Account account)
    {
        switch (request.PaymentScheme)
        {
            case PaymentScheme.Bacs:
                return paymentSchemeHelper.HandleBacsPaymentScheme(account);
            case PaymentScheme.FasterPayments:
                return paymentSchemeHelper.HandleFasterPaymentsPaymentScheme(account, request);
            case PaymentScheme.Chaps:
                return paymentSchemeHelper.HandleChapsPaymentScheme(account);
            default:
                return new MakePaymentResult();
        }
    }
}