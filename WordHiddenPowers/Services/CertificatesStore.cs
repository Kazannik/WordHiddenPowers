using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace WordHiddenPowers.Services
{
	internal class CertificatesStore
	{
		/// <summary>
		/// Коллекция сертификатов с политиками применения 
		/// "1.3.6.1.5.5.7.3.1" - проверка подлинности сервера
		/// и "1.3.6.1.5.5.7.3.2" - проверка подлинности клиента.
		/// </summary>
		/// <returns></returns>
		public static X509Certificate2Collection GetSertificates()
		{
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				Oid oidServerAuthentication = new Oid("1.3.6.1.5.5.7.3.1"); // OID for Server Authentication
				Oid oidClientAuthentication = new Oid("1.3.6.1.5.5.7.3.2"); // OID for Client Authentication

				X509Certificate2Collection serverAuthenticationCertificates = store.Certificates.Find(X509FindType.FindByApplicationPolicy, oidServerAuthentication.Value, false);
				X509Certificate2Collection clientAuthenticationCertificates = store.Certificates.Find(X509FindType.FindByApplicationPolicy, oidClientAuthentication.Value, false);

				X509Certificate2Collection result = new X509Certificate2Collection();

				foreach (X509Certificate2 certificate in serverAuthenticationCertificates)
				{
					if (!result.Contains(certificate))
						result.Add(certificate);
				}

				foreach (X509Certificate2 certificate in clientAuthenticationCertificates)
				{
					if (!result.Contains(certificate))
						result.Add(certificate);
				}

				return result;
			}
		}

		/// <summary>
		/// Поиск сертификата по отпечатку. 
		/// </summary>
		/// <param name="thumbprint">Отпечаток сертификата.</param>
		/// <returns></returns>
		public static X509Certificate2 FindByThumbprint(string thumbprint)
		{
			// Code to retrieve certificates by thumbprint from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);
				if (certificates.Count > 0)
					return certificates[0];
				else
					return null;
			}
		}


		static void Main1()
		{
			string storeName = "My"; // Хранилище личных сертификатов
			StoreLocation storeLocation = StoreLocation.CurrentUser; // Хранилище текущего пользователя

			using (X509Store store = new X509Store(storeName, storeLocation))
			{
				store.Open(OpenFlags.ReadWrite);

				// Получение коллекции сертификатов
				X509Certificate2Collection certificates = store.Certificates;

				// Перебор сертификатов и работа с ними
				foreach (X509Certificate2 cert in certificates)
				{
					Console.WriteLine("Субъект: " + cert.Subject);
					Console.WriteLine("Издатель: " + cert.Issuer);
				}
			}
		}

		static void Main2()
		{
			Console.WriteLine("\r\nExists Certs Name and Location");
			Console.WriteLine("------ ----- -------------------------");

			foreach (StoreLocation storeLocation in (StoreLocation[])Enum.GetValues(typeof(StoreLocation)))
			{
				foreach (StoreName storeName in (StoreName[])Enum.GetValues(typeof(StoreName)))
				{
					X509Store store = new X509Store(storeName, storeLocation);

					try
					{
						store.Open(OpenFlags.OpenExistingOnly);

						Console.WriteLine("Yes    {0,4}  {1}, {2}",
							store.Certificates.Count, store.Name, store.Location);
					}
					catch (CryptographicException)
					{
						Console.WriteLine("No           {0}, {1}",
							store.Name, store.Location);
					}
				}
				Console.WriteLine();
			}
		}


		public static void Main3(string[] args)
		{
			//Create new X509 store called teststore from the local certificate store.
			X509Store store = new X509Store("teststore", StoreLocation.CurrentUser);
			store.Open(OpenFlags.ReadWrite);
			X509Certificate2 certificate = new X509Certificate2();

			//Create certificates from certificate files.
			//You must put in a valid path to three certificates in the following constructors.
			X509Certificate2 certificate1 = new X509Certificate2("c:\\mycerts\\*****.cer");
			X509Certificate2 certificate2 = new X509Certificate2("c:\\mycerts\\*****.cer");
			X509Certificate2 certificate5 = new X509Certificate2("c:\\mycerts\\*****.cer");

			//Create a collection and add two of the certificates.
			X509Certificate2Collection collection = new X509Certificate2Collection();
			collection.Add(certificate2);
			collection.Add(certificate5);

			//Add certificates to the store.
			store.Add(certificate1);
			store.AddRange(collection);

			X509Certificate2Collection storecollection = (X509Certificate2Collection)store.Certificates;
			Console.WriteLine("Store name: {0}", store.Name);
			Console.WriteLine("Store location: {0}", store.Location);
			foreach (X509Certificate2 x509 in storecollection)
			{
				Console.WriteLine("certificate name: {0}", x509.Subject);
			}

			//Remove a certificate.
			store.Remove(certificate1);
			X509Certificate2Collection storecollection2 = (X509Certificate2Collection)store.Certificates;
			Console.WriteLine("{1}Store name: {0}", store.Name, Environment.NewLine);
			foreach (X509Certificate2 x509 in storecollection2)
			{
				Console.WriteLine("certificate name: {0}", x509.Subject);
			}

			//Remove a range of certificates.
			store.RemoveRange(collection);
			X509Certificate2Collection storecollection3 = (X509Certificate2Collection)store.Certificates;
			Console.WriteLine("{1}Store name: {0}", store.Name, Environment.NewLine);
			if (storecollection3.Count == 0)
			{
				Console.WriteLine("Store contains no certificates.");
			}
			else
			{
				foreach (X509Certificate2 x509 in storecollection3)
				{
					Console.WriteLine("certificate name: {0}", x509.Subject);
				}
			}

			//Close the store.
			store.Close();
		}


		static void Main4()
		{
			try
			{
				// Specify the store name and location
				string storeName = "My"; // My store contains personal certificates
				StoreLocation storeLocation = StoreLocation.CurrentUser; // Use CurrentUser for per-user store or LocalMachine for machine-wide store

				// Create an instance of the X509Store class
				using (X509Store store = new X509Store(storeName, storeLocation))
				{
					// Open the store
					store.Open(OpenFlags.ReadOnly);

					// Retrieve all certificates from the store
					X509Certificate2Collection certificates = store.Certificates;

					// Enumerate through the certificates and display information about each certificate
					foreach (X509Certificate2 cert in certificates)
					{
						Console.WriteLine("Subject: " + cert.Subject);
						Console.WriteLine("Issuer: " + cert.Issuer);
						Console.WriteLine("Thumbprint: " + cert.Thumbprint);
						Console.WriteLine("Valid From: " + cert.NotBefore);
						Console.WriteLine("Valid Until: " + cert.NotAfter);
						Console.WriteLine();
					}

					// Close the store
					store.Close();
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
			}
		}


		private void find()
		{

			// Code to retrieve all certificates from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				foreach (var certificate in store.Certificates)
				{
					// Access each certificate
				}
			}

			using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
			{
				store.Open(OpenFlags.ReadOnly);

				foreach (var certificate in store.Certificates)
				{
					// Access each certificate
				}
			}

			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				var subjectName = "CN=ExampleCertificate";
				var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, subjectName, false);

				foreach (var certificate in certificates)
				{
					// Access each matching certificate
				}
			}

			// Code to retrieve certificates by thumbprint from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				var thumbprint = "1234567890ABCDEF1234567890ABCDEF12345678";
				var certificates = store.Certificates.Find(X509FindType.FindByThumbprint, thumbprint, false);

				foreach (var certificate in certificates)
				{
					// Access each matching certificate
				}
			}


			// Code to retrieve certificates by issuer name from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				var issuerName = "CN=ExampleCA";
				var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, issuerName, false);

				foreach (var certificate in certificates)
				{
					// Access each matching certificate
				}
			}


			// Code to retrieve certificates by expiration date from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				var expirationDate = DateTime.Now.AddDays(30);
				var certificates = store.Certificates.Find(X509FindType.FindByTimeExpired, expirationDate, false);

				foreach (var certificate in certificates)
				{
					// Access each matching certificate
				}
			}


			// Code to retrieve certificates by enhanced key usage from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				var oid = new Oid("1.3.6.1.5.5.7.3.1"); // OID for Server Authentication
				var certificates = store.Certificates.Find(X509FindType.FindByApplicationPolicy, oid.Value, false);

				foreach (var certificate in certificates)
				{
					// Access each matching certificate
				}
			}


			// Code to retrieve certificates by key usage from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				var keyUsage = X509KeyUsageFlags.KeyEncipherment;
				var certificates = store.Certificates.Find(X509FindType.FindByKeyUsage, keyUsage, false);

				foreach (var certificate in certificates)
				{
					// Access each matching certificate
				}
			}


			// Code to retrieve certificates by friendly name from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				var friendlyName = "ExampleCert";
				var certificates = store.Certificates.Find(X509FindType.FindBySubjectName, friendlyName, false);

				foreach (var certificate in certificates)
				{
					// Access each matching certificate
				}
			}


			// Code to retrieve certificates by serial number from X509Store in C#
			using (var store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
			{
				store.Open(OpenFlags.ReadOnly);

				var serialNumber = "1234567890ABCDEF1234567890ABCDEF";
				var certificates = store.Certificates.Find(X509FindType.FindBySerialNumber, serialNumber, false);

				foreach (var certificate in certificates)
				{
					// Access each matching certificate
				}
			}


		}

	}
}


