using LLMConnectorLibrary.Authentication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using WordHiddenPowers.EventsBus;


namespace WordHiddenPowers.Repository
{
	partial class ProfilesCollection : IEnumerable<IAuthenticationProfile>
	{
		public void InitializeTypesTables()
		{
			ApiTypes.InitializeTypesTable();
			ProfileTypes.InitializeTypesTable();		
			GlobalsEventsBus.DoInitializeSetting(this);
		}

		public void AddRange(IEnumerable<IAuthenticationProfile> profiles)
		{
			foreach (IAuthenticationProfile profile in profiles)
			{
				Profiles.Add(profile);
			}
			GlobalsEventsBus.DoInitializeSetting(this);
		}

		public void Clear()
		{
			Profiles.Clear();
			ApiTypes.Clear();
			ProfileTypes.Clear();

			GlobalsEventsBus.DoInitializeSetting(this);
		}

		public Bitmap GetBitmap(IAuthenticationProfile profile)
		{
			ProfilesRow row = Profiles.Get(profile.ClientOptions.Endpoint.OriginalString);
			if (row != null) 
			{
				byte[] byteArray = Convert.FromBase64String(row.icon);
				return ByteArrayToBitmap(byteArray);
			}
			else
				return null;
		}

		public void SetIcon(IAuthenticationProfile profile, Bitmap bitmap)
		{
			ProfilesRow row = Profiles.Get(profile.ClientOptions.Endpoint.OriginalString);
			if (row != null)
			{
				byte[] byteArray = BitmapToByteArray(bitmap);
				string icon = Convert.ToBase64String(byteArray);
				row.BeginEdit();
				row.icon = icon;
				row.EndEdit();
			}
		}

		private static Bitmap ByteArrayToBitmap(byte[] byteArray)
		{
			using MemoryStream ms = new(byteArray);
			return new Bitmap(ms);
		}

		private static byte[] BitmapToByteArray(Bitmap bitmap)
		{
			using MemoryStream stream = new();
			bitmap.Save(stream, ImageFormat.Png);
			return stream.ToArray();
		}
		
		public IEnumerator<IAuthenticationProfile> GetEnumerator() => new AuthenticationProfileEnum(Profiles);

		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
				
		partial class ProfilesDataTable
		{
			public void Add(IAuthenticationProfile profile)
			{
				int typeId;
				int apiId;
				string thumbprint = string.Empty;
				string accessToken = string.Empty;
				double accesTokenLifeTime = 0;

				int oauthPingTimeout = 0;
				double oauthTimeout = 0;
				string oauthEndpoint = string.Empty;
				double oauthAccessTokenLifeTime = 0;

				if (profile.ApiType == ApiTypeEnum.OpenAI
					&& profile is IDefaultAuthenticationProfile<OpenAI.OpenAIClient>)
				{
					typeId = 0;
					apiId = 0;
				}
				else if (profile.ApiType == ApiTypeEnum.GigaChat
					&& profile is IDefaultAuthenticationProfile<GigaChat.GigaChatClient>)
				{
					typeId = 0;
					apiId = 1;
				}

				else if (profile.ApiType == ApiTypeEnum.OpenAI
					&& profile is IPreAuthenticationProfile<OpenAI.OpenAIClient> openAiPreAuthentication)
				{
					typeId = 2;
					apiId = 0;
					accessToken = openAiPreAuthentication.AccessToken;
					accesTokenLifeTime = openAiPreAuthentication.AccessTokenLifeTime.TotalMilliseconds;

					oauthPingTimeout = openAiPreAuthentication.OauthClientOptions.PingTimeout;
					oauthTimeout = openAiPreAuthentication.OauthClientOptions.Timeout.TotalMilliseconds;
					oauthEndpoint = openAiPreAuthentication.OauthClientOptions.Endpoint.AbsoluteUri;
					oauthAccessTokenLifeTime = openAiPreAuthentication.OauthAccessTokenLifeTime.TotalMilliseconds;
				}
				else if (profile.ApiType == ApiTypeEnum.GigaChat
					&& profile is IPreAuthenticationProfile<GigaChat.GigaChatClient> gigaChatPreAuthentication)
				{
					typeId = 2;
					apiId = 1;
					accessToken = gigaChatPreAuthentication.AccessToken;
					accesTokenLifeTime = gigaChatPreAuthentication.AccessTokenLifeTime.TotalMilliseconds;

					oauthPingTimeout = gigaChatPreAuthentication.OauthClientOptions.PingTimeout;
					oauthTimeout = gigaChatPreAuthentication.OauthClientOptions.Timeout.TotalMilliseconds;
					oauthEndpoint = gigaChatPreAuthentication.OauthClientOptions.Endpoint.AbsoluteUri;
					oauthAccessTokenLifeTime = gigaChatPreAuthentication.OauthAccessTokenLifeTime.TotalMilliseconds;
				}

				else if (profile.ApiType == ApiTypeEnum.OpenAI
					&& profile is IAuthorizationBearerAuthenticationProfile<OpenAI.OpenAIClient> openAiBearerAuthentication)
				{
					typeId = 1;
					apiId = 0;
					accessToken = openAiBearerAuthentication.AccessToken;
					accesTokenLifeTime = openAiBearerAuthentication.AccessTokenLifeTime.TotalMilliseconds;
				}
				else if (profile.ApiType == ApiTypeEnum.GigaChat
					&& profile is IAuthorizationBearerAuthenticationProfile<GigaChat.GigaChatClient> gigaChatBearerAuthentication)
				{
					typeId = 1;
					apiId = 1;
					accessToken = gigaChatBearerAuthentication.AccessToken;
					accesTokenLifeTime = gigaChatBearerAuthentication.AccessTokenLifeTime.TotalMilliseconds;
				}

				else if (profile.ApiType == ApiTypeEnum.OpenAI
					&& profile is ICertificateAuthenticationProfile<OpenAI.OpenAIClient> openAiCertificateProfile)
				{
					typeId = 3;
					apiId = 0;
					thumbprint = openAiCertificateProfile.Certificate == null ? string.Empty : openAiCertificateProfile.Certificate.Thumbprint;
				}
				else if (profile.ApiType == ApiTypeEnum.GigaChat
					&& profile is ICertificateAuthenticationProfile<GigaChat.GigaChatClient> gigaChatCertificateProfile)
				{
					typeId = 3;
					apiId = 1;
					thumbprint = gigaChatCertificateProfile.Certificate == null ? string.Empty : gigaChatCertificateProfile.Certificate.Thumbprint;
				}
				else
					throw new ArgumentException();

				Rows.Add(
					null,
					profile.Caption,
					profile.ClientOptions.PingTimeout,
					profile.ClientOptions.Endpoint.AbsoluteUri,
					profile.ClientOptions.Timeout.TotalMilliseconds,
					typeId,
					apiId,
					thumbprint,
					accessToken,
					accesTokenLifeTime,
					oauthPingTimeout,
					oauthEndpoint,
					oauthTimeout,
					oauthAccessTokenLifeTime);
			}

			public ProfilesRow Get(string endpoint)
			{
				return (from ProfilesRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row.endpoint == endpoint
						select row).FirstOrDefault();
			}

			public bool Exists(string endpoint)
			{
				return (from ProfilesRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row.endpoint == endpoint
						select row).Any();
			}

			public void Remove(string endpoint)
			{
				if (Exists(endpoint))
				{
					RemoveProfilesRow(Get(endpoint));
				}
			}
		}

		partial class ApiTypesDataTable
		{
			public const string OPEN_AI_API = "OpenAI";
			public const string GIGA_CHAT_API = "GigaChat";

			public void InitializeTypesTable()
			{
				if (!Exists(apiType: ApiTypeEnum.OpenAI)) Add(ApiTypeEnum.OpenAI);
				if (!Exists(apiType: ApiTypeEnum.GigaChat)) Add(ApiTypeEnum.GigaChat);
			}

			private void Add(ApiTypeEnum apiType)
			{
				switch (apiType)
				{
					case ApiTypeEnum.OpenAI:
						Rows.Add(0, OPEN_AI_API);
						break;
					case ApiTypeEnum.GigaChat:
						Rows.Add(1, GIGA_CHAT_API);
						break;
					default:
						throw new ArgumentException();
				}
			}

			private ApiTypesRow Get(ApiTypeEnum apiType)
			{
				return apiType switch
				{
					ApiTypeEnum.OpenAI => Get(OPEN_AI_API),
					ApiTypeEnum.GigaChat => Get(GIGA_CHAT_API),
					_ => throw new ArgumentException(),
				};
			}

			private bool Exists(ApiTypeEnum apiType)
			{
				return apiType switch
				{
					ApiTypeEnum.OpenAI => Exists(OPEN_AI_API),
					ApiTypeEnum.GigaChat => Exists(GIGA_CHAT_API),
					_ => throw new ArgumentException(),
				};
			}

			private ApiTypesRow Get(string name)
			{
				return (from ApiTypesRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row.name == name
						select row).FirstOrDefault();
			}

			private bool Exists(string name)
			{
				return (from ApiTypesRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row.name == name
						select row).Any();
			}
		}

		partial class ProfileTypesDataTable
		{
			public const string DEFAULT_TYPE = "Default";
			public const string BEARER_TYPE = "AuthorizationBearer";
			public const string PRE_AUTHENTICATION_TYPE = "PreAuthentication";
			public const string CERTIFICATE_TYPE = "Certificate";

			public void InitializeTypesTable()
			{
				if (!Exists(authenticationType: AuthenticationTypeEnum.Default)) Add(AuthenticationTypeEnum.Default);
				if (!Exists(authenticationType: AuthenticationTypeEnum.AuthorizationBearer)) Add(AuthenticationTypeEnum.AuthorizationBearer);
				if (!Exists(authenticationType: AuthenticationTypeEnum.PreAuthentication)) Add(AuthenticationTypeEnum.PreAuthentication);
				if (!Exists(authenticationType: AuthenticationTypeEnum.Certificate)) Add(AuthenticationTypeEnum.Certificate);
			}

			private void Add(AuthenticationTypeEnum authenticationTyp)
			{
				switch (authenticationTyp)
				{
					case AuthenticationTypeEnum.Default:
						Rows.Add(0, DEFAULT_TYPE);
						break;
					case AuthenticationTypeEnum.AuthorizationBearer:
						Rows.Add(1, BEARER_TYPE);
						break;
					case AuthenticationTypeEnum.PreAuthentication:
						Rows.Add(2, PRE_AUTHENTICATION_TYPE);
						break;
					case AuthenticationTypeEnum.Certificate:
						Rows.Add(3, CERTIFICATE_TYPE);
						break;
					default:
						throw new ArgumentException();
				}
			}

			private ProfileTypesRow Get(AuthenticationTypeEnum authenticationType)
			{
				return authenticationType switch
				{
					AuthenticationTypeEnum.Default => Get(DEFAULT_TYPE),
					AuthenticationTypeEnum.AuthorizationBearer => Get(BEARER_TYPE),
					AuthenticationTypeEnum.PreAuthentication => Get(PRE_AUTHENTICATION_TYPE),
					AuthenticationTypeEnum.Certificate => Get(CERTIFICATE_TYPE),
					_ => throw new ArgumentException(),
				};
			}

			private bool Exists(AuthenticationTypeEnum authenticationType)
			{
				return authenticationType switch
				{
					AuthenticationTypeEnum.Default => Exists(DEFAULT_TYPE),
					AuthenticationTypeEnum.AuthorizationBearer => Exists(BEARER_TYPE),
					AuthenticationTypeEnum.PreAuthentication => Exists(PRE_AUTHENTICATION_TYPE),
					AuthenticationTypeEnum.Certificate => Exists(CERTIFICATE_TYPE),
					_ => throw new ArgumentException(),
				};
			}

			private ProfileTypesRow Get(string name)
			{
				return (from ProfileTypesRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row.name == name
						select row).FirstOrDefault();
			}

			private bool Exists(string name)
			{
				return (from ProfileTypesRow row in Rows
						where row.RowState != DataRowState.Deleted
						&& row.name == name
						select row).Any();
			}
		}

		private class AuthenticationProfileEnum(ProfilesDataTable profilesRows) : IEnumerator<IAuthenticationProfile>
		{
			private readonly IList<ProfilesRow> profilesRows = [.. from ProfilesRow row in profilesRows.Rows
																					 where row.RowState != DataRowState.Deleted
																					 select row];
			int position = -1;

			public bool MoveNext()
			{
				position++;
				return position < profilesRows.Count();
			}
			public void Reset() => position = -1;
			
			public void Dispose()
			{
				//throw new NotImplementedException();
			}

			object IEnumerator.Current => Current;

			public IAuthenticationProfile Current
			{
				get
				{
					try
					{
						var apiType = profilesRows[position].api_type_id switch
						{
							0 => ApiTypeEnum.OpenAI,
							1 => ApiTypeEnum.GigaChat,
							_ => throw new ArgumentException(),
						};
						switch (profilesRows[position].type_id)
						{
							case 0:
								return AuthenticationProfileFactory.DefaultAuthenticationProfile(
									api: apiType,
									caption: profilesRows[position].caption,
									pingTimeout: profilesRows[position].pingTimeout,
									timeout: TimeSpan.FromMilliseconds(profilesRows[position].timeout),
									endpoint: new Uri(profilesRows[position].endpoint)
									);
							case 1:
								return AuthenticationProfileFactory.AuthorizationBearerAuthenticationProfile(
									api: apiType,
									caption: profilesRows[position].caption,
									pingTimeout: profilesRows[position].pingTimeout,
									timeout: TimeSpan.FromMilliseconds(profilesRows[position].timeout),
									endpoint: new Uri(profilesRows[position].endpoint),
									profilesRows[position].access_token,
									accessTokenLifeTime: TimeSpan.FromMilliseconds(profilesRows[position].access_token_life_time));
							case 2:
								return AuthenticationProfileFactory.PreAuthenticationProfile(
									api: apiType,
									caption: profilesRows[position].caption,
									oauthPingTimeout: profilesRows[position].oauthPingTimeout,
									oauthTimeout: TimeSpan.FromMilliseconds(profilesRows[position].oauthTimeout),
									oauthEndpoint: new Uri(profilesRows[position].oauthEndpoint),
									oauthAccessTokenLifeTime: TimeSpan.FromMilliseconds(profilesRows[position].oauth_access_token_life_time),
									pingTimeout: profilesRows[position].pingTimeout,
									timeout: TimeSpan.FromMilliseconds(profilesRows[position].timeout),
									endpoint: new Uri(profilesRows[position].endpoint),
									accessToken: profilesRows[position].access_token,
									accessTokenLifeTime: TimeSpan.FromMilliseconds(profilesRows[position].access_token_life_time));
							case 3:
								X509Certificate2 certificate = Services.CertificatesStore.FindByThumbprint(profilesRows[position].thumbprint) ?? null;
								return AuthenticationProfileFactory.CertificateAuthenticationProfile(
									api: apiType,
									caption: profilesRows[position].caption,
									pingTimeout: profilesRows[position].pingTimeout,
									timeout: TimeSpan.FromMilliseconds(profilesRows[position].timeout),
									endpoint: new Uri(profilesRows[position].endpoint),
									certificate: certificate);
							default:
								throw new ArgumentException();
						}
					}
					catch (Exception)
					{
						throw new InvalidOperationException();
					}
				}
			}
		}
	}
}
