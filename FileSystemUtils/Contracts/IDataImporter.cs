namespace FileSystemUtils.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    /// <summary>
    /// Interface describing classes that import data to the database through some DbContext object.
    /// Data may not be send immediately to the server for performance reasons. Always call IDataImporter.FinalizeImporting() 
    /// before disposing IDataImporter because data loss may occur.
    /// </summary>
    public interface IDataImporter
    {
        /// <summary>
        /// Imports data to DbContext. 
        /// </summary>
        /// <param name="data"></param>
        void ImportData(IList<object> data);

        /// <summary>
        /// Needed to save all the cached and unsaved DbContext changes.
        /// </summary>
        void FinalizeImporting();
    }
}