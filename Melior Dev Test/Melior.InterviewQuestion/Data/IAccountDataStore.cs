﻿namespace Melior.InterviewQuestion.Data;

public interface IAccountDataStore
{
    Account GetAccount(string accountNumber);
    void UpdateAccount(Account account);
}
