namespace MinaryLib.Exceptions
{
  using System;


  public class MinaryWarningException : Exception
  {
    public MinaryWarningException(string message)
      : base(message)
    {
    }
  }
}
