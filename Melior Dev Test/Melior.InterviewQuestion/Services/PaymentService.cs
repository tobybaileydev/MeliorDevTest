namespace Melior.InterviewQuestion.Services;

public class PaymentService(
    IAccountHelper accountHelper, 
    IPaymentHelper paymentHelper
    ) : IPaymentService
{
    public MakePaymentResult MakePayment(MakePaymentRequest request)
    {
        var account = accountHelper.GetAccount(request.DebtorAccountNumber);

        var result = paymentHelper.DecidePaymentResult(request, account);

        if (result.Success)
        {
            accountHelper.UpdateAccount(request, account);
        }

        return result;
    }
}