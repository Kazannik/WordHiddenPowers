using ControlLibrary.Controls.TimeControls;
using LLMConnectorLibrary.Authentication;
using Microsoft.Office.Interop.Word;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;
using WordHiddenPowers.Dialogs;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace WordHiddenPowers.Controls.AuthenticationProfileListControl
{
	[ToolboxItem(false)]
	public partial class AuthenticationProfileListItem : UserControl
	{
		private bool drawFocusedBorder;

		private readonly AuthenticationProfileListBox parent;

		private bool isEvents;

		private Label captionLabel;
		private TextBox captionTextBox;

		private Button closeButton;

		private Label apiLabel;
		private ListItemComboBox apiComboBox;

		private Label authenticationLabel;
		private ListItemComboBox authenticationComboBox;

		private ConnectionControlBox oauthConnectionBox;

		private Label optionallyLabel;
		private TextBox optionallyTextBox;

		private Button certificateBrowserButton;

		private Label accessTokenLifeTimeLabel;
		private TimeBox accessTokenLifeTimeBox;

		private ConnectionControlBox connectionBox;

		private IAuthenticationProfile profile;

		private string _caption;
		private ApiTypeEnum _apiType;
		private AuthenticationTypeEnum _authenticationType;

		private string _accessToken;
		private TimeSpan _accessTokenLifeTime;
		private X509Certificate2 _certificate;

		private int _pingTimeout;
		private TimeSpan _timeout;
		private Uri _uri;

		private int _oauthPingTimeout;
		private TimeSpan _oauthTimeout;
		private Uri _oauthUri;

		#region Profile Properties

		public string Caption
		{
			get => _caption;
			set
			{
				if (_caption != value)
				{
					_caption = value;
					DoProfileCaptionChanged();
				}
			}
		}

		public ApiTypeEnum ApiType
		{
			get => _apiType;
			set
			{
				if (_apiType != value)
				{
					_apiType = value;
					DoProfileApiTypeChanged();
				}
			}
		}

		public AuthenticationTypeEnum AuthenticationType
		{
			get => _authenticationType;
			set
			{
				if (value != _authenticationType)
				{
					_authenticationType = value;
					DoProfileAuthenticationTypeChanged();
				}
			}
		}

		public string AccessToken
		{
			get => _accessToken;
			set
			{
				if (value != _accessToken)
				{
					_accessToken = value;
					DoProfileAccessTokenChanged();
				}
			}
		}

		public TimeSpan AccessTokenLifeTime
		{
			get => _accessTokenLifeTime;
			set
			{
				if (value != _accessTokenLifeTime)
				{
					_accessTokenLifeTime = value;
					DoProfileAccessTokenLifeTimeChanged();
				}
			}
		}

		public X509Certificate2 Certificate
		{
			get => _certificate;
			set
			{
				if (value != _certificate)
				{
					_certificate = value;
					DoProfileCertificateChanged();
				}
			}
		}

		public int PingTimeout
		{
			get => _pingTimeout;
			set
			{
				if (value != _pingTimeout)
				{
					_pingTimeout = value;
					DoProfilePingTimeoutChanged();
				}
			}
		}

		public TimeSpan Timeout
		{
			get => _timeout;
			set
			{
				if (value != _timeout)
				{
					_timeout = value;
					DoProfileTimeoutChanged();
				}
			}
		}

		public Uri Uri
		{
			get => _uri;
			set
			{
				if (value != _uri)
				{
					_uri = value;
					DoProfileUriChanged();
				}
			}
		}

		public int OauthPingTimeout
		{
			get => _oauthPingTimeout;
			set
			{
				if (value != _oauthPingTimeout)
				{
					_oauthPingTimeout = value;
					DoProfileOauthPingTimeoutChanged();
				}
			}
		}

		public TimeSpan OauthTimeout
		{
			get => _oauthTimeout;
			set
			{
				if (value != _oauthTimeout)
				{
					_oauthTimeout = value;
					DoProfileOauthTimeoutChanged();
				}
			}
		}

		public Uri OauthUri
		{
			get => _oauthUri;
			set
			{
				if (value != _oauthUri)
				{
					_oauthUri = value;
					DoProfileOauthUriChanged();
				}
			}
		}

		public IAuthenticationProfile Profile
		{
			get => profile;
			set
			{
				if (value != profile)
				{
					profile = value;
					DoProfileChanged();
				}
			}
		}

		#endregion

		#region Profile Events

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileCaptionChanged;
		protected virtual void OnProfileCaptionChanged(ItemEventArgs e) => ProfileCaptionChanged?.Invoke(this, e);
		private void DoProfileCaptionChanged()
		{
			captionTextBox.Text = Caption;

			if (isEvents)
			{
				OnProfileCaptionChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileApiTypeChanged;
		protected virtual void OnProfileApiTypeChanged(ItemEventArgs e) => ProfileApiTypeChanged?.Invoke(this, e);
		private void DoProfileApiTypeChanged()
		{
			apiComboBox.SelectedIndex = ApiType switch
			{
				ApiTypeEnum.OpenAI => 0,
				ApiTypeEnum.GigaChat => 1,
				_ => 0
			};				

			if (isEvents)
			{
				OnProfileApiTypeChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileAuthenticationTypeChanged;
		protected virtual void OnProfileAuthenticationTypeChanged(ItemEventArgs e) => ProfileAuthenticationTypeChanged?.Invoke(this, e);
		private void DoProfileAuthenticationTypeChanged()
		{
			authenticationComboBox.SelectedIndex = AuthenticationType switch
			{
				AuthenticationTypeEnum.Default => 0,
				AuthenticationTypeEnum.AuthorizationBearer => 1,
				AuthenticationTypeEnum.PreAuthentication => 2,
				AuthenticationTypeEnum.Certificate => 3,
				_ => 0,
			};

			this.profile = CreateProfile(
				caption: _caption,
				apiType: _apiType,
				authenticationType: _authenticationType,
				accessToken: _accessToken,
				accessTokenLifeTime: _accessTokenLifeTime,
				certificate: _certificate,
				pingTimeout: _pingTimeout,
				timeout: _timeout,
				uri: _uri,
				oauthPingTimeout: _oauthPingTimeout,
				oauthTimeout: _oauthTimeout,
				oauthUri: _oauthUri);

			InitializeProfileComponents();

			InitializeProfile();

			ResizeComponents();

			if (isEvents)
			{
				OnProfileAuthenticationTypeChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileAccessTokenChanged;
		protected virtual void OnProfileAccessTokenChanged(ItemEventArgs e) => ProfileAccessTokenChanged?.Invoke(this, e);
		private void DoProfileAccessTokenChanged()
		{
			if (AuthenticationType == AuthenticationTypeEnum.AuthorizationBearer |
				AuthenticationType == AuthenticationTypeEnum.PreAuthentication)
			{
				optionallyTextBox.Text = AccessToken;
			}

			if (isEvents)
			{
				OnProfileAccessTokenChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileAccessTokenLifeTimeChanged;
		protected virtual void OnProfileAccessTokenLifeTimeChanged(ItemEventArgs e) => ProfileAccessTokenLifeTimeChanged?.Invoke(this, e);
		private void DoProfileAccessTokenLifeTimeChanged()
		{
			if (AuthenticationType == AuthenticationTypeEnum.AuthorizationBearer |
				AuthenticationType == AuthenticationTypeEnum.PreAuthentication)
			{
				accessTokenLifeTimeBox.Value = AccessTokenLifeTime;
			}

			if (isEvents)
			{
				OnProfileAccessTokenLifeTimeChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileCertificateChanged;
		protected virtual void OnProfileCertificateChanged(ItemEventArgs e) => ProfileCertificateChanged?.Invoke(this, e);
		private void DoProfileCertificateChanged()
		{
			if (AuthenticationType == AuthenticationTypeEnum.Certificate)
			{
				optionallyTextBox.Text = GetCertificateName(Certificate);
			}

			if (isEvents)
			{
				OnProfileCertificateChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfilePingTimeoutChanged;
		protected virtual void OnProfilePingTimeoutChanged(ItemEventArgs e) => ProfilePingTimeoutChanged?.Invoke(this, e);
		private void DoProfilePingTimeoutChanged()
		{
			connectionBox.PingTimeout = PingTimeout;

			if (isEvents)
			{
				OnProfilePingTimeoutChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileTimeoutChanged;
		protected virtual void OnProfileTimeoutChanged(ItemEventArgs e) => ProfileTimeoutChanged?.Invoke(this, e);
		private void DoProfileTimeoutChanged()
		{
			connectionBox.Timeout = Timeout;

			if (isEvents)
			{
				OnProfileTimeoutChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileUriChanged;
		protected virtual void OnProfileUriChanged(ItemEventArgs e) => ProfileUriChanged?.Invoke(this, e);
		private void DoProfileUriChanged()
		{
			connectionBox.Uri = Uri;

			if (isEvents)
			{
				OnProfileUriChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileOauthPingTimeoutChanged;
		protected virtual void OnProfileOauthPingTimeoutChanged(ItemEventArgs e) => ProfileOauthPingTimeoutChanged?.Invoke(this, e);
		private void DoProfileOauthPingTimeoutChanged()
		{
			if (AuthenticationType == AuthenticationTypeEnum.AuthorizationBearer |
				AuthenticationType == AuthenticationTypeEnum.PreAuthentication)
			{
				oauthConnectionBox.PingTimeout = OauthPingTimeout;
			}

			if (isEvents)
			{
				OnProfileOauthPingTimeoutChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileOauthTimeoutChanged;
		protected virtual void OnProfileOauthTimeoutChanged(ItemEventArgs e) => ProfileOauthTimeoutChanged?.Invoke(this, e);
		private void DoProfileOauthTimeoutChanged()
		{
			if (AuthenticationType == AuthenticationTypeEnum.AuthorizationBearer |
				AuthenticationType == AuthenticationTypeEnum.PreAuthentication)
			{
				oauthConnectionBox.Timeout = OauthTimeout;
			}

			if (isEvents)
			{
				OnProfileOauthTimeoutChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileOauthUriChanged;
		protected virtual void OnProfileOauthUriChanged(ItemEventArgs e) => ProfileOauthUriChanged?.Invoke(this, e);
		private void DoProfileOauthUriChanged()
		{
			if (AuthenticationType == AuthenticationTypeEnum.AuthorizationBearer |
				AuthenticationType == AuthenticationTypeEnum.PreAuthentication)
			{
				oauthConnectionBox.Uri = OauthUri;
			}

			if (isEvents)
			{
				OnProfileOauthUriChanged(new ItemEventArgs(this));
				DoProfileChanged();
			}
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemEventArgs> ProfileChanged;
		protected virtual void OnProfileChanged(ItemEventArgs e) => ProfileChanged?.Invoke(this, e);
		private void DoProfileChanged()
		{
			this.profile = CreateProfile(
				caption: _caption,
				apiType: _apiType,
				authenticationType: _authenticationType,
				accessToken: _accessToken,
				accessTokenLifeTime: _accessTokenLifeTime,
				certificate: _certificate,
				pingTimeout: _pingTimeout,
				timeout: _timeout,
				uri: _uri,
				oauthPingTimeout: _oauthPingTimeout,
				oauthTimeout: _oauthTimeout,
				oauthUri: _oauthUri);

			if (isEvents) OnProfileChanged(new ItemEventArgs(this));
			CheckConnection();
		}

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemConnectionEventArgs> StateChanged;
		protected virtual void OnStateChanged(ItemConnectionEventArgs e) => StateChanged?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemConnectionEventArgs> PingChanged;
		protected virtual void OnPingChanged(ItemConnectionEventArgs e) => PingChanged?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemConnectionEventArgs> Connecting;
		protected virtual void OnConnecting(ItemConnectionEventArgs e) => Connecting?.Invoke(this, e);

		[Category("Behavior"), Browsable(true), EditorBrowsable(EditorBrowsableState.Always)]
		public event EventHandler<ItemConnectionEventArgs> Connected;
		protected virtual void OnConnected(ItemConnectionEventArgs e) => Connected?.Invoke(this, e);

		#endregion

		public static IAuthenticationProfile CreateProfile(
			string caption,
			ApiTypeEnum apiType,
			AuthenticationTypeEnum authenticationType,
			string accessToken,
			TimeSpan accessTokenLifeTime,
			X509Certificate2 certificate,
			int pingTimeout,
			TimeSpan timeout,
			Uri uri,
			int oauthPingTimeout,
			TimeSpan oauthTimeout,
			Uri oauthUri)
		{
			return authenticationType switch
			{
				AuthenticationTypeEnum.Default => AuthenticationProfileFactory.DefaultAuthenticationProfile(
											api: apiType,
											caption: caption,
											pingTimeout: pingTimeout,
											timeout: timeout,
											endpoint: uri),
				AuthenticationTypeEnum.AuthorizationBearer => AuthenticationProfileFactory.AuthorizationBearerAuthenticationProfile(
											api: apiType,
											caption: caption,
											pingTimeout: pingTimeout,
											timeout: timeout,
											endpoint: uri,
											accessToken: accessToken,
											accessTokenLifeTime: TimeSpan.Zero),
				AuthenticationTypeEnum.PreAuthentication => AuthenticationProfileFactory.PreAuthenticationProfile(
											api: apiType,
											caption: caption,
											oauthPingTimeout: oauthPingTimeout,
											oauthTimeout: oauthTimeout,
											oauthEndpoint: oauthUri,
											oauthAccessTokenLifeTime: accessTokenLifeTime,
											pingTimeout: pingTimeout,
											timeout: timeout,
											endpoint: uri,
											accessToken: accessToken,
											accessTokenLifeTime: TimeSpan.Zero),
				AuthenticationTypeEnum.Certificate => AuthenticationProfileFactory.CertificateAuthenticationProfile(
											api: apiType,
											caption: caption,
											pingTimeout: pingTimeout,
											timeout: timeout,
											endpoint: uri,
											certificate: certificate),
				_ => throw new ArgumentException(),
			};
		}

		/// <summary>
		/// Временно приостанавливает обработку событий, связанных со свойствами профиля.
		/// </summary>
		private void SuspendEvents()
		{
			isEvents = false;
		}

		/// <summary>
		/// Возобновляет обработку событий, связанных со свойствами профиля.
		/// </summary>
		private void ResumeEvents()
		{
			isEvents = true;
		}

		private void InitializeComponents()
		{
			closeButton = new Button();
			captionLabel = new Label();
			captionTextBox = new TextBox();
			apiLabel = new Label();
			apiComboBox = new ListItemComboBox();
			connectionBox = new ConnectionControlBox(Profile.HttpClient);
			authenticationLabel = new Label();
			authenticationComboBox = new ListItemComboBox();
			SuspendLayout();
			// 
			// captionLabel
			// 
			captionLabel.AutoSize = true;
			captionLabel.Name = "captionLabel";
			captionLabel.TabIndex = 0;
			captionLabel.Text = "Name:";
			// 
			// captionTextBox
			// 
			captionTextBox.Name = "captionTextBox";
			captionTextBox.TabIndex = 1;
			captionTextBox.TextChanged += new EventHandler(NameTextBox_TextChanged);
			captionTextBox.Enter += new EventHandler(Component_Enter);
			captionTextBox.Leave += new EventHandler(Component_Leave);
			// 
			// apiLabel
			// 
			apiLabel.AutoSize = true;
			apiLabel.Name = "apiLabel";
			apiLabel.TabIndex = 2;
			apiLabel.Text = "API:";
			// 
			// apiComboBox
			// 
			apiComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			apiComboBox.FormattingEnabled = true;
			apiComboBox.Name = "apiComboBox";
			apiComboBox.Size = new Size(100, 24);
			apiComboBox.TabIndex = 3;
			apiComboBox.SelectedIndexChanged += new EventHandler(ApiComboBox_SelectedIndexChanged);
			apiComboBox.Enter += new EventHandler(Component_Enter);
			apiComboBox.Leave += new EventHandler(Component_Leave);
			// 
			// authenticationLabel
			// 
			authenticationLabel.AutoSize = true;
			authenticationLabel.Name = "authenticationLabel";
			authenticationLabel.TabIndex = 4;
			authenticationLabel.Text = "Authentication:";
			// 
			// authenticationComboBox
			// 
			authenticationComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			authenticationComboBox.FormattingEnabled = true;
			authenticationComboBox.Name = "authenticationComboBox";
			authenticationComboBox.Size = new Size(144, 24);
			authenticationComboBox.TabIndex = 5;
			authenticationComboBox.SelectedIndexChanged += new EventHandler(AuthenticationComboBox_SelectedIndexChanged);
			authenticationComboBox.Enter += new EventHandler(Component_Enter);
			authenticationComboBox.Leave += new EventHandler(Component_Leave);
			// 
			// connectionBox
			// 
			connectionBox.Caption = "Server Address:";
			connectionBox.ForeColor = SystemColors.WindowText;
			connectionBox.Name = "connectionBox";
			connectionBox.Size = new Size(438, 48);
			connectionBox.TabIndex = 12;
			connectionBox.AddressChanged += new EventHandler(ConnectionControlBox_AddressChanged);
			connectionBox.PingTimeoutChanged += new EventHandler(ConnectionControlBox_PingTimeoutChanged);
			connectionBox.TimeoutChanged += new EventHandler(ConnectionControlBox_TimeoutChanged);
			connectionBox.StateChanged += new EventHandler<ConnectionControlBox.ConnectionEventArgs>(ConnectionControlBox_StateChanged);
			connectionBox.PingChanged += new EventHandler<ConnectionControlBox.ConnectionEventArgs>(ConnectionControlBox_PingChanged);
			connectionBox.Connecting += new EventHandler<ConnectionControlBox.ConnectionEventArgs>(ConnectionControlBox_Connecting);
			connectionBox.Connected += new EventHandler<ConnectionControlBox.ConnectionEventArgs>(ConnectionControlBox_Connected);
			connectionBox.Enter += new EventHandler(Component_Enter);
			connectionBox.Leave += new EventHandler(Component_Leave);
			// 
			// closeButton
			// 
			closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			closeButton.FlatStyle = FlatStyle.Flat;
			closeButton.Image = Properties.Resources.WindowClose_16;
			closeButton.Name = "closeButton";
			closeButton.Size = new Size(16, 16);
			closeButton.TabIndex = 13;
			closeButton.Click += new EventHandler(CloseButton_Click);
			closeButton.Enter += new EventHandler(Component_Enter);
			closeButton.Leave += new EventHandler(Component_Leave);
			// 
			// AuthenticationProfileListItem
			// 
			AutoScaleMode = AutoScaleMode.Inherit;
			Controls.Add(closeButton);
			Controls.Add(captionLabel);
			Controls.Add(captionTextBox);
			Controls.Add(apiLabel);
			Controls.Add(apiComboBox);
			Controls.Add(authenticationLabel);
			Controls.Add(authenticationComboBox);
			Controls.Add(connectionBox);
			Name = "AuthenticationProfileListItem";
			ResumeLayout(false);
			PerformLayout();
		}

		private void InitializeMasterData()
		{
			apiComboBox.Items.Add("Open AI");
			apiComboBox.Items.Add("GigaChat");

			authenticationComboBox.Items.Add("Default");
			authenticationComboBox.Items.Add("Authorization Bearer");
			authenticationComboBox.Items.Add("Pre Authentication");
			authenticationComboBox.Items.Add("Certificate");
		}

		private void InitializeProfileComponents()
		{
			SuspendLayout();

			RemoveControl(ref optionallyLabel);
			RemoveControl(ref optionallyTextBox);
			RemoveControl(ref certificateBrowserButton);
			RemoveControl(ref accessTokenLifeTimeLabel);
			RemoveControl(ref accessTokenLifeTimeBox);
			RemoveControl(ref oauthConnectionBox);

			switch (AuthenticationType)
			{
				case AuthenticationTypeEnum.AuthorizationBearer:
					optionallyLabel = new Label();
					optionallyTextBox = new TextBox();

					optionallyLabel.Text = "Token:";
					break;
				case AuthenticationTypeEnum.PreAuthentication:
					optionallyLabel = new Label();
					optionallyTextBox = new TextBox();

					optionallyLabel.Text = "Token:";
					accessTokenLifeTimeLabel = new Label();
					accessTokenLifeTimeBox = new TimeBox();
					oauthConnectionBox = new ConnectionControlBox(Profile.OauthHttpClient);
					break;
				case AuthenticationTypeEnum.Certificate:
					optionallyLabel = new Label();
					optionallyTextBox = new TextBox();

					optionallyLabel.Text = "Certificate:";
					certificateBrowserButton = new Button();
					break;
			}

			if (optionallyLabel != null)
			{
				// 
				// optionallyLabel
				// 
				optionallyLabel.AutoSize = true;
				optionallyLabel.Name = "optionallyLabel";
				optionallyLabel.TabIndex = 6;
				Controls.Add(optionallyLabel);
			}
			if (optionallyTextBox != null)
			{
				// 
				// optionallyTextBox
				// 
				optionallyTextBox.Name = "optionallyTextBox";
				optionallyTextBox.TabIndex = 7;
				optionallyTextBox.TextChanged += new EventHandler(OptionallyTextBox_TextChanged);
				optionallyTextBox.Enter += new EventHandler(Component_Enter);
				optionallyTextBox.Leave += new EventHandler(Component_Leave);
				Controls.Add(optionallyTextBox);
			}
			if (certificateBrowserButton != null)
			{
				// 
				// certificateBrowserButton
				// 
				certificateBrowserButton.Name = "certificateBrowserButton";
				certificateBrowserButton.Size = new Size(100, 27);
				certificateBrowserButton.TabIndex = 8;
				certificateBrowserButton.Text = "Обзор...";
				certificateBrowserButton.Click += new EventHandler(CertificateBrowserButton_Click);
				certificateBrowserButton.Enter += new EventHandler(Component_Enter);
				certificateBrowserButton.Leave += new EventHandler(Component_Leave);
				Controls.Add(certificateBrowserButton);
			}
			if (oauthConnectionBox != null)
			{
				// 
				// oauthConnectionBox
				// 
				oauthConnectionBox.BackColor = SystemColors.Window;
				oauthConnectionBox.Caption = "Oauth Server Address:";
				oauthConnectionBox.Name = "oauthConnectionBox";
				oauthConnectionBox.PingTimeout = 100;
				oauthConnectionBox.TabIndex = 11;
				oauthConnectionBox.Timeout = TimeSpan.Parse("00:03:00");
				oauthConnectionBox.Uri = null;
				oauthConnectionBox.AddressChanged += new EventHandler(OauthConnectionControlBox_AddressChanged);
				oauthConnectionBox.PingTimeoutChanged += new EventHandler(OauthConnectionControlBox_PingTimeoutChanged);
				oauthConnectionBox.TimeoutChanged += new EventHandler(OauthConnectionControlBox_TimeoutChanged);
				oauthConnectionBox.StateChanged += new EventHandler<ConnectionControlBox.ConnectionEventArgs>(OauthConnectionControlBox_StateChanged);
				oauthConnectionBox.PingChanged += new EventHandler<ConnectionControlBox.ConnectionEventArgs>(OauthConnectionControlBox_PingChanged);
				oauthConnectionBox.Connecting += new EventHandler<ConnectionControlBox.ConnectionEventArgs>(OauthConnectionControlBox_Connecting);
				oauthConnectionBox.Connected += new EventHandler<ConnectionControlBox.ConnectionEventArgs>(OauthConnectionControlBox_Connected);
				oauthConnectionBox.Enter += new EventHandler(Component_Enter);
				oauthConnectionBox.Leave += new EventHandler(Component_Leave);
				Controls.Add(oauthConnectionBox);
			}
			if (accessTokenLifeTimeBox != null)
			{
				// 
				// accessTokenLifeTimeBox
				// 
				accessTokenLifeTimeBox.MaxValue = new TimeSpan(days: 0, hours: 23, minutes: 59, seconds: 59, milliseconds: 999);
				accessTokenLifeTimeBox.Name = "accessTokenLifeTimeBox";
				accessTokenLifeTimeBox.TabIndex = 10;
				accessTokenLifeTimeBox.Value = TimeSpan.Parse("00:00:00");
				accessTokenLifeTimeBox.View = TimeBox.TimeControlUnitFlags.All;
				accessTokenLifeTimeBox.ValueChanged += new EventHandler(AccessTokenLifeTimeBox_ValueChanged); 
				accessTokenLifeTimeBox.Enter += new EventHandler(Component_Enter);
				accessTokenLifeTimeBox.Leave += new EventHandler(Component_Leave);
				Controls.Add(accessTokenLifeTimeBox);
			}
			if (accessTokenLifeTimeLabel != null)
			{
				// 
				// accessTokenLifeTimeLabel
				// 
				accessTokenLifeTimeLabel.AutoSize = true;
				accessTokenLifeTimeLabel.Name = "accessTokenLifeTimeLabel";
				accessTokenLifeTimeLabel.TabIndex = 9;
				accessTokenLifeTimeLabel.Text = "Access Token LifeTime:";
				Controls.Add(accessTokenLifeTimeLabel);
			}
			// 
			// AuthenticationProfileListItem
			// 
			DoBackColor();
			ResumeLayout(false);
			Invalidate();
		}
		
		private void RemoveControl(ref Label label)
		{
			Control control = label;
			RemoveControl(control: ref control);
			label = null;
		}

		private void RemoveControl(ref TextBox textBox)
		{
			Control control = textBox;
			RemoveControl(control: ref control);
			textBox = null;
		}

		private void RemoveControl(ref Button button)
		{
			Control control = button;
			RemoveControl(control: ref control);
			button = null;
		}

		private void RemoveControl(ref TimeBox timeBox)
		{
			Control control = timeBox;
			RemoveControl(control: ref control);
			timeBox = null;
		}

		private void RemoveControl(ref ConnectionControlBox connectionBox)
		{
			Control control = connectionBox;
			RemoveControl(control: ref control);
			connectionBox = null;
		}

		private void RemoveControl(ref Control control)
		{
			if (control != null)
			{
				if (Controls.Contains(control))
					Controls.Remove(control);
				control.Dispose();
			}
		}

		internal readonly string guid = Guid.NewGuid().ToString();

		public bool IsDouble
		{
			get
			{
				for (int i = 0; i < Index; i++)
				{
					if (Profile.Equals(parent[i])) return true;
				}
				return false;
			}
		}

		public AuthenticationProfileListItem(AuthenticationProfileListBox parent)
			: this(parent: parent, profile: AuthenticationProfileFactory.Default) { }

		public AuthenticationProfileListItem(AuthenticationProfileListBox parent, IAuthenticationProfile profile)
		{
			drawFocusedBorder = false;
			this.parent = parent;
			this.profile = profile;

			SetValues(profile: Profile,
				caption: ref _caption,
				apiType: ref _apiType,
				authenticationType: ref _authenticationType,
				accessToken: ref _accessToken,
				accessTokenLifeTime: ref _accessTokenLifeTime,
				certificate: ref _certificate,
				pingTimeout: ref _pingTimeout,
				timeout: ref _timeout,
				uri: ref _uri,
				oauthPingTimeout: ref _oauthPingTimeout,
				oauthTimeout: ref _oauthTimeout,
				oauthUri: ref _oauthUri);

			ResumeEvents();

			InitializeComponents();

			InitializeMasterData();

			InitializeProfileComponents();

			InitializeProfile();

			ResizeComponents();

			CheckConnection();
		}

		private void InitializeProfile()
		{
			SuspendEvents();

			captionTextBox.Text = Profile.Caption;
			apiComboBox.SelectedIndex = Profile.ApiType == ApiTypeEnum.OpenAI ? 0 : 1;

			connectionBox.PingTimeout = Profile.ClientOptions.PingTimeout;
			connectionBox.Timeout = Profile.ClientOptions.Timeout;
			connectionBox.Uri = Profile.ClientOptions.Endpoint;

			if (Profile.AuthenticationType == AuthenticationTypeEnum.Default)
			{
				authenticationComboBox.SelectedIndex = 0;
			}

			else if (Profile is IPreAuthenticationProfile<OpenAI.OpenAIClient> openAiPreTokenProfile)
			{
				authenticationComboBox.SelectedIndex = 2;

				optionallyTextBox.Text = openAiPreTokenProfile.AccessToken;
				accessTokenLifeTimeBox.Value = openAiPreTokenProfile.OauthAccessTokenLifeTime;
				oauthConnectionBox.PingTimeout = openAiPreTokenProfile.OauthClientOptions.PingTimeout;
				oauthConnectionBox.Timeout = openAiPreTokenProfile.OauthClientOptions.Timeout;
				oauthConnectionBox.Uri = openAiPreTokenProfile.OauthClientOptions.Endpoint;
			}
			else if (Profile is IPreAuthenticationProfile<GigaChat.GigaChatClient> gigaChatPreTokenProfile)
			{
				authenticationComboBox.SelectedIndex = 2;

				optionallyTextBox.Text = gigaChatPreTokenProfile.AccessToken;
				accessTokenLifeTimeBox.Value = gigaChatPreTokenProfile.OauthAccessTokenLifeTime;
				oauthConnectionBox.PingTimeout = gigaChatPreTokenProfile.OauthClientOptions.PingTimeout;
				oauthConnectionBox.Timeout = gigaChatPreTokenProfile.OauthClientOptions.Timeout;
				oauthConnectionBox.Uri = gigaChatPreTokenProfile.OauthClientOptions.Endpoint;
			}

			else if (Profile is IAuthorizationBearerAuthenticationProfile<OpenAI.OpenAIClient> openAiTokenProfile)
			{
				authenticationComboBox.SelectedIndex = 1;

				optionallyTextBox.Text = openAiTokenProfile.AccessToken;
			}
			else if (Profile is IAuthorizationBearerAuthenticationProfile<GigaChat.GigaChatClient> gigaChatTokenProfile)
			{
				authenticationComboBox.SelectedIndex = 1;

				optionallyTextBox.Text = gigaChatTokenProfile.AccessToken;
			}
			
			else if (Profile is ICertificateAuthenticationProfile<OpenAI.OpenAIClient> openAiCertProfile)
			{
				authenticationComboBox.SelectedIndex = 3;
				Certificate = openAiCertProfile.Certificate;
				optionallyTextBox.Text = GetCertificateName(Certificate);
			}
			else if (Profile is ICertificateAuthenticationProfile<GigaChat.GigaChatClient> gigaChatCertProfile)
			{
				authenticationComboBox.SelectedIndex = 3;
				Certificate = gigaChatCertProfile.Certificate;
				optionallyTextBox.Text = GetCertificateName(Certificate);
			}
			else
			{
				authenticationComboBox.SelectedIndex = 0;
			}

			ResumeEvents();
		}

		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			ResizeComponents();
		}

		protected override void OnResize(EventArgs e)
		{
			ResizeComponents();
			base.OnResize(e);
		}

		public void ResizeComponents()
		{
			int offset = 4;
			Point position = new(offset, 2);

			SuspendLayout();
			Width = parent.Width - SystemInformation.VerticalScrollBarWidth - SystemInformation.BorderSize.Width;

			// Close Button
			closeButton.Size = new Size(16, 16);
			closeButton.Location = new Point(Width - closeButton.Width - 2, position.Y);

			// Name
			SetLocatonComponent(new Point(offset, 8 + (closeButton.Height - captionTextBox.Height) / 2), Width - offset * 4 - closeButton.Width, captionLabel, captionTextBox, out position);

			// Api & Authentication Type
			SetLocatonComponent(position.NextLocation(offset), apiLabel.Width + apiComboBox.Width + 12, apiLabel, apiComboBox, out _);
			SetLocatonComponent(position.Left(apiComboBox.Location.X + apiComboBox.Width + 12), Width - offset - apiLabel.Width - apiComboBox.Width - 28, authenticationLabel, authenticationComboBox, out position);

			if (AuthenticationType == AuthenticationTypeEnum.AuthorizationBearer)
			{
				// Access
				SetLocatonComponent(position.NextLocation(offset), Width - offset * 2, optionallyLabel, optionallyTextBox, out position);
			}
			else if (AuthenticationType == AuthenticationTypeEnum.PreAuthentication)
			{
				// Access
				SetLocatonComponent(position.NextLocation(offset), Width - offset * 2, optionallyLabel, optionallyTextBox, out position);
				// Access Lifetime
				SetLocatonComponent(position.NextLocation(offset), Width - offset * 2, accessTokenLifeTimeLabel, accessTokenLifeTimeBox, out position);
				// Oauth Connection Box
				SetLocatonComponent(position.NextLocation(offset), Width - offset * 2, oauthConnectionBox, out position);
			}
			else if (AuthenticationType == AuthenticationTypeEnum.Certificate)
			{
				SetLocatonComponent(position.NextLocation(offset), Width - offset * 2 - certificateBrowserButton.Width, optionallyLabel, optionallyTextBox, out _);
				SetLocatonComponent(position.Left(Width - certificateBrowserButton.Width - 2), certificateBrowserButton.Width, certificateBrowserButton, out position);
			}

			SetLocatonComponent(position.NextLocation(offset), Width - offset * 2, connectionBox, out position);

			int height = position.Y + 2;

			if (Height != height)
			{
				Height = height;
				parent.ItemsResize();
			}
			ResumeLayout(false);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			if (drawFocusedBorder)
			{
				Pen pen = new(SystemColors.ControlDark, 2)
				{
					DashStyle = System.Drawing.Drawing2D.DashStyle.Dot
				};
				e.Graphics.DrawRectangle(pen,
					new Rectangle(new Point(1, 1), new Size(Width - 1, Height - 2)));
			}
			base.OnPaint(e);
		}

		protected override void OnBackColorChanged(EventArgs e)
		{
			DoBackColor();
			base.OnBackColorChanged(e);
		}

		private void DoBackColor()
		{
			foreach (Control item in Controls)
			{
				if (item is not ComboBox &
					item is not TextBox &
					item is not TimeBox) item.BackColor = BackColor;
			}
		}

		public int Index => parent.Controls.IndexOf(this);

		public int Top
		{
			get
			{
				if (Index <= 0)
					return AuthenticationProfileListBox.TOP_POSITION;
				else
					return ((AuthenticationProfileListItem)parent.Controls[Index - 1]).Top + ((AuthenticationProfileListItem)parent.Controls[Index - 1]).Height;
			}
		}

		private void CloseButton_Click(object sender, EventArgs e) =>
			parent.RemoveAt(Index);

		private void Component_Enter(object sender, EventArgs e)
		{
			drawFocusedBorder = true;
			Invalidate();
		}

		private void Component_Leave(object sender, EventArgs e)
		{
			drawFocusedBorder = false;
			Invalidate();
		}

		private void NameTextBox_TextChanged(object sender, EventArgs e)
		{
			Caption = captionTextBox.Text;
		}

		private void ApiComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			ApiType = GetApiComboBoxSelectedIndex() switch
			{
				0 => ApiTypeEnum.OpenAI,
				1 => ApiTypeEnum.GigaChat,
				_ => ApiTypeEnum.OpenAI,
			};
		}

		private void AuthenticationComboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			AuthenticationType = GetAuthenticationComboBoxSelectedIndex() switch
			{
				0 => AuthenticationTypeEnum.Default,
				1 => AuthenticationTypeEnum.AuthorizationBearer,
				2 => AuthenticationTypeEnum.PreAuthentication,
				3 => AuthenticationTypeEnum.Certificate,
				_ => AuthenticationTypeEnum.Default,
			};
		}

		private void CertificateBrowserButton_Click(object sender, EventArgs e)
		{
			CertificatesBrowser browser = new();
			if (browser.ShowDialog(this) == DialogResult.OK)
			{
				Certificate = browser.Certificate;
			}
		}

		private void OptionallyTextBox_TextChanged(object sender, EventArgs e)
		{
			TextBox textBox = sender as TextBox;
			if (AuthenticationType == AuthenticationTypeEnum.AuthorizationBearer ||
				AuthenticationType == AuthenticationTypeEnum.PreAuthentication)
			{
				AccessToken = textBox.Text;
			}			
		}

		private void ConnectionControlBox_AddressChanged(object sender, EventArgs e)
		{
			ConnectionControlBox control = sender as ConnectionControlBox;
			Uri = control.Uri;
		}

		private void AccessTokenLifeTimeBox_ValueChanged(object sender, EventArgs e)
		{
			TimeBox timeBox = sender as TimeBox;
			if (AuthenticationType == AuthenticationTypeEnum.PreAuthentication)
			{
				AccessTokenLifeTime = timeBox.Value;
			}
		}

		private void ConnectionControlBox_PingTimeoutChanged(object sender, EventArgs e)
		{
			ConnectionControlBox control = sender as ConnectionControlBox;
			PingTimeout = control.PingTimeout;
		}

		private void ConnectionControlBox_TimeoutChanged(object sender, EventArgs e)
		{
			ConnectionControlBox control = sender as ConnectionControlBox;
			Timeout = control.Timeout;
		}

		private void OauthConnectionControlBox_AddressChanged(object sender, EventArgs e)
		{
			ConnectionControlBox control = sender as ConnectionControlBox;
			OauthUri = control.Uri;
		}

		private void OauthConnectionControlBox_PingTimeoutChanged(object sender, EventArgs e)
		{
			ConnectionControlBox control = sender as ConnectionControlBox;
			OauthPingTimeout = control.PingTimeout;
		}

		private void OauthConnectionControlBox_TimeoutChanged(object sender, EventArgs e)
		{
			ConnectionControlBox control = sender as ConnectionControlBox;
			OauthTimeout = control.Timeout;
		}

		public void CheckConnection()
		{
			connectionBox.CheckConnection();
			oauthConnectionBox?.CheckConnection();
		}

		private void ConnectionControlBox_StateChanged(object sender, ConnectionControlBox.ConnectionEventArgs e)
		{
			OnStateChanged(new ItemConnectionEventArgs(this, e.State));
		}

		private void ConnectionControlBox_PingChanged(object sender, ConnectionControlBox.ConnectionEventArgs e)
		{
			OnPingChanged(new ItemConnectionEventArgs(this, e.State));
		}

		private void ConnectionControlBox_Connecting(object sender, ConnectionControlBox.ConnectionEventArgs e)
		{
			OnConnecting(new ItemConnectionEventArgs(this, e.State));
		}

		private void ConnectionControlBox_Connected(object sender, ConnectionControlBox.ConnectionEventArgs e)
		{
			OnConnected(new ItemConnectionEventArgs(this, e.State));
		}

		private void OauthConnectionControlBox_StateChanged(object sender, ConnectionControlBox.ConnectionEventArgs e)
		{
			OnStateChanged(new ItemConnectionEventArgs(this, e.State));
		}

		private void OauthConnectionControlBox_PingChanged(object sender, ConnectionControlBox.ConnectionEventArgs e)
		{
			OnPingChanged(new ItemConnectionEventArgs(this, e.State));
		}

		private void OauthConnectionControlBox_Connecting(object sender, ConnectionControlBox.ConnectionEventArgs e)
		{
			OnConnecting(new ItemConnectionEventArgs(this, e.State));
		}

		private void OauthConnectionControlBox_Connected(object sender, ConnectionControlBox.ConnectionEventArgs e)
		{
			OnConnected(new ItemConnectionEventArgs(this, e.State));
		}

		/// <summary>
		/// Рассчет прямоугольника, залючающего в себе элементы управления.
		/// </summary>
		/// <param name="location">Начальная координата.</param>
		/// <param name="width">Общая ширина метки и элемента управления.</param>
		/// <param name="label">Метка.</param>
		/// <param name="control">Элемент управления.</param>
		/// <param name="position">Позация нижнего левого угла.</param>
		/// <returns>Прямоугольник, залючающий в себе элементы управления.</returns>
		private static Rectangle SetLocatonComponent(Point location, int width, Label label, Control control, out Point position)
		{
			int offset = (control.Height - label.Height) / 2;
			label.Location = new Point(location.X, location.Y + offset);
			label.Invalidate();
			control.Location = new Point(label.Location.X + label.Width + 12, location.Y);
			control.Width = width - label.Width - 12;
			control.Invalidate();
			position = new Point(location.X, location.Y + control.Height);
			return new Rectangle(location, new Size(label.Width + control.Width + 12, control.Height));
		}

		/// <summary>
		/// Рассчет прямоугольника, залючающего в себе элементы управления.
		/// </summary>
		/// <param name="location">Начальная координата.</param>
		/// <param name="width">Общая ширина метки и элемента управления.</param>
		/// <param name="control">Элемент управления.</param>
		/// <param name="position">Позация нижнего левого угла.</param>
		/// <returns>Прямоугольник, залючающий в себе элементы управления.</returns>
		private static Rectangle SetLocatonComponent(Point location, int width, Control control, out Point position)
		{
			control.Location = new Point(location.X, location.Y);
			control.Width = width;
			control.Invalidate();
			position = new Point(location.X, location.Y + control.Height);
			return new Rectangle(location, new Size(control.Width, control.Height));
		}

		private int GetApiComboBoxSelectedIndex()
		{
			if (apiComboBox.InvokeRequired)
			{
				return (int)apiComboBox.Invoke(new Func<int>(GetApiComboBoxSelectedIndex));
			}
			else
			{
				return apiComboBox.SelectedIndex;
			}
		}

		private int GetAuthenticationComboBoxSelectedIndex()
		{
			if (authenticationComboBox.InvokeRequired)
			{
				return (int)authenticationComboBox.Invoke(new Func<int>(GetAuthenticationComboBoxSelectedIndex));
			}
			else
			{
				return authenticationComboBox.SelectedIndex;
			}
		}

		/// <summary>
		/// Задать значение переменных на основе профиля.
		/// </summary>
		/// <param name="profile"></param>
		private static void SetValues(
			IAuthenticationProfile profile,
			ref string caption,
			ref ApiTypeEnum apiType,
			ref AuthenticationTypeEnum authenticationType,
			ref string accessToken,
			ref TimeSpan accessTokenLifeTime,
			ref X509Certificate2 certificate,
			ref int pingTimeout,
			ref TimeSpan timeout,
			ref Uri uri,
			ref int oauthPingTimeout,
			ref TimeSpan oauthTimeout,
			ref Uri oauthUri)
		{
			caption = profile.Caption;
			apiType = profile.ApiType;
			authenticationType = GetAuthenticationType(profile);
			pingTimeout = profile.ClientOptions.PingTimeout;
			timeout = profile.ClientOptions.Timeout;
			uri = profile.ClientOptions.Endpoint;

			if (profile is IAuthorizationBearerAuthenticationProfile<OpenAI.OpenAIClient> openAiTokenProfile)
			{
				accessToken = openAiTokenProfile.AccessToken;
				accessTokenLifeTime = TimeSpan.Zero;

				oauthPingTimeout = 0;
				oauthTimeout = TimeSpan.Zero;
				oauthUri = null;
				certificate = null;
			}
			else if (profile is IAuthorizationBearerAuthenticationProfile<GigaChat.GigaChatClient> gigaChatTokenProfile)
			{
				accessToken = gigaChatTokenProfile.AccessToken;
				accessTokenLifeTime = TimeSpan.Zero;

				oauthPingTimeout = 0;
				oauthTimeout = TimeSpan.Zero;
				oauthUri = null;
				certificate = null;
			}
			else if (profile is IPreAuthenticationProfile<OpenAI.OpenAIClient> openAiPreTokenProfile)
			{
				accessToken = openAiPreTokenProfile.AccessToken;
				accessTokenLifeTime = openAiPreTokenProfile.OauthAccessTokenLifeTime;
				oauthPingTimeout = openAiPreTokenProfile.OauthClientOptions.PingTimeout;
				oauthTimeout = openAiPreTokenProfile.OauthClientOptions.Timeout;
				oauthUri = openAiPreTokenProfile.OauthClientOptions.Endpoint;

				certificate = null;
			}
			else if (profile is IPreAuthenticationProfile<GigaChat.GigaChatClient> gigaChatPreTokenProfile)
			{
				accessToken = gigaChatPreTokenProfile.AccessToken;
				accessTokenLifeTime = gigaChatPreTokenProfile.OauthAccessTokenLifeTime;
				oauthPingTimeout = gigaChatPreTokenProfile.OauthClientOptions.PingTimeout;
				oauthTimeout = gigaChatPreTokenProfile.OauthClientOptions.Timeout;
				oauthUri = gigaChatPreTokenProfile.OauthClientOptions.Endpoint;

				certificate = null;
			}
			else if (profile is ICertificateAuthenticationProfile<OpenAI.OpenAIClient> openAiCertProfile)
			{
				certificate = openAiCertProfile.Certificate;

				accessToken = string.Empty;
				accessTokenLifeTime = TimeSpan.Zero;
				oauthPingTimeout = 0;
				oauthTimeout = TimeSpan.Zero;
				oauthUri = null;
			}
			else if (profile is ICertificateAuthenticationProfile<GigaChat.GigaChatClient> gigaChatCertProfile)
			{
				certificate = gigaChatCertProfile.Certificate;

				accessToken = string.Empty;
				accessTokenLifeTime = TimeSpan.Zero;
				oauthPingTimeout = 0;
				oauthTimeout = TimeSpan.Zero;
				oauthUri = null;
			}
			else
			{
				accessToken = string.Empty;
				accessTokenLifeTime = TimeSpan.Zero;
				oauthPingTimeout = 0;
				oauthTimeout = TimeSpan.Zero;
				oauthUri = null;
				certificate = null;
			}
		}

		private static AuthenticationTypeEnum GetAuthenticationType(IAuthenticationProfile profile)
		{
			if (profile is IDefaultAuthenticationProfile<OpenAI.OpenAIClient> |
				profile is IDefaultAuthenticationProfile<GigaChat.GigaChatClient>)
			{
				return AuthenticationTypeEnum.Default;
			}
			else if (profile is IAuthorizationBearerAuthenticationProfile<OpenAI.OpenAIClient> |
				profile is IAuthorizationBearerAuthenticationProfile<GigaChat.GigaChatClient> gigaChatTokenProfile)
			{
				return AuthenticationTypeEnum.AuthorizationBearer;
			}
			else if (profile is IPreAuthenticationProfile<OpenAI.OpenAIClient> |
				profile is IPreAuthenticationProfile<GigaChat.GigaChatClient>)
			{
				return AuthenticationTypeEnum.PreAuthentication;
			}
			else if (profile is ICertificateAuthenticationProfile<OpenAI.OpenAIClient> |
				profile is ICertificateAuthenticationProfile<GigaChat.GigaChatClient>)
			{
				return AuthenticationTypeEnum.Certificate;
			}
			else
			{
				throw new ArgumentException();
			}
		}

		private static string GetCertificateName(X509Certificate2 certificate)
		{
			if (certificate != null)
				return !string.IsNullOrEmpty(certificate.FriendlyName) ?
					certificate.FriendlyName :
					CertificatesBrowser.GetName(certificate.SubjectName.Name);
			else
				return string.Empty;
		}

		public class ItemEventArgs(AuthenticationProfileListItem item) : EventArgs
		{
			public AuthenticationProfileListItem Item { get; } = item;
		}

		public class ItemConnectionEventArgs(AuthenticationProfileListItem item, ConnectionControlBox.StateEnum state) : EventArgs
		{
			public AuthenticationProfileListItem Item { get; } = item;

			public ConnectionControlBox.StateEnum State { get; } = state;
		}

		private class ListItemComboBox : ComboBox
		{
			protected override void OnMouseWheel(MouseEventArgs e)
			{
				if (e is HandledMouseEventArgs handledArgs)
				{
					handledArgs.Handled = true;
				}
				base.OnMouseWheel(e);
			}
		}
	}

	public static class PointExtensions
	{
		public static Point NextLocation(ref this Point location)
		{
			location = new Point(location.X, location.Y + 12);
			return location;
		}

		public static Point NextLocation(ref this Point location, int X)
		{
			location = new Point(X, location.Y + 12);
			return location;
		}

		public static Point Left(ref this Point location, int X)
		{
			location = new Point(X, location.Y);
			return location;
		}
	}
}
