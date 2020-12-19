using System;

namespace CookieBook.Infrastructure.DTO.Base
{
    public class AccountDto : EntityDto
    {
        /// <summary>
        /// Nick that will be used to distinguish users one from another, from te UI point of view.
        /// </summary>
        /// <example>Steave77</example>
        public string Nick { get; set; }
    }
}