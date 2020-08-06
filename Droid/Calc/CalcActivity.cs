
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
      var aaa = layout.FindViewById<TextView>(Resource.Id.textView1);

      float startX = 0, startY = 0;
      string selected = "0";
      FindViewById<ImageButton>(Resource.Id.button0_4).Touch += (sender, e) => {
        switch(e.Event.Action) {
        case MotionEventActions.Down:
          GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
          startX = e.Event.GetX();
          startY = e.Event.GetY();
          aaa.Text = "0";
          selected = "0";
          break;
        case MotionEventActions.Move:
          var X = e.Event.GetX();
          var Y = e.Event.GetY();
          var diffX = X - startX;
          var diffY = Y - startY;

          if(diffX < 100 && diffX > -100 && diffY < 100 && diffY > -100) {
            selected = "0";
          } else {
            if(diffX > 100 && Y > startX - diffX && Y < startX + diffX) {
              selected = "3";
            } else if(diffX < -100 && Y > startX + diffX && Y < startX - diffX) {
              selected = "1";
            } else if(diffY > 100 && X < startY + diffY && X > startY - diffY) {
              selected = "2";
            } else if(diffY < -100 && X > startY + diffY && X < startY - diffY) {
              selected = "4";
            }
          }
          Android.Util.Log.Debug("diff", $"{diffX}, {diffY}");
          aaa.Text = selected;
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          FindViewById<TextView>(Resource.Id.formula).Text += selected;
          break;
        }
      };



      FindViewById<ImageButton>(Resource.Id.button5_9).Touch += (sender, e) => {
        switch(e.Event.Action) {
        case MotionEventActions.Down:
          GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
          startX = e.Event.GetX();
          startY = e.Event.GetY();
          aaa.Text = "5";
          selected = "5";
          break;
        case MotionEventActions.Move:
          var X = e.Event.GetX();
          var Y = e.Event.GetY();
          var diffX = X - startX;
          var diffY = Y - startY;

          if(diffX < 100 && diffX > -100 && diffY < 100 && diffY > -100) {
            selected = "5";
          } else {
            if(diffX > 100 && Y > startX - diffX && Y < startX + diffX) {
              selected = "8";
            } else if(diffX < -100 && Y > startX + diffX && Y < startX - diffX) {
              selected = "6";
            } else if(diffY > 100 && X < startY + diffY && X > startY - diffY) {
              selected = "7";
            } else if(diffY < -100 && X > startY + diffY && X < startY - diffY) {
              selected = "9";
            }
          }
          Android.Util.Log.Debug("diff", $"{diffX}, {diffY}");
          aaa.Text = selected;
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          FindViewById<TextView>(Resource.Id.formula).Text += selected;
          break;
        }
      };

      FindViewById<ImageButton>(Resource.Id.buttonop).Touch += (sender, e) => {
        switch(e.Event.Action) {
        case MotionEventActions.Down:
          GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
          startX = e.Event.GetX();
          startY = e.Event.GetY();
          aaa.Text = "+";
          selected = "+";
          break;
        case MotionEventActions.Move:
          var X = e.Event.GetX();
          var Y = e.Event.GetY();
          var diffX = X - startX;
          var diffY = Y - startY;

          if(diffX < 100 && diffX > -100 && diffY < 100 && diffY > -100) {
            selected = "+";
          } else {
            if(diffX > 100 && Y > startX - diffX && Y < startX + diffX) {
              selected = "*";
            } else if(diffX < -100 && Y > startX + diffX && Y < startX - diffX) {
              selected = "-";
            } else if(diffY > 100 && X < startY + diffY && X > startY - diffY) {
              selected = ".";
            } else if(diffY < -100 && X > startY + diffY && X < startY - diffY) {
              selected = "/";
            }
          }
          Android.Util.Log.Debug("diff", $"{diffX}, {diffY}");
          aaa.Text = selected;
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          var a = FindViewById<TextView>(Resource.Id.formula).Text.Last();
          if(FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('+') |
          FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('-') |
          FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('/') |
          FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('*') |
          FindViewById<TextView>(Resource.Id.formula).Text.Last().Equals('.')) {
            FindViewById<TextView>(Resource.Id.formula).Text = FindViewById<TextView>(Resource.Id.formula).Text.Substring(0, FindViewById<TextView>(Resource.Id.formula).Text.Count() - 1) + selected;
          } else {
            FindViewById<TextView>(Resource.Id.formula).Text += selected;
          }
          break;
        }
      };

      FindViewById<ImageButton>(Resource.Id.buttonop2).Touch += (sender, e) => {
        switch(e.Event.Action) {
        case MotionEventActions.Down:
          GetSystemService(WindowService).JavaCast<IWindowManager>().AddView(layout, param);
          startX = e.Event.GetX();
          startY = e.Event.GetY();
          aaa.Text = "X";
          selected = "X";
          break;
        case MotionEventActions.Move:
          var X = e.Event.GetX();
          var Y = e.Event.GetY();
          var diffX = X - startX;
          var diffY = Y - startY;

          if(diffX < 100 && diffX > -100 && diffY < 100 && diffY > -100) {
            selected = "X";
          } else {
            if(diffX > 100 && Y > startX - diffX && Y < startX + diffX) {
              selected = "C";
            } else if(diffX < -100 && Y > startX + diffX && Y < startX - diffX) {
              selected = "AC";
            } else if(diffY > 100 && X < startY + diffY && X > startY - diffY) {
              selected = "=";
            } else if(diffY < -100 && X > startY + diffY && X < startY - diffY) {
              selected = "↑";
            }
          }
          Android.Util.Log.Debug("diff", $"{diffX}, {diffY}");
          aaa.Text = selected;
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          if(selected.Equals("=")) {
            RPN rpn = new RPN();
            FindViewById<TextView>(Resource.Id.formula).Text = rpn.Proc(FindViewById<TextView>(Resource.Id.formula).Text);
          } else {
          }
          break;
        }
      };



    enum AREA {
      CENTER,
      RIGHT,
      LEFT,
      TOP,
      BOTTOM
    }

    AREA SelectetArea(float _startX, float _startY, float _nowX, float _nowY, float _rectSize) {
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
  }
}
