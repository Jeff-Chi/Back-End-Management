using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Managemrnt.EFCore
{
    public static class ModelBuilderExtensions
    {
        /// <summary>
        /// 处理表名
        /// </summary>
        /// <param name="entityTypeBuilder"></param>
        /// <returns></returns>
        public static EntityTypeBuilder ToTable(this EntityTypeBuilder entityTypeBuilder)
        {
            Type entityType = entityTypeBuilder.Metadata.ClrType;
            TableAttribute? attr = entityType.GetCustomAttribute<TableAttribute>();
            if (attr == null)
            {
                entityTypeBuilder.ToTable(entityType.Name);
            }
            else
            {
                entityTypeBuilder.ToTable(attr.Name);
            }
            return entityTypeBuilder;
        }
    }
}
