using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureFactory.Data.MySql.Models
{
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
                Id = report.ID,
                Name = report.Name,
                EmailAddress = report.EmailAddress,
                DateCreated = report.DateCreated
            }).ToTable("Customer");
            salesMapping.HasProperty(c => c.ID).IsIdentity();

            configurations.Add(salesMapping);

            return configurations;
        }
    }
}
