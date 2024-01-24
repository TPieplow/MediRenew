
namespace MediRenew.ConsoleApp.Utils;

public static class ResultEnums
{
    public static void ReturnMessage<TEntity>(CrudOperation operation, Result result)
    {
        string entityName = typeof(TEntity).Name;

        switch (operation)
        {
            case CrudOperation.Create:
                DisplayMessage.Message(GetMessage(entityName, "added", result));
                break;
            case CrudOperation.Read:
                DisplayMessage.Message(GetMessage(entityName, "read", result));
                break;
            case CrudOperation.Update:
                DisplayMessage.Message(GetMessage(entityName, "updated", result));
                break;
            case CrudOperation.Delete:
                DisplayMessage.Message(GetMessage(entityName, "deleted", result));
                break;
            default:
                DisplayMessage.Message("An unexpected ERROR occurred.");
                break;
        }
    }

    private static string GetMessage(string entityName, string action, Result result)
    {
        return $"{entityName} {action} {GetResultMessage(result)}";
    }

    private static string GetResultMessage(Result result)
    {
        return result switch
        {
            Result.Success => "successfully",
            Result.NotFound => "not found",
            Result.Failure => "failed",
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
