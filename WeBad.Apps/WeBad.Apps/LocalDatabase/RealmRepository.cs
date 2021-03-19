using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace WeBad.Apps.LocalDatabase
{
    public class RealmRepository : IRealmRepository
    {
        private Realm realm;
        public RealmRepository()
        {
            try
            {
                realm = Realm.GetInstance();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool Add<T>(T entity) where T : RealmObject
        {
            bool retVal = false;
            try
            {
                realm.Write(() =>
                {
                    T addedEntity = realm.Add(entity);
                });

                retVal = true;
            }
            catch (Exception)
            {

                retVal = false;
            }
            return retVal;
        }

        public bool Delete<T>(long id) where T : RealmObject
        {
            bool retVal = false;

            try
            {
                T entity = Get<T>(id);
                
                realm.Write(() =>
                {
                    realm.Remove(entity);
                });

                retVal = true;
            }
            catch
            {

                retVal = false;
            }

            return retVal;
        }

        public T Get<T>(long id) where T : RealmObject
        {
            T retVal = null;

            try
            {
                retVal = realm.Find<T>(id);
            }
            catch
            {
                retVal = null;
            }

            return retVal;
            
        }

        public IEnumerable<T> GetAll<T>() where T : RealmObject
        {
            return realm.All<T>();
        }

        public bool Update(Action updateAction)
        {
            bool retVal = false;
            try
            {
                realm.Write(() =>
                {
                    updateAction();
                });

                retVal = true;
            }
            catch (Exception)
            {

                retVal = false;
            }
            return retVal;
        }
    }
}
