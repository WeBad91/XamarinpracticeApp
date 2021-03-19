using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace WeBad.Apps.CustomComponents
{
    public class ClickableLayout : ContentView
    {
        public static readonly BindableProperty ClickCommandProperty =
           BindableProperty.Create(nameof(ClickCommand), typeof(ICommand), typeof(ClickableLayout), null);

        public ICommand ClickCommand
        {
            get { return (ICommand)GetValue(ClickCommandProperty); }
            set { SetValue(ClickCommandProperty, value); }
        }


        public ClickableLayout()
        {
            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            ContentView container = this;
            tapGesture.Command = new Command(async() =>
               {
                   
                   await container.FadeTo(0, 100);
                   ClickCommand.Execute(null);                   
                   await container.FadeTo(1, 100);
               });
            container.GestureRecognizers.Add(tapGesture);

            container.Content = new ContentPresenter();
        } 

    }
}
