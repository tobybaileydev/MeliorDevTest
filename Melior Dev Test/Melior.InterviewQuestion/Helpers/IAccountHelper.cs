namespace Melior.InterviewQuestion.Helpers;

public interface IAccountHelper
{
    Account GetAccount(string debtorAccountNumber);
    void UpdateAccount(MakePaymentRequest request, Account account);
}