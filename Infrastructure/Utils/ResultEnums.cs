using MediRenew.ConsoleApp.Utils;

namespace Infrastructure.Utils;

public static class ResultEnums
{
    public static void ReturnMessage<TEntity>(CrudOperation operation, Result result, string customMessage)
    {
        string entityName = typeof(TEntity).Name;

        switch (operation)
        {
            case CrudOperation.Create:
                DisplayMessage.Message(GetMessage(entityName, "added", result, customMessage));
                break;
            case CrudOperation.Read:
                DisplayMessage.Message(GetMessage(entityName, "read", result, customMessage));
                break;
            case CrudOperation.Update:
                DisplayMessage.Message(GetMessage(entityName, "updated", result, customMessage));
                break;
            case CrudOperation.Delete:
                DisplayMessage.Message(GetMessage(entityName, "deleted", result, customMessage));
                break;
            default:
                DisplayMessage.Message("An unexpected ERROR occurred.");
                break;
        }
    }

    private static string GetMessage(string entityName, string action, Result result, string customMessage)
    {
        return $"{entityName} {action} {GetResultMessage(result, customMessage)}";
    }

    private static string GetResultMessage(Result result, string customMessage)
    {
        return result switch
        {
            Result.Success => $"successfully. {customMessage}",
            Result.NotFound => $"not found. {customMessage}",
            Result.Failure => $"failed. {customMessage}",
            _ => "unknown result",
        };
    }

    public enum CrudOperation
    {
        Create,
        Read,
        Update,
        Delete
    }

    public enum Result
    {
        Success,
        NotFound,
        Failure,
    }
}