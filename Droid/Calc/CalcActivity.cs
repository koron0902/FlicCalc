
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace FlickCalc.Droid.Calc {
  [Activity(Label = "CalcActivity", MainLauncher = true)]
  public class CalcActivity : Activity {
    int cnt = 0;
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.calculator);
      // Create your application here
    }

    protected override void OnStart() {
      base.OnStart();

      var param = new WindowManagerLayoutParams(
    WindowManagerLayoutParams.WrapContent,
    WindowManagerLayoutParams.WrapContent,
    WindowManagerTypes.DrawnApplication,
    WindowManagerFlags.NotFocusable | WindowManagerFlags.NotTouchable | WindowManagerFlags.NotTouchModal,
    Format.Translucent);


      var layout = LayoutInflater.Inflate(Resource.Layout.test, null);
      GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
      var aaa = layout.FindViewById<TextView>(Resource.Id.textView1);

      FindViewById<Button>(Resource.Id.button1).Click += (sender, e) => {
        aaa.Text = (++cnt).ToString();
        if(cnt == 101) {

          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
        }
      };
    }
  }
}
