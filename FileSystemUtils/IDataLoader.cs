using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemUtils
{
    /// <summary>
    /// Interface describing classes that load data to DbContext.
    /// </summary>
    public interface IDataLoader
    {
        void LoadData(IList<Object> data);
    }
}
