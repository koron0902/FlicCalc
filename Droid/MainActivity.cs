using Android.App;
using Android.Widget;
using Android.OS;

namespace FlickCalc.Droid {
  [Activity(Label = "FlickCalc", Icon = "@mipmap/icon")]
  public class MainActivity : Activity {
    int count = 1;

    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // Get our button from the layout resource,
      // and attach an event to it
      Button button = FindViewById<Button>(Resource.Id.myButton);
      RPN rpn = new RPN();
      button.Text = rpn.Proc("1+2*3+4/5");

      button.Click += delegate { button.Text = $"{count++} clicks!"; };
    }
  }
}

