using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//Модель базы данных, заданная программно.

namespace ClassLibrary.Core
{
    public class DBModel
    {
        //Создание базы данных.
        public static void Initialization()
        {
            using (var db = new OrganizationsContext())
            {
                String str = "abc";
                var OKFC = new OKFC { name = str };
                db.OKFC.Add(OKFC);
                db.SaveChanges();
            }
        }
    }

    //Содержит данные об организации.
    public class Organization
    {
        [Key][Index]
        public int id { get; set; }//Идентификатор организации в БД. Выступает в качестве первичного ключа.
        [Index][MaxLength(100)]
        public string name { get; set; }//Наименование организации
        [Index]
        public int OKPO { get; set; }//ОКПО организации.
        [Index]
        public int codeOKFC { get; set; }//Код ОКФС(внешний ключ)
        [Index]
        public int codeOKVED { get; set; }//Код ОКВЕД (внешний ключ)
        [Index]
        public int INN { get; set; }//ИНН организации.

        public virtual OKFC OKFC { get; set; }//Ссылка на ОКФС организации.
        public virtual OKVED OKVED { get; set; }//Ссылка на ОКВЕД организации.
    }
    
    //Содержит данные о кодах и наименованиях ОКФС.
    public class OKFC
    {
        [Key]
        public int codeOKFC { get; set; }//Код ОКФС. Первичный ключ.
        public string name { get; set; }//Наименование ОКФС.

        public virtual List<Organization> Organizations { get; set; }//Организации, имеющие данный ОКФС.
    }

    //Содержит данные о кодах и наименованиях ОКВЭД.
    public class OKVED
    {
        [Key]
        public int codeOKVED { get; set; }//Код ОКВЭД.
        public string name { get; set; }//Наименование ОКВЭД.

        public virtual List<Organization> Organizations { get; set; }//Организации, имеющие данный ОКВЭД.
    }

    //Класс контекста данных. Обеспечивает поддержку Code-First, информирует Entity Framework о классах, описывающих модель БД.
    public class OrganizationsContext:DbContext
    {
        public OrganizationsContext():base("OrganizationsConnectionString"){ }

        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OKFC> OKFC { get; set; }
        public DbSet<OKVED> OKVED { get; set; }
    }
}
