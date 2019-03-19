namespace VsFullCleanup.Core.Result
{
   public class ReturnResult
   {
      public bool IsSuccess { get; set; }

      public string Message { get; set; }

      public ReturnResult(bool isSuccess, string message)
      {
         IsSuccess = isSuccess;
         Message = message;
      }
   }
}