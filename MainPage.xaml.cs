using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace Wordle;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        LabelScroll();
    }

    private void EasyButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new EasyPage());  //sends user to easy page
    }

    private void HardButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new HardPage());
    }
    private async void LabelScroll()
    {
        Label.TranslationX = 1500; //start position of label

        while (true)
        {
            await Label.TranslateTo(-1000, 255, 10000, Easing.Linear); //label flies across the screen and settles at the bottom 
            Label.TranslationX = 1000; 
        }
    }

}

