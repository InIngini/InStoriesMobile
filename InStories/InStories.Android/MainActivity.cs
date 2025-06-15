using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using InStories;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace com.companyname.instories
{
    [Activity(
        Name = "com.companyname.instories.MainActivity",
        Label = "InStories",
        Theme = "@style/MainTheme",
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            // 1. Базовая инициализация (обязательно первая строка)
            base.OnCreate(savedInstanceState);

            try
            {
                // 2. Инициализация Forms (без Essentials для теста)
                Forms.Init(this, savedInstanceState);

                // 3. Упрощенная загрузка приложения
                var app = new App();
                LoadApplication(app);
            }
            catch (System.Exception ex)
            {
                Android.Util.Log.Error("APP_INIT", $"CRASH: {ex}");
                throw;
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}