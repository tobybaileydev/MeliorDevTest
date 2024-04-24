namespace Melior.InterviewQuestion.Helpers;

public interface IPaymentHelper
{
    MakePaymentResult DecidePaymentResult(MakePaymentRequest request, Account account);
}
