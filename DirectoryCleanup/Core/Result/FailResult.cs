namespace DirectoryCleanup.Core.Result
{
   public class FailResult : ReturnResult
   {
      public FailResult(string message) : base(false, message)
      {
      }
   }
}