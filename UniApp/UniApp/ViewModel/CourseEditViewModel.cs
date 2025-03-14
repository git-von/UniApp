﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using UniApp.Model;
using Xamarin.Forms;

namespace UniApp.ViewModel
{
    public class CourseEditViewModel : BaseViewModel
    {
        private string code;
        private string totalWeight;
        private string totalMark;
        private string progress;
        private ObservableCollection<GradePredictStr> gradePredictList;
        private bool forNewCourse;

        public CourseEditViewModel(Course course)
        {
            DelCourseCommand = new Command(DelCourse);
            SaveCourseCommand = new Command(SaveCourse);
            BackCommand = new Command(Back);

            if (course != null)
            {
                ShowLines = true;
                Code = course.Code;
                TotalWeight = $"Total weight: {course.TotalWeight}%";
                TotalMark = $"Total marks: {course.TotalMark}%";
                Progress = $"Progress: {course.Progress}%";

                GradePredictList = new ObservableCollection<GradePredictStr>();
                int[] gradePredict = course.GradePredict;
                for (int grade = 0; grade < 5; grade++)
                {
                    GradePredictStr temp = new GradePredictStr();
                    temp.SetPrediction(7 - grade, gradePredict[grade], 100 - course.Progress);
                    GradePredictList.Add(temp);
                }
            }
            else
            {
                ShowLines = false;
            }
        }

        public string Code
        {
            get => code;
            set => SetProperty(ref code, value);
        }

        public string TotalWeight
        {
            get => totalWeight;
            set => SetProperty(ref totalWeight, value);
        }

        public string TotalMark
        {
            get => totalMark;
            set => SetProperty(ref totalMark, value);
        }

        public string Progress
        {
            get => progress;
            set => SetProperty(ref progress, value);
        }

        public ObservableCollection<GradePredictStr> GradePredictList
        {
            get => gradePredictList;
            set => SetProperty(ref gradePredictList, value);
        }

        public bool ShowLines
        {
            get => !forNewCourse;
            set => SetProperty(ref forNewCourse, !value);
        }

        public ICommand DelCourseCommand { get; }
        private async void DelCourse()
        {
            try
            {
                if (await DisplayYesNo("Deleting course", "Are you sure?"))
                {
                    DataAccessLayer.CurrentSemester.Courses.RemoveAt(DataAccessLayer.CurrentCourseIndex.Value);
                    DataAccessLayer.Save();
                    await OnNavigationBackAsync();
                }
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public ICommand SaveCourseCommand { get; }
        private async void SaveCourse()
        {
            try
            {
                if (forNewCourse)
                    DataAccessLayer.CurrentSemester.AddCourse(Code);
                else
                    DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value].Code = Code;

                DataAccessLayer.Save();
                await OnNavigationBackAsync();
            }
            catch (Exception ex)
            {
                await HandleException(ex);
            }
        }

        public ICommand BackCommand { get; }
        private async void Back()
        {
            await OnNavigationBackAsync();
        }
    }

    public class GradePredictStr
    {
        private string prediction;
        public string Prediction => prediction;
        public void SetPrediction(int grade, int req, int rem)
        {
            prediction = $"Grade {grade} requirement: {req}% out of {rem}%";
        }
    }
}
