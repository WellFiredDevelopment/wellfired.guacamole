using System.Collections.Generic;
using WellFired.Guacamole.StoredData.Exceptions;

namespace WellFired.Guacamole.StoredData.Synchronization
{
	public class DataCacher : IDataCacher
	{
		private readonly Dictionary<string, IDataProxy> _dataProxyDic = new Dictionary<string, IDataProxy>();

		public void Cache(string key, IDataProxy dataProxy)
		{
			if (_dataProxyDic.ContainsKey(key))
			{
				throw new ProxyDataAlreadyAssignedException(key);
			}
			
			_dataProxyDic.Add(key, dataProxy);
		}

		public string GetData(string key)
		{
			return _dataProxyDic[key].GetData();
		}

		public bool DidDataChanged(string key)
		{
			return _dataProxyDic[key].DataChanged;
		}

		public void UpdateData(string key, string dataContent)
		{
			_dataProxyDic[key].InjectData(dataContent);
		}

		public void ResetDataChanged(string key)
		{
			_dataProxyDic[key].ResetDataChanged();
		}
	}
}