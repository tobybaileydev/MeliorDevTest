namespace Melior.InterviewQuestion.Helpers;

public class ConfigurationHelper : IConfigurationHelper
{
    public string GetDataStoreType()
    {
        return ConfigurationManager.AppSettings["DataStoreType"];
    }
}
