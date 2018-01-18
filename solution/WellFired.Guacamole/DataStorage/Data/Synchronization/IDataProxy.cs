namespace WellFired.Guacamole.DataStorage.Data.Synchronization
{
	/// <summary>
	/// Classes implementing this interface can be provided to <see cref="DataAccess"/> to synchronize data between your data proxy and any storage.
	/// <see cref="IDataProxy"/> can be seen as a cached version of the storage data which is synchronized with the storage. For JSON serialization,
	/// <see cref="DataProxy{T}"/> is already provided.
	/// <seealso cref="DataAccess"/><seealso cref="DataProxy{T}"/>
	/// </summary>
	public interface IDataProxy
	{
		/// <summary>
		/// Indicate if the data changed since it was loaded in.
		/// </summary>
		bool DataChanged { get; }
		/// <summary>
		/// Allows to inject serialized data into the data proxy to initialize it. 
		/// </summary>
		/// <param name="data"></param>
		void InjectData(string data);
		/// <summary>
		/// Allows to get serialized data from the proxy.
		/// </summary>
		/// <returns></returns>
		string GetData();
		/// <summary>
		/// After calling this method, <see cref="DataChanged"/> will return <c>false</c> until the data from the proxy is modified.
		/// </summary>
		void ResetDataChanged();
	}
}