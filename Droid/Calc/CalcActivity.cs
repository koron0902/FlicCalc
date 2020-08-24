
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
    List<string> formulaHistory_;
    protected override void OnCreate(Bundle savedInstanceState) {
      base.OnCreate(savedInstanceState);

      SetContentView(Resource.Layout.calculator);
      // Create your application here
      formulaHistory_ = new List<string>();
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
      var formula = FindViewById<TextView>(Resource.Id.formula);
      #region 数値ボタン0~4の設定
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
          var area = AreaDetect(startX, startY, e.Event.GetX(), e.Event.GetY(), 100);
          switch(area) {
          case AREA.CENTER:
            selected = "0";
            break;
          case AREA.LEFT:
            selected = "1";
            break;
          case AREA.BOTTOM:
            selected = "2";
            break;
          case AREA.RIGHT:
            selected = "3";
            break;
          case AREA.TOP:
            selected = "4";
            break;
          }
          aaa.Text = selected;
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          formula.Text += selected;
          break;
        }
      };
      #endregion

      #region 数値ボタン5~9の設定
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
          var area = AreaDetect(startX, startY, e.Event.GetX(), e.Event.GetY(), 100);
          switch(area) {
          case AREA.CENTER:
            selected = "5";
            break;
          case AREA.LEFT:
            selected = "6";
            break;
          case AREA.BOTTOM:
            selected = "7";
            break;
          case AREA.RIGHT:
            selected = "8";
            break;
          case AREA.TOP:
            selected = "9";
            break;
          }
          aaa.Text = selected;
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          formula.Text += selected;
          break;
        }
      };
      #endregion

      #region  オペレータボタンの設定
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
          var area = AreaDetect(startX, startY, e.Event.GetX(), e.Event.GetY(), 100);
          switch(area) {
          case AREA.CENTER:
            selected = "+";
            break;
          case AREA.LEFT:
            selected = "-";
            break;
          case AREA.BOTTOM:
            selected = ".";
            break;
          case AREA.RIGHT:
            selected = "*";
            break;
          case AREA.TOP:
            selected = "/";
            break;
          }
          aaa.Text = selected;
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
          var area = AreaDetect(startX, startY, e.Event.GetX(), e.Event.GetY(), 100);
          switch(area) {
          case AREA.CENTER:
            selected = "X";
            break;
          case AREA.LEFT:
            selected = "AC";
            break;
          case AREA.BOTTOM:
            selected = "=";
            break;
          case AREA.RIGHT:
            selected = "C";
            break;
          case AREA.TOP:
            selected = "↑";
            break;
          }
          aaa.Text = selected;
          break;
        case MotionEventActions.Up:
          GetSystemService(WindowService).JavaCast<IWindowManager>().RemoveView(layout);
          if(selected.Equals("=")) {
            RPN rpn = new RPN();
            FindViewById<TextView>(Resource.Id.lastformula).Text = formula.Text + "=";
            formulaHistory_.Insert(0, formula.Text);
            try {
              formula.Text = rpn.Proc(formula.Text);
            } catch(Exception except) {
              Toast.MakeText(this, except.Message, ToastLength.Long).Show();
            }
          } else if(selected.Equals("X")) {
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
  }
}
