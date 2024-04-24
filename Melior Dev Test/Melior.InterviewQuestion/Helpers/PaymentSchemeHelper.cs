namespace Melior.InterviewQuestion.Helpers;

public class PaymentSchemeHelper : IPaymentSchemeHelper
{
    //Based on the logic in the original PaymentService, there's no way any of these ever return successful, which makes the relevance of the entire method pointless.
    //So they have been split out and rationalized down to their basic outputs so they can be developed later
    public MakePaymentResult HandleBacsPaymentScheme(Account account)
    {
        return new MakePaymentResult();
    }

    public MakePaymentResult HandleFasterPaymentsPaymentScheme(Account account, MakePaymentRequest request)
    {
        return new MakePaymentResult();
    }

    public MakePaymentResult HandleChapsPaymentScheme(Account account)
    {
        return new MakePaymentResult();
    }
}