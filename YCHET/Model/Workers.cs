using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;




namespace YCHET.Model
{
    public class Workers : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errors = new();
        private string phone_number;
        private string email;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public int Id { get; set; }
        public string Last_name { get; set; }
        public string First_name { get; set; }
        public string Patronymic { get; set; }
        public string Post { get; set; }
        public string Phone_number
        {
            get => phone_number;
            set
            {
                phone_number = value;
                ValidatePhoneNumber();
                OnPropertyChanged();
              
            }
        }


        public string Email
        {
            get => email;
            set
            {
                email = value;
                ValidateEmail();
                OnPropertyChanged();
            }
        }

    public bool HasErrors => _errors.Any();

    public IEnumerable GetErrors(string propertyName)
    {
        return _errors.ContainsKey(propertyName) ? _errors[propertyName] : null;
    }

    private void ValidatePhoneNumber()
    {
       ClearErrors(nameof(Phone_number));

        if (Phone_number.Where(s=>(int)s >=48 && (int)s <=57).Count() != 11)
        {
            AddError(nameof(Phone_number), "Номер телефона должен содержать 11 символов");
        }
        else
            {
                Regex validatePhoneNumberRegex = new Regex("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
                if(!validatePhoneNumberRegex.IsMatch(Phone_number))
                    AddError(nameof(Phone_number), "Номер телефона Некорректный");
            }
    }

    private void ValidateEmail()
    {
            //string error = null;

            ClearErrors(nameof(Email));

            //if (string.IsNullOrEmpty(Email))
            //    error = "Email не может быть пустым";
            //else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            //    error = "Некорректный формат email";

            if (string.IsNullOrWhiteSpace(Email))
            {
                AddError(nameof(Email), "Email не может быть пустым");
            }
            else if (!new EmailAddressAttribute().IsValid(Email))
            {
                AddError(nameof(Email), "Некорректный формат email");
            }
        }

    private void AddError(string propertyName, string error)
    {
        if (!_errors.ContainsKey(propertyName))
        {
            _errors[propertyName] = new List<string>();
        }

        if (!_errors[propertyName].Contains(error))
        {
            _errors[propertyName].Add(error);
            OnErrorsChanged(propertyName);
        }
    }

    private void ClearErrors(string propertyName)
    {
        if (_errors.Remove(propertyName))
        {
            OnErrorsChanged(propertyName);
        }
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected virtual void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
}
}
//{
//    public class Workers : IDataErrorInfo
//    {
//        public int Id { get; set; }
//        public string Last_name { get; set; }
//        public string First_name { get; set; }
//        public string Patronymic { get; set; }
//        public string Post { get; set; }
//        public int Phone_number { get; set; }
//        public string Email { get; set; }

//        public string this[string email]
//        {
//            get
//            {
//                string error = null;

//                switch (email)
//                {
//                    case nameof(Email):
//                        if (string.IsNullOrEmpty(Email))
//                            error = "Email не может быть пустым";
//                        else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
//                            error = "Некорректный формат email";
//                        break;


//                }

//                return error;
//            }
//        }

//        public string Error => null;

//        public int this[int number]
//        {
//            get
//            {
//                string error = null;

//                switch (number)
//                {


//                    case nameof(Phone_number):
//                        if (int.IsNullOrEmpty(Phone_number))
//                            error = "Номер телефона не может быть пустым";
//                        else if (!Regex.IsMatch(Phone_number, @"^\+?\d{10,15}$"))
//                            error = "Номер телефона должен содержать от 10 до 15 цифр";
//                        break;
//                }

//                return error;
//            }
//        }

//        public string Error => null;
//        //    public string this[string columnName]
//        //    {
//        //        get
//        //        {
//        //            string error = String.Empty;
//        //            switch (columnName)
//        //            {
//        //                case "Номер":
//        //                    if ((Phone_number < 0) || (Phone_number > 12))
//        //                    {
//        //                        error = "Номер телефона не более 12ти символов";
//        //                    }
//        //                    break;
//        //                case "Phone_number":
//        //                    //Обработка ошибок для свойства Name
//        //                    break;

//        //            }
//        //            return error;
//        //        }
//        //    }
//        //    public string Error
//        //    {
//        //        get { throw new NotImplementedException(); }
//        //    }
//        //}
//    }
