namespace Melior.InterviewQuestion.Helpers;

public interface IPaymentSchemeHelper
{
    MakePaymentResult HandleBacsPaymentScheme(Account account);
    MakePaymentResult HandleFasterPaymentsPaymentScheme(Account account, MakePaymentRequest request);
    MakePaymentResult HandleChapsPaymentScheme(Account account);
}