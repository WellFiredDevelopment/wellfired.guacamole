using System;

namespace WellFired.Guacamole.DataStorage.Storages
{
	/// <summary>
	/// <see cref="ComputerDataStorage"/> is used to save data that should be shared between applications in a key value fashion.
	/// on MacOs it is saved at /Users/[current user]/.local/share/[Company Name]/[Application Name]/Keys.
	/// On Window at C:\Users\[current user]\AppData\Local\[Company Name]\[Application Name]\Keys
	/// and on Linux at /home/jariq/.local/share/[Company Name]/[Application Name]/Keys
	/// </summary>
	public class ComputerDataStorage : FileStorageService
	{
		private const string Location = "Keys";
		private static readonly string  DataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		public ComputerDataStorage(string applicationName, string companyName) : base($"{DataFolder}/{companyName}/{applicationName}/{Location}")
		{
		}
	}
}