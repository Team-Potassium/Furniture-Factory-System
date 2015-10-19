namespace FurnitureFactory.Data.MySql.Models
{
    using System.Collections.Generic;
    using Telerik.OpenAccess.Metadata.Fluent;

    public partial class SalesModelConfiguration : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations =
                new List<MappingConfiguration>();

            var salesMapping = new MappingConfiguration<SalesTotalCostReport>();
            salesMapping.MapType(report => new
            {
                Id = report.Id,
                Name = report.Name,
                TotalCost = report.TotalCost,
            }).ToTable("Sales");
            salesMapping.HasProperty(c => c.Id).IsIdentity();

            configurations.Add(salesMapping);

            return configurations;
        }
    }
}
