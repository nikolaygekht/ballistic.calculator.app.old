using Android.App;
using Android.Widget;
using Android.OS;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Android.Content;
using Java.Lang;
using System;

namespace Gehtsoft.BallisticCalculator.Utils
{
	public class ApkUpdater : AsyncTask
	{
		private string _applicationPath;
		private string _applicationName;
		private string _updateServerAddress;
		private int _updateServerPort;

		private int _thisVertion;
		private Context _ctx;

		private ProgressDialog _progressDialog;

		private UpdaterState _updateState;

		enum UpdaterState
		{
			NETWORK_ERROR,
			STORAGE_ACCESS_ERROR,
			NO_UPDATES,
			UPDATE_WILL_BE_INSTALLED,
		}

		public ApkUpdater(int thisVersion, Context ctx, string updateServerAddress, int updateServerPort)
		{
			_thisVertion = thisVersion;
			_ctx = ctx;
			_applicationPath = "/sdcard/BallisticCalculatorUpdates";
		    _applicationName = "ballisticcalculator.apk";
		    _updateServerAddress = updateServerAddress;
		    _updateServerPort = updateServerPort;
			_updateState = UpdaterState.NETWORK_ERROR;
		}

		private bool applyUpdate()
		{
			string updStr = getUpdateString();
			if (updStr == null)
				return false;

			string[] parties = updStr.Split(';');
			if (parties == null)
				return false;
			if (parties.Length != 2)
				return false;

			try
			{
				int newVersion = Integer.ParseInt(parties[0]);
				if (newVersion <= _thisVertion)
				{
					_updateState = UpdaterState.NO_UPDATES;
					return true;
				}
			}
			catch (NumberFormatException)
			{
				return false;
			}

			string url = parties[1];
			if (!url.StartsWith("http://", StringComparison.CurrentCulture))
				return false;

			int pos = url.IndexOf('\n');
			if (pos == -1)
				return false;

			url = url.Substring(0, pos);

			if (!createDirIfNeed(_applicationPath))
				return false;

			try
			{
				WebClient webClient = new WebClient();
				webClient.DownloadFile(url, _applicationPath + "/" + _applicationName);
			}
			catch (Java.Lang.Exception e)
			{
				return false;
			}
			catch (System.Exception e)
			{
				return false;
			}

			if (new Java.IO.File(_applicationPath + "/" + _applicationName).Exists())
			{
				_updateState = UpdaterState.UPDATE_WILL_BE_INSTALLED;
				return true;
			}

			return false;
		}

		private bool createDirIfNeed(string dir)
		{
			var javaDir = new Java.IO.File(dir);
			var javaFile = new Java.IO.File(_applicationPath + "/" + _applicationName);

			if (javaFile.Exists())
			{
				if (!javaFile.Delete())
				{
					_updateState = UpdaterState.STORAGE_ACCESS_ERROR;
					return false;
				}
			}

			if (!javaDir.Exists())
			{
				if (!javaDir.Mkdirs())
				{
					_updateState = UpdaterState.STORAGE_ACCESS_ERROR;
					return false;
				}
			}

			if (!javaDir.CanWrite())
			{
				_updateState = UpdaterState.STORAGE_ACCESS_ERROR;
				return false;
			}

			return true;
		}

		protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
		{
			return applyUpdate();
		}

		protected override void OnPreExecute()
		{
			base.OnPreExecute();
			_progressDialog = ProgressDialog.Show(_ctx, "Update in progress", 
                "Downloading a new version of The Ballistic Calculator...");

		}

		protected override void OnPostExecute(Java.Lang.Object result)
		{
			base.OnPostExecute(result);

			_progressDialog.Hide();

			if (_updateState == UpdaterState.NETWORK_ERROR)
			{
				new AlertDialog.Builder(_ctx)
				.SetTitle("Error")               
				.SetPositiveButton("OK", (sender, e) => { })
				.SetMessage("Update has been failed. Please, try again later.")
				.Show();
			}
			else if (_updateState == UpdaterState.UPDATE_WILL_BE_INSTALLED)
			{
				new AlertDialog.Builder(_ctx)
					.SetMessage("A new version will be installed.")
					.SetPositiveButton("OK", (sender, e) =>
					{

						var fileObj = new Java.IO.File(_applicationPath + "/" + _applicationName);
						Android.Net.Uri contentUri = Android.Net.Uri.FromFile(fileObj);
						Intent i = new Intent();
						i.SetAction(Intent.ActionView);
						i.SetDataAndType(contentUri, "application/vnd.android.package-archive");
						_ctx.StartActivity(i);

					})
				.Show();
			}
			else if (_updateState == UpdaterState.NO_UPDATES)
			{
				new AlertDialog.Builder(_ctx)
				        .SetPositiveButton("OK", (sender, e) => { })
						.SetMessage("You already have the latest version of Ballistic Calculator installed")
						.Show();
			}
			else if (_updateState == UpdaterState.STORAGE_ACCESS_ERROR)
			{
				new AlertDialog.Builder(_ctx)
				        .SetTitle("Error")
						.SetMessage("Cannot reda/write device's file sytem.")
						.Show();
			}
		}

		public string getUpdateString()
		{
			try
			{
				byte[] bytes = new byte[1024];

				IPHostEntry ipHost = Dns.GetHostEntry(_updateServerAddress);
				IPAddress ipAddr = ipHost.AddressList[0];
				IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, _updateServerPort);

				var sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

				sender.Connect(ipEndPoint);

				string message = "get_version" + "\n";

				byte[] msg = Encoding.UTF8.GetBytes(message);

				int bytesSent = sender.Send(msg);

				int bytesRec = sender.Receive(bytes);

				string str = Encoding.UTF8.GetString(bytes);

				sender.Shutdown(SocketShutdown.Both);
				sender.Close();
				return str;
			}
			catch (System.Exception e)
			{
				return null;
			}
		}
	}
}
