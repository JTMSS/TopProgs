using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TopProgs.DB;

namespace TopProgs.Helpers
{
    internal class SavedDataHelper : IDisposable 
    {
        private SavedDataEntities mclsDB = new SavedDataEntities();

        public void Dispose()
        {
            mclsDB.Dispose();
        }

        public bool LoadItem<T>(string vstrUser, 
                                string vstrTitle,
                                string vstrProduct,
                                string vstrKey, 
                                ref T robjItem, 
                                ref string rstrErr)
        {
            IQueryable<SavedDataItem> qrySDItems;
            byte[] bytData;
            bool blnOk = false;

            try
            {
                qrySDItems = mclsDB.SavedDataItems.Where(x => x.Key == vstrKey
                                                              &&
                                                              x.SavedDataUser.AspNetUser.UserName == vstrUser
                                                              &&
                                                              (
                                                                x.SavedDataUser.Title == vstrTitle
                                                                ||
                                                                x.SavedDataUser.Title != null
                                                              )
                                                              &&
                                                              (
                                                                x.SavedDataUser.Product == vstrProduct
                                                                ||
                                                                x.SavedDataUser.Product == null
                                                              )
                                                        );
                if (qrySDItems != null && qrySDItems.Count() > 0)
                {
                    bytData = qrySDItems.First<SavedDataItem>().Data;

                    blnOk = RedisHelper.DeserializeFromBytes(bytData, ref robjItem);
                }
            }
            catch (Exception exc)
            {
                rstrErr = exc.Message;
            }

            return blnOk;
        }

        public bool SaveItem<T>(string vstrUser,
                                string vstrTitle,
                                string vstrProduct,
                                string vstrKey,
                                T vobjItem,
                                ref string rstrErr)
        {
            SavedDataUser sdUser;
            SavedDataItem sdItem;
            IQueryable<SavedDataUser> qrySDUser;
            IEnumerable<SavedDataItem> qrySDItem;
            IQueryable<AspNetUser> qryASPUser;
            byte[] bytData = null;
            bool blnOk = false;

            try
            {
                if (RedisHelper.SerializeToBytes(vobjItem, ref bytData))
                {
                    qrySDUser = mclsDB.SavedDataUsers.Where(x => x.AspNetUser.UserName == vstrUser
                                                                 &&
                                                                 x.Title == vstrTitle
                                                                 &&
                                                                 x.Product == vstrProduct);
                    if (qrySDUser != null && qrySDUser.Count() > 0)
                    {
                        sdUser = qrySDUser.First();

                        qrySDItem = sdUser.SavedDataItems.Where(x => x.Key == vstrKey);
                        if (qrySDItem != null && qrySDItem.Count() > 0)
                        {
                            sdItem = qrySDItem.First();
                            sdItem.Data = bytData;
                        }
                        else
                        {
                            sdItem = new SavedDataItem();
                            sdItem.Key = vstrKey;
                            sdItem.Data = bytData;
                            sdUser.SavedDataItems.Add(sdItem);
                        }
                    }
                    else
                    {
                        qryASPUser = mclsDB.AspNetUsers.Where(x => x.UserName == vstrUser);

                        sdUser = new SavedDataUser();
                        sdUser.UserID = qryASPUser.First().Id;
                        sdUser.Title = vstrTitle;
                        sdUser.Product = vstrProduct;

                        sdItem = new SavedDataItem();
                        sdItem.Key = vstrKey;
                        sdItem.Data = bytData;

                        sdUser.SavedDataItems.Add(sdItem);

                        mclsDB.SavedDataUsers.Add(sdUser);
                    }

                    mclsDB.SaveChanges();
                    blnOk = true;
                }
                else
                {
                    rstrErr = "Could not serialise data";
                }
            }
            catch (Exception exc)
            {
                rstrErr = exc.Message;
            }

            return blnOk;
        }
   }
}