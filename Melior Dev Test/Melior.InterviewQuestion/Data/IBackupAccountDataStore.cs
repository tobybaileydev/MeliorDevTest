namespace Melior.InterviewQuestion.Data;

public interface IBackupAccountDataStore
{
    Account GetAccount(string accountNumber);
    void UpdateAccount(Account account);
}
