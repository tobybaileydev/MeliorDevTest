namespace Melior.InterviewQuestion.Helpers;

public class AccountHelper(
    IConfigurationHelper configurationHelper,
    IAccountDataStore accountDataStore,
    IBackupAccountDataStore backupAccountDataStore
    ) : IAccountHelper
{
    public Account GetAccount(string debtorAccountNumber)
    {
        var dataStoreType = configurationHelper.GetDataStoreType();

        if (dataStoreType == "Backup")
        {
            return backupAccountDataStore.GetAccount(debtorAccountNumber);
        }
        else
        {
            return accountDataStore.GetAccount(debtorAccountNumber);
        }
    }

    public void UpdateAccount(MakePaymentRequest request, Account account)
    {
        account.Balance -= request.Amount;

        var dataStoreType = configurationHelper.GetDataStoreType();

        if (dataStoreType == "Backup")
        {
            backupAccountDataStore.UpdateAccount(account);
        }
        else
        {
            accountDataStore.UpdateAccount(account);
        }
    }
}
