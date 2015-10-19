namespace FileSystemUtils
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface describing classes that load data to DbContext.
    /// </summary>
    public interface IDataLoader
    {
        void LoadData(IList<Object> data);
    }
}
