using System;

namespace Entities.Attributes
{
    /// <summary>
    /// Used to enable user auditing for an entity
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EnableUserAudit : Attribute
    {

    }
}