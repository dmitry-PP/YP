//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public int Price { get; set; }
        public System.DateTime DateRelease { get; set; }
        public bool Amount { get; set; }
        public int Author_ID { get; set; }
    
        public virtual Author Author { get; set; }
    }
}
