namespace Melior.InterviewQuestion.Unit.Tests;

[TestClass]
public class PaymentHelperTests
{
    private readonly IPaymentSchemeHelper paymentSchemeHelper;

    public PaymentHelperTests()
    {
        paymentSchemeHelper = Substitute.For<IPaymentSchemeHelper>();
    }

    private IPaymentHelper CreateSut => new PaymentHelper(paymentSchemeHelper);

    [TestMethod]
    public void DecidePaymentResult_BacsPaymentScheme_ReturnsMakePaymentResult()
    {
        var sut = CreateSut;

        paymentSchemeHelper.HandleBacsPaymentScheme(Arg.Any<Account>())
            .Returns(new MakePaymentResult
            {
                Success = true
            });

        var result = sut.DecidePaymentResult(new MakePaymentRequest
        {
            PaymentScheme = PaymentScheme.Bacs
        }, new Account
        {
            AccountNumber = "TestAccountNumber"
        });

        result.Success.Should().BeTrue();
        paymentSchemeHelper.Received(1).HandleBacsPaymentScheme(Arg.Is<Account>(x => x.AccountNumber == "TestAccountNumber"));
    }

    [TestMethod]
    public void DecidePaymentResult_FasterPaymentsPaymentScheme_ReturnsMakePaymentResult()
    {
        var sut = CreateSut;

        paymentSchemeHelper.HandleFasterPaymentsPaymentScheme(Arg.Any<Account>(), Arg.Any<MakePaymentRequest>())
            .Returns(new MakePaymentResult
            {
                Success = true
            });

        var result = sut.DecidePaymentResult(new MakePaymentRequest
        {
            CreditorAccountNumber = "CreditorAccountNumber",
            PaymentScheme = PaymentScheme.FasterPayments
        }, new Account
        {
            AccountNumber = "TestAccountNumber"
        });

        result.Success.Should().BeTrue();
        paymentSchemeHelper.Received(1).HandleFasterPaymentsPaymentScheme(
            Arg.Is<Account>(x => x.AccountNumber == "TestAccountNumber"), 
            Arg.Is<MakePaymentRequest>(x => x.CreditorAccountNumber == "CreditorAccountNumber"));
    }

    [TestMethod]
    public void DecidePaymentResult_ChapsPaymentScheme_ReturnsMakePaymentResult()
    {
        var sut = CreateSut;

        paymentSchemeHelper.HandleChapsPaymentScheme(Arg.Any<Account>())
            .Returns(new MakePaymentResult
            {
                Success = true
            });

        var result = sut.DecidePaymentResult(new MakePaymentRequest
        {
            PaymentScheme = PaymentScheme.Chaps
        }, new Account
        {
            AccountNumber = "TestAccountNumber"
        });

        result.Success.Should().BeTrue();
        paymentSchemeHelper.Received(1).HandleChapsPaymentScheme(Arg.Is<Account>(x => x.AccountNumber == "TestAccountNumber"));
    }
}
