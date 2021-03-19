using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeBad.Apps.LocalDatabase
{
    public interface IRealmRepository
    {
        bool Add<T>(T entity) where T : RealmObject;

        bool Update(Action updateAction);

        T Get<T>(long id) where T : RealmObject;

        IEnumerable<T> GetAll<T>() where T : RealmObject;


        bool Delete<T>(long id) where T : RealmObject;
    }
}
