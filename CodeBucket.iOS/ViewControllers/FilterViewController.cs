using System;
using System.Linq;
using UIKit;
using CodeBucket.DialogElements;
using Humanizer;
using System.Reactive.Linq;

namespace CodeBucket.ViewControllers
{
    public abstract class FilterViewController : DialogViewController
    {
        protected FilterViewController()
            : base(UITableViewStyle.Grouped)
        {
            Title = "Filter & Sort";

            var cancel = NavigationItem.LeftBarButtonItem = new UIBarButtonItem { Image = Images.Buttons.Cancel };
            var search = NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Search);

            OnActivation(disposables =>
            {
                cancel.GetClickedObservable()
                      .Subscribe(_ => DismissViewController(true, null))
                      .AddTo(disposables);

                search.GetClickedObservable()
                    .Do(_ => ApplyButtonPressed())
                    .Subscribe(_ => DismissViewController(true, null))
                    .AddTo(disposables);
            });
        }

        public void Present(UIViewController presenter)
        {
            presenter.PresentViewController(new ThemedNavigationController(this), true, null);
        }

        public abstract void ApplyButtonPressed();

        public void CloseViewController()
        {
            DismissViewController(true, null);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            TableView.ReloadData();
        }

        public class EnumChoiceElement<T> : ButtonElement where T : struct, IConvertible
        {
            private T _value;

            public new T Value
            {
                get { return _value; }
                set
                {
                    _value = value;
                    base.Value = ((Enum)Enum.ToObject(typeof(T), value)).Humanize();
                }
            }

            public EnumChoiceElement(string title, T defaultVal)
                : base(title, string.Empty)
            {
                Value = defaultVal;
            }
        }

        public EnumChoiceElement<T> CreateEnumElement<T>(string title, T value) where T : struct, IConvertible
        {
            var element = new EnumChoiceElement<T>(title, value);

            element.Clicked.Subscribe(_ =>
            {
                var ctrl = new DialogViewController(UITableViewStyle.Grouped);
                ctrl.Title = title;

                var sec = new Section();
                foreach (var x in Enum.GetValues(typeof(T)).Cast<Enum>())
                {
                    var e = new ButtonElement(x.Humanize())
                    { 
                        Accessory = object.Equals(x, element.Value) ? 
                            UITableViewCellAccessory.Checkmark : UITableViewCellAccessory.None 
                    };
                    e.Clicked.Subscribe(__ =>
                    { 
                        element.Value = (T)Enum.ToObject(typeof(T), x); 
                        NavigationController.PopViewController(true);
                    });

                    sec.Add(e);
                }
                ctrl.Root.Reset(sec);
                NavigationController.PushViewController(ctrl, true);
            });

            return element;
        }

        public class MultipleChoiceElement<T> : ButtonElement
        {
            public T Obj;
            public MultipleChoiceElement(string title, T obj)
                : base(title, CreateCaptionForMultipleChoice(obj))
            {
                Obj = obj;
            }
        }

        protected MultipleChoiceElement<T> CreateMultipleChoiceElement<T>(string title, T o)
        {
            var element = new MultipleChoiceElement<T>(title, o);
            element.Clicked.Subscribe(_ =>
            {
                var en = new MultipleChoiceViewController(element.Caption, o);
                en.Disappearing.Subscribe(__ => {
                    element.Value = CreateCaptionForMultipleChoice(o);
                });
                NavigationController.PushViewController(en, true);
            });

            return element;
        }

        private static string CreateCaptionForMultipleChoice<T>(T o)
        {
            var fields = o.GetType().GetFields();
            var sb = new System.Text.StringBuilder();
            int trueCounter = 0;
            foreach (var f in fields)
            {
                if ((bool)f.GetValue(o))
                {
                    sb.Append(f.Name);
                    sb.Append(", ");
                    trueCounter++;
                }
            }
            var str = sb.ToString();
            if (str.EndsWith(", "))
                return trueCounter == fields.Length ? "Any" : str.Substring(0, str.Length - 2);
            return "None";
        }
    }
}

