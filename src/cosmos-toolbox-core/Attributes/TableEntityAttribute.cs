using System;

namespace CosmosToolbox.Core.Attributes
{
    [AttributeUsage(
        AttributeTargets.Class,
        AllowMultiple = false,
        Inherited = true
    )]
    public sealed class TableEntityAttribute : Attribute
    {
        /// <summary>
        /// name of table entity is stored in
        /// </summary>
        public string TableName { get; set; }

        public TableEntityAttribute() { }

        public TableEntityAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
