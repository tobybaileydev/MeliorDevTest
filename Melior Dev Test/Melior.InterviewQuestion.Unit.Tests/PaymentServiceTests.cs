namespace Melior.InterviewQuestion.Unit.Tests;

[TestClass]
public class PaymentServiceTests
{
    private readonly IAccountHelper accountHelper;
    private readonly IPaymentHelper paymentHelper;

    public PaymentServiceTests()
    {
        accountHelper = Substitute.For<IAccountHelper>();
        paymentHelper = Substitute.For<IPaymentHelper>();
    }

    private IPaymentService CreateSut => new PaymentService(accountHelper, paymentHelper);

    [TestMethod]
    public void MakePayment_ResultIsNotSuccess_DoesNotUpdateAccount()
    {
        var sut = CreateSut;

        accountHelper.GetAccount(Arg.Any<string>())
            .Returns(new Account
            {
                AccountNumber = "AccountNumber"
            });
        paymentHelper.DecidePaymentResult(Arg.Any<MakePaymentRequest>(), Arg.Any<Account>())
            .Returns(new MakePaymentResult
            {
                Success = false
            });

        var result = sut.MakePayment(new MakePaymentRequest
        {
            DebtorAccountNumber = "DebtorAccountNumber"
        });

        result.Success.Should().BeFalse();
        accountHelper.Received(1).GetAccount("DebtorAccountNumber");
        paymentHelper.Received(1).DecidePaymentResult(
            Arg.Is<MakePaymentRequest>(x => x.DebtorAccountNumber == "DebtorAccountNumber"),
            Arg.Is<Account>(x => x.AccountNumber == "AccountNumber"));
        accountHelper.Received(0).UpdateAccount(Arg.Any<MakePaymentRequest>(), Arg.Any<Account>());
    }

    [TestMethod]
    public void MakePayment_ResultIsSuccess_DoesUpdateAccount()
    {
        var sut = CreateSut;

        accountHelper.GetAccount(Arg.Any<string>())
            .Returns(new Account
            {
                AccountNumber = "AccountNumber"
            });
        paymentHelper.DecidePaymentResult(Arg.Any<MakePaymentRequest>(), Arg.Any<Account>())
            .Returns(new MakePaymentResult
            {
                Success = true
            });

        var result = sut.MakePayment(new MakePaymentRequest
        {
            DebtorAccountNumber = "DebtorAccountNumber"
        });

        result.Success.Should().BeTrue();
        accountHelper.Received(1).GetAccount("DebtorAccountNumber");
        paymentHelper.Received(1).DecidePaymentResult(
            Arg.Is<MakePaymentRequest>(x => x.DebtorAccountNumber == "DebtorAccountNumber"),
            Arg.Is<Account>(x => x.AccountNumber == "AccountNumber"));
        accountHelper.Received(1).UpdateAccount(
            Arg.Is<MakePaymentRequest>(x => x.DebtorAccountNumber == "DebtorAccountNumber"),
            Arg.Is<Account>(x => x.AccountNumber == "AccountNumber"));
    }
}
