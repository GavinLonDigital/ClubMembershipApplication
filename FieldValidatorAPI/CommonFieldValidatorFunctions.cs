﻿using System;
using System.Text.RegularExpressions;

namespace FieldValidatorAPI
{
    public delegate bool RequiredValidDel(string fieldVal);
    public delegate bool StringLengthValidDel(string fieldVal, int min, int max);
    public delegate bool DateValidDel(string fieldVal, out DateTime validDateTime);
    public delegate bool PatternMatchValidDel(string fieldVal, string pattern);
    public delegate bool CompareFieldsValidDel(string fieldVal, string fieldValCompare);
    public class CommonFieldValidatorFunctions
    {
        private static RequiredValidDel _requiredValidDel = null;  //private storage for delegate defined above in namespace
        private static StringLengthValidDel _stringLengthValidDel = null;
        private static DateValidDel _dateValidDel = null;
        private static PatternMatchValidDel _patternMatchValidDel = null;
        private static CompareFieldsValidDel _compareFieldsValidDel = null;

        //Singleton pattern used (only one object is created
        public static RequiredValidDel RequiredFieldValidDel //Typed as bool delegate above and assigned the method and is public property.
        {
            get
            {
                if (_requiredValidDel == null) //only true the first use - Singleton
                    _requiredValidDel = new RequiredValidDel(RequiredFieldValid);

                return _requiredValidDel; //return new private delegate in public property that points to private method of this class below.
            }
        }

        public static StringLengthValidDel StringLengthFieldValidDel
        {
            get
            {
                if (_stringLengthValidDel == null)
                    _stringLengthValidDel = new StringLengthValidDel(StringFieldLengthValid);

                return _stringLengthValidDel;
            }
        }
        public static DateValidDel DateFieldValidDel
        {
            get
            {
                if (_dateValidDel == null)
                    _dateValidDel = new DateValidDel(DateFieldValid);

                return _dateValidDel;
            }
        }
        public static PatternMatchValidDel PatternMatchValidDel
        {
            get
            {
                if (_patternMatchValidDel == null)
                    _patternMatchValidDel = new PatternMatchValidDel(FieldPatternValid);

                return _patternMatchValidDel;
            }
        }

        public static CompareFieldsValidDel FieldsCompareValidDel
        {
            get
            {
                if (_compareFieldsValidDel == null)
                    _compareFieldsValidDel = new CompareFieldsValidDel(FieldComparisonValid);

                return _compareFieldsValidDel;
            }
        }


        private static bool RequiredFieldValid(string fieldVal) //method
        {
            if (!string.IsNullOrEmpty(fieldVal))
                return true;

            return false;

        }

        private static bool StringFieldLengthValid(string fieldVal, int min, int max)
        {
            if (fieldVal.Length >= min && fieldVal.Length <= max)
                return true;

            return false;

        }

        private static bool DateFieldValid(string dateTime, out DateTime validDateTime)
        {
            if (DateTime.TryParse(dateTime, out validDateTime))
                return true;

            return false;

        }

        private static bool FieldPatternValid(string fieldVal, string regularExpressionPattern)
        {
            Regex regex = new Regex(regularExpressionPattern);

            if (regex.IsMatch(fieldVal))
                return true;

            return false;

        }

        private static bool FieldComparisonValid(string field1, string field2)
        {
            if (field1.Equals(field2))
                return true;

            return false;
        }

    }
}
