namespace WarehouseManagementWeb.Api.Validators.Constants
{
    /// <summary>
    /// Класс констант для валидации.
    /// </summary>
    public class ValidationConst
    {
        #region Client

        /// <summary>
        /// Минимальная длина наименования клиента.
        /// </summary>
        public static int MIN_CLIENT_NAME_LENGTH = 3;

        /// <summary>
        /// Максимальная длина наименования клиента.
        /// </summary>
        public static int MAX_CLIENT_NAME_LENGTH = 20;

        /// <summary>
        /// Минимальная длина адреса клиента.
        /// </summary>
        public static int MIN_CLIENT_ADDRESS_LENGTH = 3;

        /// <summary>
        /// Максимальная длина адреса клиента.
        /// </summary>
        public static int MAX_CLIENT_ADDRESS_LENGTH = 20;


        /// <summary>
        /// Если не передан Id клиента.
        /// </summary>
        public static string NOT_VALID_CLIENT_ID = "Id клиента не передан.";

        /// <summary>
        /// Если не передано наименование клиента.
        /// </summary>
        public static string NOT_VALID_CLIENT_NAME = "Наименование клиента не может быть пустым.";

        /// <summary>
        /// Если не заполнено наименование клиента.
        /// </summary>
        public static string EMPTY_CLIENT_NAME = "Введите наименование клиента.";

        /// <summary>
        /// Если наименование клиента слишком короткое. 
        /// </summary>
        public static string MINIMUM_LENGTH_CLIENT_NAME =
            $"Наименование клиента должно быть не менее {MIN_CLIENT_NAME_LENGTH} символов.";

        /// <summary>
        /// Если наименование клиента слишком длинное. 
        /// </summary>
        public static string MAXIMUM_LENGTH_CLIENT_NAME =
            $"Наименование клиента должно быть не более {MAX_CLIENT_NAME_LENGTH} символов.";

        /// <summary>
        /// Если не передан адрес клиента.
        /// </summary>
        public static string NOT_VALID_CLIENT_ADDRESS = "Адрес клиента не может быть пустым.";

        /// <summary>
        /// Если не заполнен адрес клиента.
        /// </summary>
        public static string EMPTY_CLIENT_ADDRESS = "Введите адрес клиента.";

        /// <summary>
        /// Если адрес клиента слишком короткий. 
        /// </summary>
        public static string MINIMUM_LENGTH_CLIENT_ADDRESS =
            $"Адрес клиента должен быть не менее {MIN_CLIENT_ADDRESS_LENGTH} символов.";

        /// <summary>
        /// Если адрес клиента слишком длинный. 
        /// </summary>
        public static string MAXIMUM_LENGTH_CLIENT_ADDRESS =
            $"Адрес клиента должен быть не более {MAX_CLIENT_ADDRESS_LENGTH} символов.";

        /// <summary>
        /// Если статус клиента не валиден.
        /// </summary>
        public static string NOT_VALID_CLIENT_STATUS = "Недопустимый статус клиента.";

        #endregion

        #region IdentityUser

        /// <summary>
        /// Минимальная длина имени пользователя.
        /// </summary>
        public static int MIN_USER_FIRST_NAME_LENGTH = 3;

        /// <summary>
        /// Максимальная длина имени пользователя.
        /// </summary>
        public static int MAX_USER_FIRST_NAME_LENGTH = 20;

        /// <summary>
        /// Минимальная длина фамилии пользователя.
        /// </summary>
        public static int MIN_USER_LAST_NAME_LENGTH = 3;

        /// <summary>
        /// Максимальная длина фамилии пользователя.
        /// </summary>
        public static int MAX_USER_LAST_NAME_LENGTH = 20;


        /// <summary>
        /// Если не передано имя пользователя.
        /// </summary>
        public static string NOT_VALID_USER_FIRST_NAME = "Имя пользователя не может быть пустым.";

        /// <summary>
        /// Если не заполнено имя пользователя.
        /// </summary>
        public static string EMPTY_USER_FIRST_NAME = "Введите имя пользователя.";

        /// <summary>
        /// Если имя пользователя слишком короткое.
        /// </summary>
        public static string MINIMUM_LENGTH_USER_FIRST_NAME =
           $"Имя пользователя должно быть не менее {MIN_USER_FIRST_NAME_LENGTH} символов.";

        /// <summary>
        /// Если имя пользователя слишком длинное.
        /// </summary>
        public static string MAXIMUM_LENGTH_USER_FIRST_NAME =
            $"Имя пользователя должно быть не более {MAX_USER_FIRST_NAME_LENGTH} символов.";

        /// <summary>
        /// Если не передана фамилия пользователя.
        /// </summary>
        public static string NOT_VALID_USER_LAST_NAME = "Фамилия пользователя не может быть пустой.";

        /// <summary>
        /// Если не заполнена фамилия пользователя.
        /// </summary>
        public static string EMPTY_USER_LAST_NAME = "Введите фамилию пользователя.";

        /// <summary>
        /// Если фамилия пользователя слишком короткая.
        /// </summary>
        public static string MINIMUM_LENGTH_USER_LAST_NAME =
         $"Фамилия пользователя должна быть не менее {MIN_USER_LAST_NAME_LENGTH} символов.";

        /// <summary>
        /// Если фамилия пользователя слишком длинная.
        /// </summary>
        public static string MAXIMUM_LENGTH_USER_LAST_NAME =
            $"Фамилия пользователя должна быть не более {MAX_USER_LAST_NAME_LENGTH} символов.";

        /// <summary>
        /// Если не передан email пользователя.
        /// </summary>
        public static string NOT_VALID_USER_EMAIL = "Email пользователя не может быть пустым.";

        /// <summary>
        /// Если email не заполнен.
        /// </summary>
        public static string EMPTY_USER_EMAIL = "Введите email пользователя.";

        /// <summary>
        /// Если email имеет некорректный формат.
        /// </summary>
        public static string INVALID_USER_EMAIL_FORMAT = "Введите корректный email.";

        /// <summary>
        /// Если не передан пароль пользователя.
        /// </summary>
        public static string NOT_VALID_USER_PASSWORD = "Пароль пользователя не может быть пустым.";

        /// <summary>
        /// Если пароль пользователя не введен.
        /// </summary>
        public static string EMPTY_USER_PASSWORD = "Введите пароль пользователя.";

        /// <summary>
        /// Если не передано подтверждение пароля пользователя.
        /// </summary>
        public static string NOT_VALID_USER_PASSWORD_CONFIRM = 
            "Подтверждение пароля пользователя не может быть пустым.";

        /// <summary>
        /// Если подтверждение пароля пользователя не введено.
        /// </summary>
        public static string EMPTY_USER_PASSWORD_CONFIRM = "Подтвердите пароль пользователя.";

        /// <summary>
        /// Если пароль и подтверждение пароля пользователя не совпадают.
        /// </summary>
        public static string USER_PASSWORDS_NOT_MATCH = "Пароль и подтверждение пароля не совпадают.";

        /// <summary>
        /// Если не передан access token.
        /// </summary>
        public static string NOT_VALID_USER_ACCESS_TOKEN = "Токен доступа пользователя не может быть пустым.";

        /// <summary>
        /// Если access token пустой.
        /// </summary>
        public static string EMPTY_USER_ACCESS_TOKEN = "Передайте токен доступа пользователя.";

        /// <summary>
        /// Если не передан refresh token.
        /// </summary>
        public static string NOT_VALID_USER_REFRESH_TOKEN = "Токен обновления пользователя не может быть пустым.";

        /// <summary>
        /// Если refresh token пустой.
        /// </summary>
        public static string EMPTY_USER_REFRESH_TOKEN = "Передайте токен обновления пользователя.";

        #endregion
    }
}
