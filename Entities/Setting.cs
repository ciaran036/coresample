using Entities.Enums;
using Entities.Interfaces;

namespace Entities
{
    // TODO: Delete 

    //public class Setting : IEntityBase
    //{
    //    public int Id { get; set; }

    //    public string Name { get; set; }

    //    public string Description { get; set; } = string.Empty;

    //    public string Value { get; set; }

    //    public DataValueType DataType { get; set; }

    //    public int SortOrder { get; set; }

    //    public bool EcomOnly { get; set; }

    //    public bool Active { get; set; } = true;

    //    public Setting()
    //    {
            
    //    }

    //    public Setting(string name, string value, string description = "", bool ecomOnly = false, bool textEditor = false)
    //    {
    //        DataType = textEditor ? DataValueType.TextEditor : DataValueType.Text;
    //        Value = value;
    //        Map(name, description, ecomOnly);
    //    }

    //    public Setting(string name, bool value, string description = "", bool ecomOnly = false)
    //    {
    //        DataType = DataValueType.Boolean;
    //        Value = value ? "true" : "false";
    //        Map(name, description, ecomOnly);
    //    }

    //    public Setting(string name, int value, string description = "", bool ecomOnly = false)
    //    {
    //        DataType = DataValueType.Number;
    //        Value = value.ToString();
    //        Map(name, description, ecomOnly);
    //    }

    //    private void Map(string name, string description, bool ecomOnly)
    //    {
    //        Name = name;
    //        Description = description;
    //        EcomOnly = ecomOnly;
    //    }
    //}
}
