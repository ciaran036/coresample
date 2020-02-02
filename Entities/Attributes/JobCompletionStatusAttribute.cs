using System;

namespace Entities.Attributes
{
    /// <summary>
    /// Used to indicate a job log status is a job completion status
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class JobCompletionStatusAttribute : Attribute
    {

    }
}