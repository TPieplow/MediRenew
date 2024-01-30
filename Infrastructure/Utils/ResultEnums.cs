using MediRenew.ConsoleApp.Utils;
namespace Infrastructure.Utils;

public static class ResultEnums
{
    /// <summary>
    /// Method that takes a generic entity, using type(of) to get the Objects name.
    /// Contains a switch that operates on different CRUD-operations.
    /// </summary>
    /// <typeparam name="TEntity">The entity affected by the CRUD-operation</typeparam>
    /// <param name="operation">The CRUD-operation performed</param>
    /// <param name="result">The result of the CRUD-operation</param>
    /// <param name="customMessage">A custom message for the user</param>
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

    /// <summary>
    /// Gets the right message containing information about the operation and the affected entity.
    /// </summary>
    /// <param name="entityName">The entity affected by the CRUD-operation.</param>
    /// <param name="action">A prewritten message for the user, "added", "read", "updated", "delete".</param>
    /// <param name="result">The result of the operation being sent back to the user.</param>
    /// <param name="customMessage">A custom message for the user.</param>
    /// <returns>A message containing information about the operation and the affected entity</returns>
    private static string GetMessage(string entityName, string action, Result result, string customMessage)
    {
        return $"{entityName} {action} {GetResultMessage(result, customMessage)}";
    }

    /// <summary>
    /// Returns a message depending on the provided result, using the Result-Enum.
    /// Also sends a customMessage to the user.
    /// </summary>
    /// <param name="result">The Enum result</param>
    /// <param name="customMessage">Returns a custom message depending on the result.</param>
    /// <returns></returns>
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

    /// <summary>
    /// Represents the CRUD-operations.
    /// </summary>
    public enum CrudOperation
    {
        Create,
        Read,
        Update,
        Delete
    }

    /// <summary>
    /// Represents all the possible results.
    /// </summary>
    public enum Result
    {
        Success,
        NotFound,
        Failure,
    }
}