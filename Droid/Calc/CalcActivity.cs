
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using static Java.Interop.JniEnvironment;

namespace FlickCalc.Droid.Calc {
  [Activity(Label = "CalcActivity", MainLauncher = true)]
  public class CalcActivity : Activity, ViewTreeObserver.IOnGlobalLayoutListener {
    int cnt = 0;
    List<string> formulaHistory_;
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.calculator);
      // Create your application here
      formulaHistory_ = new List<string>();

      // Test App ID
      MobileAds.Initialize(this,
              "ca-app-pub-9986685244078667~8077011042");

      AdView adView = FindViewById<AdView>(Resource.Id.adView);
      AdRequest adRequest = new Android.Gms.Ads.AdRequest.Builder()
        .AddTestDevice("7DE225BF3F8AD112ABC25493ADD6CEAF")
        .AddTestDevice("92B8012EAE306FF8394406C86DF51797")
        .Build();
      adView.LoadAd(adRequest);
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
      var aaa = layout.FindViewById<ImageView>(Resource.Id.selectedButton);

      float startX = 0, startY = 0;
      var selectedButton = Resource.Drawable.ic_0;
      var selected = "0";
      var formula = FindViewById<TextView>(Resource.Id.formula);
      #region 数値ボタン0~4の設定
      var button0_4 = FindViewById<ImageButton>(Resource.Id.button0_4);
      var img0_4 = Android.Graphics.BitmapFactory.DecodeResource(Resources, Resource.Drawable.num0_4);
      button0_4.ViewTreeObserver.GlobalLayout += (sender, e) => {
        button0_4.SetImageBitmap(Bitmap.CreateScaledBitmap(img0_4, button0_4.Width, button0_4.Height, true));
      };

      button0_4.Touch += (sender, e) => {
        switch(e.Event.Action) {
        case MotionEventActions.Down:
          GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
          startX = e.Event.GetX();
          startY = e.Event.GetY();
          aaa.SetImageResource(Resource.Drawable.ic_0);
          selectedButton = Resource.Drawable.ic_0;
          break;
        case MotionEventActions.Move:
          var area = AreaDetect(startX, startY, e.Event.GetX(), e.Event.GetY(), 100);
          switch(area) {
          case AREA.CENTER:
            selected = "0";
            selectedButton = Resource.Drawable.ic_0;
            break;
          case AREA.LEFT:
            selected = "1";
            selectedButton = Resource.Drawable.ic_1;
            break;
          case AREA.BOTTOM:
            selected = "2";
            selectedButton = Resource.Drawable.ic_2;
            break;
          case AREA.RIGHT:
            selected = "3";
            selectedButton = Resource.Drawable.ic_3;
            break;
          case AREA.TOP:
            selected = "4";
            selectedButton = Resource.Drawable.ic_4;
            break;
          }
          aaa.SetImageResource(selectedButton);
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          formula.Text += selected;
          break;
        case MotionEventActions.Cancel:
          Toast.MakeText(this, "Canceled!!!!!", ToastLength.Short).Show();
          break;
        }
      };
      #endregion

      #region 数値ボタン5~9の設定
      var button5_9 = FindViewById<ImageButton>(Resource.Id.button5_9);
      var img5_9 = Android.Graphics.BitmapFactory.DecodeResource(Resources, Resource.Drawable.num5_9);
      button5_9.ViewTreeObserver.GlobalLayout += (sender, e) => {
        button5_9.SetImageBitmap(Bitmap.CreateScaledBitmap(img5_9, button5_9.Width, button5_9.Height, true));
      };
      button5_9.Touch += (sender, e) => {
        switch(e.Event.Action) {
        case MotionEventActions.Down:
          GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
          startX = e.Event.GetX();
          startY = e.Event.GetY();
          aaa.SetImageResource(Resource.Drawable.ic_5);
          selectedButton = Resource.Drawable.ic_5;
          selected = "5";
          break;
        case MotionEventActions.Move:
          var area = AreaDetect(startX, startY, e.Event.GetX(), e.Event.GetY(), 100);
          switch(area) {
          case AREA.CENTER:
            selected = "5";
            selectedButton = Resource.Drawable.ic_5;
            break;
          case AREA.LEFT:
            selected = "6";
            selectedButton = Resource.Drawable.ic_6;
            break;
          case AREA.BOTTOM:
            selected = "7";
            selectedButton = Resource.Drawable.ic_7;
            break;
          case AREA.RIGHT:
            selected = "8";
            selectedButton = Resource.Drawable.ic_8;
            break;
          case AREA.TOP:
            selected = "9";
            selectedButton = Resource.Drawable.ic_9;
            break;
          }
          aaa.SetImageResource(selectedButton);
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          formula.Text += selected;
          break;
        }
      };
      #endregion

      #region  オペレータボタンの設定
      var buttonop = FindViewById<ImageButton>(Resource.Id.buttonop);
      var imgop = Android.Graphics.BitmapFactory.DecodeResource(Resources, Resource.Drawable.op);
      buttonop.ViewTreeObserver.GlobalLayout += (sender, e) => {
        buttonop.SetImageBitmap(Bitmap.CreateScaledBitmap(imgop, buttonop.Width, buttonop.Height, true));
      };
      buttonop.Touch += (sender, e) => {
        switch(e.Event.Action) {
        case MotionEventActions.Down:
          GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
          startX = e.Event.GetX();
          startY = e.Event.GetY();
          aaa.SetImageResource(Resource.Drawable.add);
          selectedButton = Resource.Drawable.add;
          selected = "+";
          break;
        case MotionEventActions.Move:
          var area = AreaDetect(startX, startY, e.Event.GetX(), e.Event.GetY(), 100);
          switch(area) {
          case AREA.CENTER:
            selected = "+";
            selectedButton = Resource.Drawable.add;
            break;
          case AREA.LEFT:
            selected = "-";
            selectedButton = Resource.Drawable.minus;
            break;
          case AREA.BOTTOM:
            selected = ".";
            selectedButton = Resource.Drawable.dot;
            break;
          case AREA.RIGHT:
            selected = "*";
            selectedButton = Resource.Drawable.mul;
            break;
          case AREA.TOP:
            selected = "/";
            selectedButton = Resource.Drawable.div;
            break;
          }
          aaa.SetImageResource(selectedButton);
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          //var a = FindViewById<TextView>(Resource.Id.formula).Text.Last();
          if(FindViewById<TextView>(Resource.Id.formula).Text.Count() > 0) {
            if(FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('+') |
            FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('-') |
            FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('/') |
            FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('*') |
            FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('.')) {
              formula.Text = formula.Text.Substring(0, formula.Text.Count() - 1) + selected;
            } else {
              FindViewById<TextView>(Resource.Id.formula).Text += selected;
            }
          }
          break;
        }
      };
      #endregion

      #region オペレータボタン(=とか)の設定
      var buttonop2 = FindViewById<ImageButton>(Resource.Id.buttonop2);
      var imgop2 = Android.Graphics.BitmapFactory.DecodeResource(Resources, Resource.Drawable.op2);
      buttonop2.ViewTreeObserver.GlobalLayout += (sender, e) => {
        buttonop2.SetImageBitmap(Bitmap.CreateScaledBitmap(imgop2, buttonop2.Width, buttonop2.Height, true));
      };
      buttonop2.Touch += (sender, e) => {
        switch(e.Event.Action) {
        case MotionEventActions.Down:
          GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
          startX = e.Event.GetX();
          startY = e.Event.GetY();
          aaa.SetImageResource(Resource.Drawable.equal);
          selectedButton = Resource.Drawable.equal;
          break;
        case MotionEventActions.Move:
          var area = AreaDetect(startX, startY, e.Event.GetX(), e.Event.GetY(), 100);
          switch(area) {
          case AREA.CENTER:
            selectedButton = Resource.Drawable.delete;
            break;
          case AREA.LEFT:
            selectedButton = Resource.Drawable.allClear;
            break;
          case AREA.BOTTOM:
            selectedButton = Resource.Drawable.equal;
            break;
          case AREA.RIGHT:
            selectedButton = Resource.Drawable.Clear;
            break;
          case AREA.TOP:
            selectedButton = Resource.Drawable.num0_4;
            break;
          }
          aaa.SetImageResource(selectedButton);
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          if(selectedButton == Resource.Drawable.equal) {
            RPN rpn = new RPN();
            try {
              var lastFormula = formula.Text;
              formula.Text = rpn.Proc(formula.Text);
              FindViewById<TextView>(Resource.Id.lastformula).Text = lastFormula + "=";
              formulaHistory_.Insert(0, lastFormula);
            } catch(Exception except) {
              Toast.MakeText(this, except.Message, ToastLength.Long).Show();
            }
          } else if(selectedButton == Resource.Drawable.delete) {
            if(formula.Text.Count() > 0)
              formula.Text = formula.Text.Substring(0, formula.Text.Count() - 1);
          } else {

          }
          break;
        }
      };
      #endregion

    }




    enum AREA {
      CENTER,
      RIGHT,
      LEFT,
      TOP,
      BOTTOM
    }

    AREA AreaDetect(float _startX, float _startY, float _nowX, float _nowY, float _rectSize) {
      var diffX = _nowX - _startX;
      var diffY = _nowY - _startY;
      if(diffX > _rectSize && _nowY > _startX - diffX && _nowY < _startX + diffX) {
        return AREA.RIGHT;
      } else if(diffX < -_rectSize && _nowY > _startX + diffX && _nowY < _startX - diffX) {
        return AREA.LEFT;
      } else if(diffY > _rectSize && _nowX < _startY + diffY && _nowX > _startY - diffY) {
        return AREA.BOTTOM;
      } else if(diffY < -_rectSize && _nowX > _startY + diffY && _nowX < _startY - diffY) {
        return AREA.TOP;
      }

      return AREA.CENTER;
    }

    public void OnGlobalLayout() {
      throw new NotImplementedException();
    }
  }
}
