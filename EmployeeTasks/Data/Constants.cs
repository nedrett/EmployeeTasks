namespace EmployeeTasks.Data
{
    /// <summary>
    /// Contains all constants
    /// </summary>
    public class Constants
    {
        /// <summary>
        /// Employee Data Model Constants
        /// </summary>
        public class Employee
        {
            public const int FullNameMinLength = 2;

            public const int FullNameMaxLength = 30;

            public const int EmailMinLength = 5;

            public const int EmailMaxLength = 30;

            public const int PhoneNumberMinLength = 5;

            public const int PhoneNumberMaxLength = 15;
        }

        /// <summary>
        /// Task Data Model Constants
        /// </summary>
        public class Task
        {
            public const int TitleMinLength = 2;

            public const int TitleMaxLength = 15;

            public const int DescriptionMinLength = 10;

            public const int DescriptionMaxLength = 150;

        }
    }
}
