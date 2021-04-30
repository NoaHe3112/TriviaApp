﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using TriviaApp.Services;
using TriviaApp.Models;
using TriviaApp.Views;

namespace TriviaApp.ViewModels
{
    class HomeWhenLoggedViewModel
    {
        public ICommand Play => new Command(play);

        async void play()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            AmericanQuestion a = await proxy.GetRandomQuestion();
            string[] options = new string[4];
            Random r = new Random();
            int num = r.Next(0, 4);
            options[num] = a.CorrectAnswer;
            for(int i=0, optionNum =0; i < options.Length; i++)
            {
                if(options[i] == null)
                {
                    options[i] = a.OtherAnswers[optionNum];
                    optionNum++;
                }
            }
            GameViewModel game = new GameViewModel
            {
                Options = options,
                Question = a, 
                
            };

        }
    }
}