﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using UniApp.Model;

namespace UniApp.ViewModel
{
    public class AssessViewModel : BaseViewModel
    {
        private ObservableCollection<Assessment> assessList;

        public AssessViewModel()
        {
            try
            {
                AssessList = new ObservableCollection<Assessment>(DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value].Assessments);
            }
            catch
            {
                AssessList = null;
            }
        }

        public ObservableCollection<Assessment> AssessList
        {
            get => assessList;
            set => SetProperty(ref assessList, value);
        }

        public bool ShowMsg => assessList is null;
        public bool ShowList => assessList != null;
        public string Title
        {
            get
            {
                if (DataAccessLayer.CurrentCourseIndex is null)
                    return "Unavailable";
                return DataAccessLayer.CurrentSemester.Courses[DataAccessLayer.CurrentCourseIndex.Value].Code;
            }
        }
    }
}
