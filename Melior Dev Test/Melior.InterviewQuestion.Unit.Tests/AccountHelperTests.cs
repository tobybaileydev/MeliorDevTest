namespace Melior.InterviewQuestion.Unit.Tests;

[TestClass]
public class AccountHelperTests
{
    private readonly IConfigurationHelper configurationHelper;
    private readonly IAccountDataStore accountDataStore;
    private readonly IBackupAccountDataStore backupAccountDataStore;

    public AccountHelperTests()
    {
        configurationHelper = Substitute.For<IConfigurationHelper>();
        accountDataStore = Substitute.For<IAccountDataStore>();
        backupAccountDataStore = Substitute.For<IBackupAccountDataStore>();
    }

    private IAccountHelper CreateSut => new AccountHelper(configurationHelper, accountDataStore, backupAccountDataStore);

    [TestMethod]
    public void GetAccount_DataStoreTypeIsBackup_ReturnsBackupResult()
    {
        var sut = CreateSut;

        configurationHelper.GetDataStoreType()
            .Returns("Backup");
        backupAccountDataStore.GetAccount(Arg.Any<string>())
            .Returns(new Account
            {
                AccountNumber = "BackupAccountNumber"
            });

        var account = sut.GetAccount("DebtorAccountNumber");

        account.AccountNumber.Should().Be("BackupAccountNumber");
        configurationHelper.Received(1).GetDataStoreType();
        backupAccountDataStore.Received(1).GetAccount("DebtorAccountNumber");
        accountDataStore.Received(0).GetAccount(Arg.Any<string>());
    }

    [TestMethod]
    public void GetAccount_DataStoreTypeIsNotBackup_ReturnsResult()
    {
        var sut = CreateSut;

        configurationHelper.GetDataStoreType()
            .Returns("NotBackup");
        accountDataStore.GetAccount(Arg.Any<string>())
            .Returns(new Account
            {
                AccountNumber = "AccountNumber"
            });

        var account = sut.GetAccount("DebtorAccountNumber");

        account.AccountNumber.Should().Be("AccountNumber");
        configurationHelper.Received(1).GetDataStoreType();
        backupAccountDataStore.Received(0).GetAccount(Arg.Any<string>());
        accountDataStore.Received(1).GetAccount("DebtorAccountNumber");
    }

    [TestMethod]
    public void UpdateAccount_DataStoreTypeIsBackup_CallsDataService()
    {
        var sut = CreateSut;

        configurationHelper.GetDataStoreType()
            .Returns("Backup");
        backupAccountDataStore.UpdateAccount(Arg.Any<Account>());

       sut.UpdateAccount(new MakePaymentRequest
       {
           Amount = 6
       }, 
       new Account
       {
           Balance = 10
       });

        configurationHelper.Received(1).GetDataStoreType();
        backupAccountDataStore.Received(1).UpdateAccount(Arg.Is<Account>(x => x.Balance == 4));
        accountDataStore.Received(0).UpdateAccount(Arg.Any<Account>());
    }

    [TestMethod]
    public void UpdateAccount_DataStoreTypeIsNotBackup_CallsDataService()
    {
        var sut = CreateSut;

        configurationHelper.GetDataStoreType()
            .Returns("NotBackup");
        accountDataStore.UpdateAccount(Arg.Any<Account>());

        sut.UpdateAccount(new MakePaymentRequest
            {
                Amount = 6
            },
            new Account
            {
                Balance = 10
            });

        configurationHelper.Received(1).GetDataStoreType();
        backupAccountDataStore.Received(0).UpdateAccount(Arg.Any<Account>());
        accountDataStore.Received(1).UpdateAccount(Arg.Is<Account>(x => x.Balance == 4));
    }

}