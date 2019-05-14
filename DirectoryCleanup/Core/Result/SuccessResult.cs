namespace DirectoryCleanup.Core.Result
{
   public class SuccessResult : ReturnResult
   {
      public SuccessResult() : base(true, string.Empty)
      {
      }
   }
}